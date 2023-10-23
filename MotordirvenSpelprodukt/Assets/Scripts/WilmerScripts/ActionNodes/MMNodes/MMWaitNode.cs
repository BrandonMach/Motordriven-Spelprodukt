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
        _meleeMinionScript.Anim.SetTrigger("Idle");
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {

        while (!_meleeMinionScript.CanChase || !_meleeMinionScript.OnGround || _meleeMinionScript.Impaired)
        {
            _meleeMinionScript.OnGround = true;
            _meleeMinionScript.OnGround = true;
            _meleeMinionScript.Impaired = true;
            return State.Success;
        }
        //if (Time.time - startTime > _duration)
        //{
        //    _meleeMinionScript.CanChase = true;
        //    return State.Success;
        //}
        return State.Running;

    }
}
