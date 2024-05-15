using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private Animator animator;
    private CapsuleCollider rootColider;

    // Speed to be used in animator
    public float animatorSpeed;
    private Vector3 lastKnowPos;

    //private AudioSource audioSource; //Audio
    //[SerializeField] private AudioClip audioClip; //Audio

    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }

    // Last Know Position of the Player, updated when in sight
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Canvas canvas;

    [Header("Weapon")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)] public float fireRate;
    [SerializeField] private string currentState;

    public EnemyPath path;

    private GameObject player; // Revizar si es sphere y siborrar.
    //private GameObject debugsphere; // Revizar si es player y siborrar.

    [Header("Sight Values")]
    public float sightRange = 80f;
    public float sightRangeNormal = 80f;
    public float sightRangeAlert = 150f;
    public float fieldOfView = 85f;
    public float fieldOfViewNormal = 85f;
    public float fieldOfViewAlert = 180f;
    public float eyeHeight;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rootColider = GetComponent<CapsuleCollider>();
        stateMachine.Initialize();
        player = GameObject.FindWithTag("Player");
        health = maxHealth;
        SetupHealthBar(canvas, Camera.main);

        //audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(audioClip);
    }

    void Update()
    {
        // Get agent speed and set it into the animator
        animatorSpeed = agent.velocity.magnitude;
        if (animatorSpeed <= 0.01f)
        {
            animatorSpeed = 0f;
        }

        animator.SetFloat("speed", animatorSpeed);

        //audioSource.PlayOneShot(audioClip); // Audio

        currentState = stateMachine.activeState.ToString();
        //debugsphere.transform.position = lastKnowPos; //Revisar cual va
    }

    /// <summary>
    /// Checks if the player is within the sight range and can be seen by the enemy.
    /// </summary>
    /// <returns>True if the player is within the sight range and can be seen, false otherwise.</returns>
    public bool CanSeePlayer()
    {
        if (player != null)
        {
            // Is the player close enough to be seen
            if (Vector3.Distance(transform.position, player.transform.position) < sightRange)
            {

                Vector3 targetDirection = player.transform.position - transform.position + (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

                // Is the player in the field of view
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    Debug.DrawRay(ray.origin, ray.direction * sightRange, Color.green);
                    RaycastHit hitInfo = new RaycastHit();

                    // Does the ray hit the player
                    if (Physics.Raycast(ray, out hitInfo, sightRange))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            lastKnowPos = player.transform.position;
                            Debug.DrawRay(ray.origin, ray.direction * sightRange, Color.red);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBar.SetProgress(health / maxHealth, 5f);
        if (health <= 0)
        {
            Die();
            GameManager.Instance.defeatedEnemies += 1;
        }
        else
        {
            StartCoroutine(AttackOnDamageTaken());
        }
    }

    IEnumerator AttackOnDamageTaken()
    {
        fieldOfView = fieldOfViewAlert;
        sightRange = sightRangeAlert;
        yield return new WaitForSeconds(0.5f);
        fieldOfView = fieldOfViewNormal;
        sightRange = sightRangeNormal;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void SetupHealthBar(Canvas canvas, Camera camera)
    {
        healthBar.transform.SetParent(canvas.transform);
        if (healthBar.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.camera = camera;
        }
    }
}
