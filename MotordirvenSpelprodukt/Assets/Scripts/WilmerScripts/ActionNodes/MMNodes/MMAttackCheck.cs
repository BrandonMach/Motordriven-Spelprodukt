using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMAttackCheck : ActionNode
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
        if (_playerScript != null)
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(_meleeMinionScript.transform.position, _playerScript.transform.position);

            // Check if the player is within attack range
            if (distanceToPlayer <= _meleeMinionScript.AttackRange)
            {
                return State.Success;
            }
            else
            {
                return State.Failure; // Player is out of attack range
            }
        }
        else
        {
            return State.Failure; // Player not found
        }
    }

}
