using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCheck : ActionNode
{
    public float AttackRange = 20.0f;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (player != null)
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);

            // Check if the player is within attack range
            if (distanceToPlayer <= AttackRange)
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
