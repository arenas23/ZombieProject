using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;


    public void Initialize()
    {

        ChangeState(new PatrolState());
    }

    private void Update()
    {
        if (activeState != null)
        {
            activeState.Do();
        }
    }

    /// <summary>
    /// Changes the current active state to a new state. 
    /// If there is an existing active state, it exits that state before changing.
    /// Sets the new state as the active state, assigns the state machine reference and enemy reference to the new state, 
    /// then calls the new state's Enter method.
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(BaseState newState)
    {
        if (activeState != null)
        {
            activeState.Exit();
        }

        activeState = newState;

        if (activeState != null)
        {
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }

    }

}
