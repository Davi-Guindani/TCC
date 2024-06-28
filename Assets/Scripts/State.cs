using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
 
public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected float startTime;
    public float time => Time.time - startTime;

    protected StateMachineCore core;
    protected Rigidbody2D rb => core.rb;
    // protected Animator animator;
    protected GroundSensor groundSensor => core.groundSensor;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }
    
    public void SetCore(StateMachineCore _core)
    {
        core = _core;
    }
    public void Initialise()
    {
        isComplete = false;
        startTime = Time.time; 
    }
}
