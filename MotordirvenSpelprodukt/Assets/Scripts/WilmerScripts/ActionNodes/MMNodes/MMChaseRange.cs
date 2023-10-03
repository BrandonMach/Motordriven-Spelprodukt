using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMChaseRange : ActionNode
{
    //public float ChaseDistance;

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

            float distanceToPlayer = Vector3.Distance(_meleeMinionScript.transform.position, _playerScript.transform.position);
            if (distanceToPlayer > _meleeMinionScript.ChaseDistance)
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


