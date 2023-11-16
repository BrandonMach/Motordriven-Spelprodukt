using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMImpairedCheck : ActionNode
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

        // If not impaired, return success
        if (_rangedMinionScript.CurrentState != MinionScript.EnemyState.stunned
            && _rangedMinionScript.CurrentState != MinionScript.EnemyState.airborne
            && _rangedMinionScript.CurrentState != MinionScript.EnemyState.pushed
            && _rangedMinionScript.OnGround)
        {
            //Debug.Log("Enemy is NOT impaired");
            return State.Failure;
        }
        //Debug.Log("Enemy IS impaired");
        return State.Success;
    }
}
