using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMChaseRange : ActionNode
{
    //public float ChaseDistance;

    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        _playerScript = Player.Instance;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        float distanceToPlayer = Vector3.Distance(_meleeMinionScript.transform.position, _playerScript.transform.position);
        if (_playerScript != null && _meleeMinionScript != null && distanceToPlayer > _meleeMinionScript.AttackRange)
        {

            
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


