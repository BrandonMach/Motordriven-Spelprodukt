using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMTaunt2 : ActionNode
{
    // MeleeMinionTaunt
    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        _playerScript = /*GameObject.FindWithTag("Player").GetComponent<Player>();*/ Player.Instance;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {

        // Should always chase if attack or taunt fails.
        if (_meleeMinionScript.CurrentState == MinionScript.EnemyState.taunt)
        {
            return State.Success;
        }
        
        //Debug.Log("Enemy is Chasing");
        return State.Failure;
    }
}
