using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutOfCombat : ActionNode
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

        return State.Failure;
        if (_meleeMinionScript.OutOfCombat)
        {

            return State.Success;
        }
        else
        {
            return State.Failure;
        }
        return State.Success;
        return State.Failure;
    }
}
