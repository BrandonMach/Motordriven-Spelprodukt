using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPAtkJumpSlam : ActionNode
{

    public float duration;
    float startTime;
    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();
        if (!_championScript.AnimationPlaying && _championScript.AttackIndex == 2)
        {
            _championScript.Anim.SetInteger("Attack Index", 2);
            
        }

        startTime = 0;
        duration = _championScript.Anim.runtimeAnimatorController.animationClips.Length;

    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {
        //if (_championScript.AttackIndex == 2)
        //{
        //    //_championScript.CanAttack = false;
        //    return State.Success;
        //}
        //else
        //{
        //    return State.Failure;
        //}

        if(_championScript.AttackIndex == 2)
        {
            if (startTime >= duration)
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
