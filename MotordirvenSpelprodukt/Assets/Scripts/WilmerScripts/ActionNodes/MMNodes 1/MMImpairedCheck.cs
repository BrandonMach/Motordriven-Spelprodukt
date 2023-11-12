using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMImpairedCheck : ActionNode
{
    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {

        // If not impaired, return success
        if (_meleeMinionScript.CurrentState != EnemyScript.EnemyState.stunned
            && _meleeMinionScript.CurrentState != EnemyScript.EnemyState.airborne
            && _meleeMinionScript.CurrentState != EnemyScript.EnemyState.pushed
            && _meleeMinionScript.OnGround)
        {
            //Debug.Log("Enemy is NOT impaired");
            return State.Failure;
        }
        //Debug.Log("Enemy IS impaired");
        return State.Success;
    }
}
