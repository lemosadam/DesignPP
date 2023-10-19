using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void EnterState(DynamicObject dynamicObject);
    void UpdateState(DynamicObject dynamicObject);
    void ExitState(DynamicObject dynamicObject);
}
