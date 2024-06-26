using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

// State class is tagged as abstract cause we dont want it to be created as an object
// Instead, we want it to work like a template to other classes 
public abstract class State : MonoBehaviour
{
    // isComplete is a property where the getter is public and the setter is protected
    // This means that any class will be able to check whenever the state is finished
    // But only classes that inherit from State will be able to set their own isComplete property
    // So its handled internally, but anybody else can read it
    public bool isComplete { get; protected set; }

    protected float startTime;

    // By definition, properties with lambda expressions can't be set
    // The expression is always going to be evaluated when we call it 
    // So we can declare it as public without problems
    public float time => Time.time - startTime;

    protected Rigidbody2D rb;

    // protected Animator animator;
    protected PlayerMovement input;

    // All these methods are tagged as virtual cause I want that every class that inherit from State
    // Override them with its own methods
    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }
    
    public void Setup(Rigidbody2D _rb, PlayerMovement _input)
    {
        rb = _rb;
        input = _input;
    }
    public void Initialise()
    {
        isComplete = false;
        startTime = Time.time; 
    }
}
