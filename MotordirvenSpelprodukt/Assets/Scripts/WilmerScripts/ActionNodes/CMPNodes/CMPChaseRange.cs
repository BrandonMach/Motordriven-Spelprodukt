using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPChaseRange : ActionNode
{
    protected override void OnStart()
    {
        _campionScript = _enemyObject.GetComponent<CMPScript>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if(_playerScript != null && _campionScript != null)
        {

            float distanceToPlayer = Vector3.Distance(_campionScript.transform.position, _playerScript.transform.position);
            if(distanceToPlayer > _campionScript.ChaseDistance)
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
