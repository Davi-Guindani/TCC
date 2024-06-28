using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateMachineCore : MonoBehaviour
{
    public StateMachine machine;
    public Rigidbody2D rb;
    // public Animator animator;
    public GroundSensor groundSensor;

    public void SetupInstances()
    {
        machine = new StateMachine();

        State[] allChildStates = GetComponentsInChildren<State>();
        foreach (State state in allChildStates)
        {
            state.SetCore(this);
        }
    }
}
