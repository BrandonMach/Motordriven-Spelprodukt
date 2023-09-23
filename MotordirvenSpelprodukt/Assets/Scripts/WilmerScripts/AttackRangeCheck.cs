using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCheck : ActionNode
{

    
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (playerScript != null)
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(enemyScript.transform.position, playerScript.transform.position);

            // Check if the player is within attack range
            if (distanceToPlayer <= 5/*minionScript.AttackRange*/)
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
