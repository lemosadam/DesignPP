using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : IState
{
   public void EnterState(DynamicObject dynamicObject)
    {
       Debug.Log("Entering Attacking State");
   }

    public void UpdateState(DynamicObject dynamicObject)
    {
         Debug.Log("Updating Attacking State");
    }

    public void ExitState(DynamicObject dynamicObject)
    {
         Debug.Log("Exiting Attacking State");
    }
}
