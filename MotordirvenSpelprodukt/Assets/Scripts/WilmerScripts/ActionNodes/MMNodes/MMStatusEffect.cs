using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMStatusEffect : ActionNode
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

        if (_meleeMinionScript.CurrentImpairement == EnemyScript.Impairement.none
            && _meleeMinionScript.OnGround)
        {
            return State.Failure;
        }
        return State.Success;
    }
}
