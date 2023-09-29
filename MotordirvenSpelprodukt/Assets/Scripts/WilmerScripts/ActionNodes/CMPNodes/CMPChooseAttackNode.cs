using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPChooseAttackNode : ActionNode
{
    int chooseAttack;
    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();
        chooseAttack = Random.Range(1, 3);

        _championScript.AttackIndex = Random.Range(1, 3);


        //switch (chooseAttack)
        //{
        //    case 0:
        //        _championScript.Anim.SetInteger("Attack Index", chooseAttack);
        //        break;

        //    case 1:
        //        _championScript.Anim.SetInteger("Attack Index", chooseAttack);
        //        break;

        //    case 2:
        //        _championScript.Anim.SetInteger("Attack Index", chooseAttack);
        //        break;

        //    case 3:
        //        _championScript.Anim.SetInteger("Attack Index", chooseAttack);
        //        break;
        //}


    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
