//#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static EnemyScript;
#if UNITY_EDITOR
using static UnityEditor.Experimental.GraphView.GraphView;
#endif

public class RMAttackCheck : ActionNode
{
    protected override void OnStart()
    {
        _rangedMinionScript = _enemyObject.GetComponent<RMScript>();
    }

    protected override void OnStop()
    {

    }


    protected override State OnUpdate()
    {
        // If withing range to attack
        Debug.Log(_rangedMinionScript.DistanceToPlayer);
        if (_rangedMinionScript.DistanceToPlayer <= _rangedMinionScript.AttackRange
            && _rangedMinionScript.DistanceToPlayer > _rangedMinionScript.StartFleeRange)
        {
            if (_rangedMinionScript.CurrentState != MinionScript.EnemyState.inAttack)
            {
                _rangedMinionScript.PreviousState = _rangedMinionScript.CurrentState;
            }
            _rangedMinionScript.CurrentState = MinionScript.EnemyState.inAttack;
            //Debug.Log("Enemy is in attack");
            return State.Success;
        }
        //Debug.Log("Enemy is NOT in attack");
        return State.Failure;

    }
}

