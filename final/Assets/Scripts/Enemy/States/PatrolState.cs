using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private int wayPointIndex = 0;
    private float waitTimer;
    // Amount of time to wait before moving to the next waypoint.
    private float waitTime = 3f;

    
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.transform.position);
    }

    public override void Do()
    {
        Debug.DrawRay(enemy.transform.position, enemy.transform.forward * enemy.sightRange, Color.magenta);
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {

    }

    /// <summary>
    /// Executes the patrol cycle for the enemy.
    /// </summary>
    /// <remarks>
    /// This method is responsible for moving the enemy along a predefined path in enemy.path (GameObject).
    /// It checks the remaining distance of the enemy's agent and waits for a certain amount of time
    /// before moving to the next waypoint.
    /// </remarks>
    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                if (wayPointIndex < enemy.path.wayPoints.Count - 1)
                {
                    wayPointIndex++;
                }
                else
                {
                    wayPointIndex = 0;
                }
                enemy.Agent.SetDestination(enemy.path.wayPoints[wayPointIndex].position);
                waitTimer = 0f;
            }
        }
    }
}
