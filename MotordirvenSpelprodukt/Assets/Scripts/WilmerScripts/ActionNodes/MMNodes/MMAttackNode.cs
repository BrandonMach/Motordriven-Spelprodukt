//#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static EnemyScript;
#if UNITY_EDITOR
using static UnityEditor.Experimental.GraphView.GraphView;
#endif

public class MMAttackNode : ActionNode
{
    // MeleeMinionAttackNode
    // Start is called before the first frame update
    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        _meleeMinionScript.CurrentImpairement = Impairement.inAttack;

        _meleeMinionScript.Anim.SetTrigger("LightAttack");

        if (_meleeMinionScript.AIMovementScript.GoTowardsPlayer == true)
        {
            _meleeMinionScript.AIMovementScript.GoTowardsPlayer = false;
        }
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}

