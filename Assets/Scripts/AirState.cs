using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : State
{
    public float jumpSpeed { get; private set;}
    
    public override void Enter()
    {
        jumpSpeed = 3.5f;
    }
    public override void Do()
    {
        if (input.grounded)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {
        
    }
}