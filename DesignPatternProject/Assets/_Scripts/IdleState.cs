using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void EnterState(DynamicObject dynamicObject)
    {
        Debug.Log("Entering Idle State");
    }

    public void UpdateState(DynamicObject dynamicObject)
    {
        Debug.Log("Updating Idle State");
    }   

    public void ExitState(DynamicObject dynamicObject)
    {
        Debug.Log("Exiting Idle State");
    }   
}
