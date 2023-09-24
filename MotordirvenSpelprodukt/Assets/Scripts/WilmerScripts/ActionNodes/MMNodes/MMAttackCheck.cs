using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMAttackCheck : ActionNode
{

    
    protected override void OnStart()
    {
        meleeMinionScript = enemyObject.GetComponent<MMScript>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (playerScript != null)
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(meleeMinionScript.transform.position, playerScript.transform.position);

            // Check if the player is within attack range
            if (distanceToPlayer <= meleeMinionScript.AttackRange)
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
