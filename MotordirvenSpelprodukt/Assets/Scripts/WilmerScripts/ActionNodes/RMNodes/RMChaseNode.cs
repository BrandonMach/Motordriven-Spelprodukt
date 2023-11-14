using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RMChaseNode : ActionNode
{
    public float lerpSpeed = 200.0f;

    protected override void OnStart()
    {
        _rangedMinionScript = _enemyObject.GetComponent<RMScript>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        // Should always chase if attack or taunt fails.
        if (_rangedMinionScript.CurrentState != MinionScript.EnemyState.chasing)
        {
            _rangedMinionScript.PreviousState = _rangedMinionScript.CurrentState;
        }
        _rangedMinionScript.CurrentState = MinionScript.EnemyState.chasing;
        //Debug.Log("Enemy is Chasing");
        return State.Success; 
    }
}
