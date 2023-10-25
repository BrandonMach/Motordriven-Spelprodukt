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

        if (_meleeMinionScript.CurrentImpairement != EnemyScript.Impairement.none)
        {
            return State.Success;
        }
        return State.Failure;

        if (_playerScript != null && _meleeMinionScript != null)
        {
            if(_meleeMinionScript.CurrentImpairement != EnemyScript.Impairement.none)
            {
                return State.Success;
            }
            //if (!_meleeMinionScript.CanChase || _meleeMinionScript.Impaired || !_meleeMinionScript.OnGround)
            //{
            //    return State.Success;
            //}
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
