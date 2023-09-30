using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNode
{
    public float duration;
    float startTime;

    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        startTime = Time.time;
        duration = _meleeMinionScript.StunDuration;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        
        if(Time.time - startTime > duration)
        {
            _meleeMinionScript.Stunned = false;
            return State.Success;
        }
        else return State.Running;
        
    }
}
