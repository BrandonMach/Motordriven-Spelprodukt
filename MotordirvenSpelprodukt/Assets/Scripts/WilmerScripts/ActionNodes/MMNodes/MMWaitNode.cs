using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMWaitNode : ActionNode
{
    private float _duration;
    float startTime = 0;

    protected override void OnStart()   // Kallas varje g�ng noden n�s
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        
        _duration = _meleeMinionScript.Anim.GetCurrentAnimatorStateInfo(0).length;

        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        //startTime += Time.fixedDeltaTime;



        //switch (_meleeMinionScript.CurrentState)
        //{

        //    case EnemyScript.EnemyState.stunned:
        //        _duration = _meleeMinionScript.StunDuration;
        //        //_meleeMinionScript.Anim.SetTrigger("Idle");
        //        return State.Success;

        //    case EnemyScript.EnemyState.pushed:
        //        //_meleeMinionScript.Anim.SetTrigger("Idle");
        //        //_meleeMinionScript.Anim.SetTrigger("PushedBack");
        //        return State.Success;

        //    case EnemyScript.EnemyState.airborne:
        //        //_meleeMinionScript.Anim.SetTrigger("Idle");
        //        return State.Success;
        //    case EnemyScript.EnemyState.inAttack:
        //        //_meleeMinionScript.Anim.SetTrigger("Idle");
        //        return State.Success;
        //}

        //if (_meleeMinionScript.OnGround)
        //{
        //    return State.Success;
        //}
        return State.Failure;
    }
}
