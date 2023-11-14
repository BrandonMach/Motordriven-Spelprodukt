using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMChaseNode2 : ActionNode
{
    public float lerpSpeed = 200.0f;

    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        // Should always chase if attack or taunt fails.
        if (_meleeMinionScript.CurrentState != MinionScript.EnemyState.chasing)
        {
            _meleeMinionScript.PreviousState = _meleeMinionScript.CurrentState;
        }
        _meleeMinionScript.CurrentState = MinionScript.EnemyState.chasing;
        //Debug.Log("Enemy is Chasing");
        return State.Success; 

    }
}
