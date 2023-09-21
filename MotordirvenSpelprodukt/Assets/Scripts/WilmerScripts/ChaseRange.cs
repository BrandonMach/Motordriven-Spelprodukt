using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseRange : ActionNode
{
    public float ChaseDistance;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (player != null && enemy != null)
        {

            float distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);
            if (distanceToPlayer > ChaseDistance)
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


