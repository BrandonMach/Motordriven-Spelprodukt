using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPAtkJumpSlam : ActionNode
{

    
    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();
        
        _championScript.Damage = 40; //Set specific attack damage
        _championScript.ETPDecreaseValue = 10;
        if (/*!_championScript.AnimationPlaying &&*/ _championScript.AttackIndex == 2 && _championScript.NTime > 1.0f)
        {
            _championScript.AnimationPlaying = true;
            _championScript.Anim.Play("Attack2");
            _championScript.CanChase = false;

        }

    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {
        if (_championScript.AttackIndex == 2)
        {

            
            if (_championScript.NTime > 1.0f)
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
