using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavy : Enemy, IEnemyPrototype
{

    private GameObject enemyPrefab;
    public GameManager gameManager;


    public void Initialize(GameObject enemyPrefab)
    {
        this.enemyPrefab = enemyPrefab;
    }

    public GameObject Clone(Vector3 position, Quaternion rotation)
    {
        return Instantiate(enemyPrefab, position, rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 2f;
        unitHP = 100;
        speed = .5f;
        currentState = State.Idle;
        base.Start();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerCore = GameObject.Find("PlayerCore");
        target = GameObject.Find("PlayerCore").transform;
        if (playerCore == null)
        {
            Debug.LogError("PlayerCore GameObject not found. Make sure it is named 'PlayerCore' in the scene.");
        }
        gameManager = FindObjectOfType<GameManager>();

    }

    private bool IsDead()
    {
        return gameObject == null || gameObject.activeSelf == false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsDead() && unitHP <= 0)
        {
            gameManager.money += 7;
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
                    Attack();
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


        float distanceToPlayerCore = Vector3.Distance(transform.position, playerCore.transform.position);
        if (distanceToPlayerCore <= 15.0f)
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
            Vector3 directionToPlayerCore = playerCore.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(directionToPlayerCore);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 localMoveDirection = Vector3.forward;
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

    }

    protected override void Attack()
    {
        Debug.Log("I am Attacking");
        if (attackTarget != null)
        {
            PlayerUnit playerUnit = attackTarget.GetComponent<PlayerUnit>();
            playerUnit.unitHP = playerUnit.unitHP - 15;
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
        if (other.gameObject.CompareTag("PlayerCore"))
        {
            isTouchingCore = true;
        }
    }

    protected override void CheckDistanceToEnemies()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= detectionRadius)
            {
                // You have detected an enemy within the detection radius.
                // You can add your custom logic here, such as attacking the enemy.
                // For example, you can call an Attack() function on the enemy or deal damage.
                
                
                isAttacking = true;
                attackTarget = player;
            }
        }
    }


}
