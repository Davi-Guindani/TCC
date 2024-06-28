using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private BoxCollider2D groundCheck;
    [SerializeField] private LayerMask groundMask;

    public bool grounded { get; private set;}

    void FixedUpdate()
    {
        CheckGround();
    }

    void CheckGround()
    {
        // Using the collider more like a rectangule to check collision
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }
}
