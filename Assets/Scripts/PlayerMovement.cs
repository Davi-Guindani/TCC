using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : StateMachineCore
{
    [SerializeField] private IdleState idleState;
    [SerializeField] private RunState runState;
    [SerializeField] private AirState airState;

    public float xInput { get; private set;}
    public bool jumpInput { get; private set;}

    void Start()
    {
        SetupInstances();
        machine.Set(idleState);
    }
    void Update()
    {
        CheckInput();
        HandleJump();
        SelectState();
        machine.state.Do();
    }
    void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
    }
    void HandleJump()
    {
        if (jumpInput && groundSensor.grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, airState.jumpSpeed);
        }
    }
    void SelectState()
    {
        if (groundSensor.grounded)
        {
            if (xInput == 0)
            {
                machine.Set(idleState);
            }
            else
            {
                machine.Set(runState);
            }
        }
        else
        {
            machine.Set(airState);
        }
    }
    void FixedUpdate()
    {
        HandleXMovement();
        ApplyFriction();
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
        if (groundSensor.grounded && xInput == 0 && rb.velocity.y <= 0)
        {
            rb.velocity *= runState.groundDecay;
        }
    }
}
