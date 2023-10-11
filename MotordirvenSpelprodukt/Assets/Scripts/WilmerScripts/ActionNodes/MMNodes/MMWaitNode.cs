using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMWaitNode : ActionNode
{
    private float _duration;
    float startTime;

    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        startTime = Time.time;
        _duration = _meleeMinionScript.Anim.GetCurrentAnimatorStateInfo(0).length;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {

        if (Time.time - startTime > _duration)
        {
            _meleeMinionScript.CanChase = true;
            return State.Success;
        }
        else return State.Running;

    }
}
