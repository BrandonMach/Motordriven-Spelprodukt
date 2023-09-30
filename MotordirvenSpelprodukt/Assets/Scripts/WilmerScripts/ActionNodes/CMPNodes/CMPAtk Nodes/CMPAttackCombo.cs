using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPAttackCombo : ActionNode
{

    public float duration;
    float startTime;
    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();

        if (/*_championScript.CanAttack && */_championScript.AttackIndex == 3)
        {
            _championScript.Anim.SetInteger("Attack Index", 3);

        }
        startTime = 0;
        duration = _championScript.Anim.runtimeAnimatorController.animationClips.Length;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        //if (_championScript.AttackIndex == 3)
        //{
        //    return State.Success;
        //}
        //else
        //{
        //    return State.Failure;
        //}

        startTime += Time.deltaTime;

        if(_championScript.AttackIndex == 3)
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
