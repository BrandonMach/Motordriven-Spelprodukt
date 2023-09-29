using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPAtkClubSwingDown : ActionNode
{
    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();

        if (_championScript.CanAttack && _championScript.AttackIndex == 1)
        {
            _championScript.Anim.SetInteger("Attack Index", 1);
            
        }
        

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (_championScript.AttackIndex == 1)
        {
            _championScript.CanAttack = false;
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
