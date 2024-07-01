using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
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

    public StateMachine machine;
    protected StateMachine parent;
    public State state => machine.state;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }
    
    public void DoBranch()
    {
        Do();
        state?.DoBranch();
    }
    public void FixedDoBranch()
    {
        FixedDo();
        state?.FixedDoBranch();
    }
    protected void Set(State newState, bool forceReset = false)
    {
        machine.Set(newState, forceReset);
    }
    public void SetCore(StateMachineCore _core)
    {
        machine = new StateMachine();
        core = _core;
    }
    public void Initialise(StateMachine _parent)
    {
        parent = _parent;
        isComplete = false;
        startTime = Time.time; 
    }
}
