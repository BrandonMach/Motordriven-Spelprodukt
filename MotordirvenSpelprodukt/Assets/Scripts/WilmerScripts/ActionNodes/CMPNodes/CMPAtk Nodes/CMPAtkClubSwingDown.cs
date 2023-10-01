using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPAtkClubSwingDown : ActionNode
{

    protected override void OnStart()
    {
        
        _championScript = _enemyObject.GetComponent<CMPScript>();
        float NTime = _championScript.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        _championScript.Damage = 20;

        if (/*!_championScript.AnimationPlaying &&*/ _championScript.AttackIndex == 1 && NTime > 1.0f)
        {   
            _championScript.AnimationPlaying = true;
            //_championScript.Anim.SetInteger("Attack Index", 1);
            _championScript.Anim.Play("Attack1");
            
        }
        

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (_championScript.AttackIndex == 1)
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
