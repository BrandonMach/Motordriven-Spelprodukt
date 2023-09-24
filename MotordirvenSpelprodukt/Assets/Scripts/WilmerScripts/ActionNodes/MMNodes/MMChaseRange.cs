using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMChaseRange : ActionNode
{
    //public float ChaseDistance;

    protected override void OnStart()
    {
        meleeMinionScript = enemyObject.GetComponent<MMScript>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (playerScript != null && meleeMinionScript != null)
        {

            float distanceToPlayer = Vector3.Distance(meleeMinionScript.transform.position, playerScript.transform.position);
            if (distanceToPlayer > meleeMinionScript.ChaseDistance)
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


