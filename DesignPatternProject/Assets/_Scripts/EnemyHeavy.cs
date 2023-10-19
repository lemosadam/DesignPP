using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavy : Enemy
{
    
    // Start is called before the first frame update
    void Start()
    {
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


    }

    // Update is called once per frame
    void Update()
    {

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

                Move();
                // Transition to "Attacking" state if a condition is met
                if (isTouchingCore == true)
                {
                    CoreSplode();
                    currentState = State.Idle;
                }
                // Transition back to "Idle" state if a condition is met
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    Attack();
                    currentState = State.Attacking;
                }
                break;

            case State.Attacking:
                // Transition back to "Idle" state if a condition is met
                if (Input.GetKeyDown(KeyCode.I))
                {
                    Idle();
                    currentState = State.Idle;
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



}
