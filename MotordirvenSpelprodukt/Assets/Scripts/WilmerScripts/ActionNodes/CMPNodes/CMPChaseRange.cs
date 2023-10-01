using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPChaseRange : ActionNode
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
        if (_playerScript != null && _championScript != null)
        {

            float distanceToPlayer = Vector3.Distance(_championScript.transform.position, _playerScript.transform.position);
            if (distanceToPlayer > _championScript.ChaseDistance)
            {
                return State.Success;
            }
            else
            {

                return State.Running;
            }
        }
        else
        {

            return State.Failure;
        }
    }
  
    
}