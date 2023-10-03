using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectCheck : ActionNode
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
        if (_playerScript != null && _meleeMinionScript != null)
        {

            if (_meleeMinionScript.Stunned)
            {
                return State.Success;
            }
            else
            {

                return State.Failure;
            }
        }
        else
        {

            return State.Failure;
        }
    }


}
