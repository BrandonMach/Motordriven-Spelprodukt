using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutOfCombat : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        //if (playerIsOutOfCombat)
        //{
        //    return State.Failure;
        //}
        //else
        //{
        //    return State.Success;
        //}
        //return State.Success;
        return State.Failure;
    }
}
