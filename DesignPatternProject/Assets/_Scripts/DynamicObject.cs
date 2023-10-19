using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    protected GameObject playerCore;
    protected UnityEngine.AI.NavMeshAgent agent;
    protected IState currentState;
    

    // Start is called before the first frame update
    void Start()
    {
        currentState = new MovingState();
        currentState.EnterState(this);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerCore = GameObject.Find("PlayerCore");
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

    }

    public void ChangeState(IState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
