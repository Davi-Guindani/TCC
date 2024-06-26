using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public float groundSpeed { get; private set;} 
    public float groundDecay { get; private set;}
    public float acceleration { get; private set;}
    
    public override void Enter()
    {
        groundSpeed = 1.5f;
        groundDecay = 0.9f;
        acceleration = 0.3f;
    }
    public override void Do()
    {
        if (!input.grounded)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {
        
    }
}