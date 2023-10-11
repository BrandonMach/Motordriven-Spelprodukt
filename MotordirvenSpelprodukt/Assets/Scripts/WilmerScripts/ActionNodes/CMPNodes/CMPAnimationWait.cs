using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPAnimationWait : ActionNode
{
    public float duration;
    float startTime;

    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();
        startTime = Time.time;
        duration = _championScript.Anim.GetCurrentAnimatorStateInfo(0).length;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {

        if (Time.time - startTime > duration)
        {
            _championScript.CanChase = true;
            return State.Success;
        }
        else return State.Running;

    }
}
