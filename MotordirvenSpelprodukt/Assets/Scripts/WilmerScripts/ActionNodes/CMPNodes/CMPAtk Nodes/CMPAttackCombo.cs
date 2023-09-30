using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPAttackCombo : ActionNode
{
    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();

        if (_championScript.CanAttack && _championScript.AttackIndex == 3)
        {
            _championScript.Anim.SetInteger("Attack Index", 3);

        }
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (_championScript.AttackIndex == 3)
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
