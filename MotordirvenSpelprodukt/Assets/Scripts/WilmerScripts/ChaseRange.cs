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
        if (playerScript != null && enemyScript != null)
        {

            float distanceToPlayer = Vector3.Distance(enemyScript.transform.position, playerScript.transform.position);
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


