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
                _meleeMinionScript.Anim.SetTrigger("Idle");
                break;

            case EnemyScript.Impairement.airborne:
                
                return State.Success;
                break;
            case EnemyScript.Impairement.inAttack:
                if (startTime > 2.0f/*_duration*/)
                {

                    //return State.Running;
                    startTime = 0;
                    return State.Failure;

                }
                return State.Success;
                break;
            case EnemyScript.Impairement.pushed:
                _meleeMinionScript.Anim.SetTrigger("Idle");
                break;
            //default:
            //    break;
        }


        if (startTime < 2.0f/*_duration*/)
        {

            //return State.Running;
            startTime = Time.time;
            return State.Failure;

        }
        else _meleeMinionScript.CurrentImpairement = EnemyScript.Impairement.none;
        //return State.Success;
        return State.Success;

    }
}
