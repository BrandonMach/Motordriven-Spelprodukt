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
    private float _duration, _startTime = 0;

    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        _meleeMinionScript.CurrentImpairement = Impairement.inAttack;

        _duration = _meleeMinionScript.Anim.GetCurrentAnimatorStateInfo(0).length;
        //_startTime = Time.time;

        //_meleeMinionScript.Anim.SetTrigger("LightAttack");

        //if (_meleeMinionScript.AIMovementScript.GoTowardsPlayer == true)
        //{
        //    _meleeMinionScript.AIMovementScript.GoTowardsPlayer = false;
        //}
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        _startTime += Time.fixedDeltaTime;
        _meleeMinionScript.AIMovementScript.GoTowardsPlayer = false;
        if (_startTime < _meleeMinionScript.AttackCooldown)
        {

            _meleeMinionScript.Anim.SetTrigger("Idle");
            return State.Running;
        }
        else 
        _meleeMinionScript.CurrentImpairement = Impairement.none;
        _startTime = 0;
        _meleeMinionScript.Anim.SetTrigger("LightAttack");

        //return State.Running;
        return State.Success;
    }
}

