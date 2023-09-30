using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPStopChase : ActionNode
{

    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();
        
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        //float distanceToPlayer = Vector3.Distance(_championScript.transform.position, _playerScript.transform.position);
        //if (distanceToPlayer < 2)
        //{
        //    return State.Failure;
        //}
        //else
        //{          
        //    return State.Success;
        //}
        return State.Success;
    }
}
