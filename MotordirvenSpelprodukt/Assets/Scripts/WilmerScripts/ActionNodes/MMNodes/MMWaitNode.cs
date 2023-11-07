using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMWaitNode : ActionNode
{
    private float _duration;
    float startTime = 0;

    protected override void OnStart()   // Kallas varje gång noden nås
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        
        _duration = _meleeMinionScript.Anim.GetCurrentAnimatorStateInfo(0).length;

        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {

        startTime += Time.fixedDeltaTime;

        switch (_meleeMinionScript.CurrentImpairement)
        {

            case EnemyScript.Impairement.stunned:
                _duration = _meleeMinionScript.StunDuration;
                //_meleeMinionScript.Anim.SetTrigger("Idle");
                return State.Success;

            case EnemyScript.Impairement.pushed:
                //_meleeMinionScript.Anim.SetTrigger("Idle");
                
                return State.Success;

            case EnemyScript.Impairement.airborne:
                //_meleeMinionScript.Anim.SetTrigger("Idle");
                return State.Success;
            case EnemyScript.Impairement.inAttack:
                //_meleeMinionScript.Anim.SetTrigger("Idle");
                return State.Success;
        }

        return State.Failure;


    }
}
