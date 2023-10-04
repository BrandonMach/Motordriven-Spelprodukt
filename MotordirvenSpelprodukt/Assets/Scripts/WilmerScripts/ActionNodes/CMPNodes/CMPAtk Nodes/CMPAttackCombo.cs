using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPAttackCombo : ActionNode
{

    
    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();
        float NTime = _championScript.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        _championScript.Damage = 15; //Set specific attack damage
        _championScript.ETPDecreaseValue = 10;
        if (/*!_championScript.AnimationPlaying &&*/ _championScript.AttackIndex == 3 && NTime > 1.0f)
        {
            _championScript.AnimationPlaying = true;
            //_championScript.Anim.SetInteger("Attack Index", 1);
            _championScript.Anim.Play("Attack3");

        }
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (_championScript.AttackIndex == 3)
        {

            float NTime = _championScript.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (NTime > 1.0f)
            {

                // _championScript.AnimationPlaying = false;
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
