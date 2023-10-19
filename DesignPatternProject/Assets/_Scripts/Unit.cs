using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected float unitHP;
    protected float unitDamage;
    protected float unitSpeed;
    public enum State
    {
        Idle,
        Moving,
        Attacking,
        CoreSplode
    }

    [SerializeField] protected State currentState = State.Idle;

    protected void Start()
    {

    }
    void Update()
    {
        // State machine logic
        switch (currentState)
        {
            case State.Idle:
                // Transition to "Moving" state if a condition is met
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Move();
                    currentState = State.Moving;
                }
                break;

            case State.Moving:
                // Transition to "Attacking" state if a condition is met
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Attack();
                    currentState = State.Attacking;
                }
                // Transition back to "Idle" state if a condition is met
                else if (Input.GetKeyDown(KeyCode.I))
                {
                    Idle();
                    currentState = State.Idle;
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
        }
    }

    protected abstract void Move();

    protected abstract void Attack();

    protected abstract void Idle();

    protected abstract void CoreSplode();
    

}
