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
            Vector3 distance = player.transform.position - enemy.transform.position;
            int distanceInt = (int)Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.z, 2));
            if (Mathf.Abs(distanceInt) > ChaseDistance)
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


