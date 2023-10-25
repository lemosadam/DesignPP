using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmall : Unit, IPlayerPrototype
{
    protected UnityEngine.AI.NavMeshAgent navMeshAgent;
    protected GameObject enemyCore;
    protected float moveSpeed = 5.0f;
    protected Transform target;
    public float speed = 1.0f;
    [SerializeField] protected bool isTouchingCore = false;
    [SerializeField] protected bool isAttacking = false;
    public float detectionRadius = 10f;
    private GameObject playerPrefab;


    // Start is called before the first frame update
    void Start()
    {
        unitHP = 50;
        currentState = State.Idle;
        base.Start();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyCore = GameObject.Find("EnemyCore");
        target = GameObject.Find("EnemyCore").transform;
        if (enemyCore == null)
        {
            Debug.LogError("EnemyCore GameObject not found. Make sure it is named 'EnemyCore' in the scene.");
        }



    }

    public void Initialize(GameObject playerPrefab)
    {
        this.playerPrefab = playerPrefab;
    }

    public GameObject Clone(Vector3 position, Quaternion rotation)
    {
        return Instantiate(playerPrefab, position, rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (unitHP <= 0)
        {
            Destroy(gameObject);
        }
        // State machine logic
        switch (currentState)
        {
            case State.Idle:
                // Transition to "Moving" state if a condition is met
                if (Input.GetKeyDown(KeyCode.W))
                {
                    currentState = State.Moving;
                }
                break;

            case State.Moving:

                CheckDistanceToEnemies();
                Move();
                // Transition to "Attacking" state if a condition is met
                if (isTouchingCore == true)
                {
                    CoreSplode();
                    currentState = State.Idle;
                }
                // Transition back to "Idle" state if a condition is met
                else if (isAttacking == true)
                {

                    currentState = State.Attacking;
                }
                break;

            case State.Attacking:
                if (Time.time - timeOfLastAttack >= attackCooldown)
                {
                    Attack();
                    timeOfLastAttack = Time.time;
                }
                if (attackTarget == null)
                {

                    currentState = State.Moving;
                }

                break;

            case State.CoreSplode:

                if (isTouchingCore == true)
                {
                    CoreSplode();

                }
                break;
        }
    }

    protected override void Move()
    {
        Debug.Log("I am moving");


        float distanceToEnemyCore = Vector3.Distance(transform.position, enemyCore.transform.position);
        if (distanceToEnemyCore < 15.0f)
        {
            Debug.Log("I am rotating");
            // Determine which direction to rotate towards
            Vector3 targetDirection = target.position - transform.position;
            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;
            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);
            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
            Vector3 directionToEnemyCore = enemyCore.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(directionToEnemyCore);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 localMoveDirection = Vector3.forward;
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

    }

    protected override void CheckDistanceToEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= detectionRadius)
            {
                // You have detected an enemy within the detection radius.
                // You can add your custom logic here, such as attacking the enemy.
                // For example, you can call an Attack() function on the enemy or deal damage.

                isAttacking = true;
                attackTarget = enemy;

            }
        }
    }



    protected override void Attack()
    {
        Debug.Log("I am Attacking");
        if (attackTarget != null)
        {
            Enemy enemyUnit = attackTarget.GetComponent<Enemy>();
            enemyUnit.unitHP = enemyUnit.unitHP - 10;
        }
        else
        {
            Debug.Log("No attack target");
        }

    }

    protected override void Idle()
    {
        Debug.Log("I am Idling");
    }

    protected override void CoreSplode()
    {
        Debug.Log("BOOM!");
        Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyCore"))
        {
            isTouchingCore = true;
        }
    }
}
