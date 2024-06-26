using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        
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