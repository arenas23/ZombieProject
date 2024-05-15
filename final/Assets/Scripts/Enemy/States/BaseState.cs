using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all enemy states.
/// </summary>
/// <remarks>
/// This class is the base class for all enemy states. It provides the basic functionality
/// Enter() as Start, Do() as update and Exit() as OnDisable methods.
/// </remarks>
public abstract class BaseState
{
    public Enemy enemy;
    public StateMachine stateMachine;

    public abstract void Enter();
    public abstract void Do();
    public abstract void Exit();
}
