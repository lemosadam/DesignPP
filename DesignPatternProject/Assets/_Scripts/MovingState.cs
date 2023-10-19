using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : DynamicObject, IState
{

    public void EnterState(DynamicObject dynamicObject)
    { 
       Debug.Log("Entering Moving State");
    }

    public void UpdateState(DynamicObject dynamicObject)
    {
        Debug.Log("Updating Moving State");
    }   

    public void ExitState(DynamicObject dynamicObject)
    {
        Debug.Log("Exiting Moving State");
    }   
}
