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

        switch (_meleeMinionScript.CurrentImpairement)
        {

            case EnemyScript.Impairement.stunned:
                _duration = _meleeMinionScript.StunDuration;
                _meleeMinionScript.Anim.SetTrigger("Idle");
                break;

            case EnemyScript.Impairement.airborne:
                return State.Success;
                break;
            case EnemyScript.Impairement.inAttack:
                //return State.Failure;
                break;
            case EnemyScript.Impairement.pushed:
                _meleeMinionScript.Anim.SetTrigger("Idle");
                break;
            default:
                break;
        }

        //if (!_meleeMinionScript.CanChase || !_meleeMinionScript.OnGround || _meleeMinionScript.Impaired)
        //{

        //    return State.Running;
        //}

        //if (Time.time - startTime > _meleeMinionScript.StunDuration)
        //{
        //    _meleeMinionScript.CanChase = true;
        //    return State.Success;
        //}
        while (Time.time - startTime < _duration)
        {

            return State.Running;

        }
        _meleeMinionScript.CurrentImpairement = EnemyScript.Impairement.none;
        //return State.Success;
        return State.Failure;

    }
}
