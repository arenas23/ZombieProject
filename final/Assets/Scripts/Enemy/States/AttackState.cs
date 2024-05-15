using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    // Amount of time the enemy will wait before changing to the search state.
    private float losePlayerTime = 8f;
    private float shotTimer;


    [SerializeField] private AudioClip colectar1; //Audio

    //[SerializeField] private AudioClip colectar2; //Audio


    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();

    }

    private float enemyMovment = 5f;

    public override void Enter()
    {

    }

    /// <summary>
    /// Executes the attack behavior for the enemy.
    /// </summary>
    /// <remarks>
    /// This method is called when CanSeePlayer() returns true. It is responsible for
    /// making the enemy attack the player, if the player is out of sight it will wait
    /// for losePlayerTime seconds before changing to the search state.
    /// </remarks>
    public override void Do()
    {
        if (enemy.CanSeePlayer())
        {
            enemy.Agent.isStopped = true;
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);

            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.isStopped = false;
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * enemyMovment));
                moveTimer = 0;
            }
            //enemy.LastKnowPos = enemy.Player.transform.position; // Already implemented in CanSeePlayer
        }
        else
        {
            enemy.Agent.isStopped = false;
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > losePlayerTime)
            {
                stateMachine.ChangeState(new SearchState());
            }
        }
    }



    public void Shoot()
    {
        // Sets the shootDirection to the direction from the gun barrel to the player
        // Transform gunbarrel = enemy.gunBarrel;
        // Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;

        // Calls the ShootAmmo method in the EnemyAtackPrueba script, which shoots exactly one bullet
        shotTimer = 0;
        enemy.GetComponent<EnemyAtackPrueba>().ShootAmmo();
    }

    public override void Exit()
    {

    }
}
