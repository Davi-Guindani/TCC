using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D groundCheck;
    [SerializeField] private LayerMask groundMask;

    List<State> states = new List<State>();
    [SerializeField] private IdleState idleState;
    [SerializeField] private RunState runState;
    [SerializeField] private AirState airState;
    private State state;

    public bool grounded { get; private set;}
    public float xInput { get; private set;}
    public bool jumpInput { get; private set;}

    void Start()
    {
        states.Add(idleState);
        states.Add(runState);
        states.Add(airState);

        foreach (State st in states)
        {
            st.Setup(rb, this);
        }
        
        state = idleState;
    }
    void Update()
    {
        CheckInput();
        HandleJump();
        SelectState();
        state.Do();
    }
    void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
    }
    void HandleJump()
    {
        if (jumpInput && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, airState.jumpSpeed);
        }
    }
    void SelectState()
    {
        State oldState = state;

        if (grounded)
        {
            if (xInput == 0)
            {
                state = idleState;
            }
            else
            {
                state = runState;
            }
        }
        else
        {
            state = airState;
        }

        if (oldState != state || oldState.isComplete)
        {
            oldState.Exit();
            state.Initialise();
            state.Enter();  
        }
    }
    void FixedUpdate()
    {
        CheckGround();
        HandleXMovement();
        ApplyFriction();
    }
    void CheckGround()
    {
        // Using the collider more like a rectangule to check collision
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }
    void HandleXMovement()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            // Calculates the speed increment and clamp it to maintain it in the groundSpeed span (0, groundSpeed)
            float increment = xInput * runState.acceleration;
            float newSpeed = Mathf.Clamp(rb.velocity.x + increment, -runState.groundSpeed, runState.groundSpeed);

            rb.velocity = new Vector2(newSpeed, rb.velocity.y);

            FaceInput();
        }
    }
    void FaceInput()
    {
        // Rotates the sprite in the x axis to match the movement direction
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }
    void ApplyFriction()
    {
        // Decreases the horizontal speed only when grounded and not moving horizontally 
        // The y axis velocity check its to prevent a sticky felling while jumping
        if (grounded && xInput == 0 && rb.velocity.y <= 0)
        {
            rb.velocity *= runState.groundDecay;
        }
    }
}
