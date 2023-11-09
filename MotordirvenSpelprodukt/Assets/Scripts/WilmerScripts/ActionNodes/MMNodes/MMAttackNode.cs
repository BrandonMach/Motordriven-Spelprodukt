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

    
    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {


        //if (_meleeMinionScript.TimeSinceLastAttack < _meleeMinionScript.TimeBetweenAttacks)
        //{
        //    _meleeMinionScript.Anim.SetTrigger("Idle");
        //    //_meleeMinionScript.CurrentImpairement = Impairement.none;
        //    Debug.Log("In none");
        //    return State.Failure;
        //}
        //else
        //{
        //if (_meleeMinionScript.CurrentImpairement == Impairement.none)
        //{
            _meleeMinionScript.CurrentImpairement = Impairement.inAttack;
            int randomValue = Mathf.FloorToInt(Random.Range(1.0f, 3.0f));
            //_meleeMinionScript.TimeSinceLastAttack = 0;
            _meleeMinionScript.ResetTriggers();

            if (randomValue == 1)
            {
                _meleeMinionScript.Anim.SetTrigger("LightAttack");
            }
            else
            {
                _meleeMinionScript.Anim.SetTrigger("HeavyAttack");
            }
        //}
        //else if (_meleeMinionScript.CurrentImpairement == Impairement.inAttack) _meleeMinionScript.CurrentImpairement = Impairement.none;

        return State.Success;
    }
}

