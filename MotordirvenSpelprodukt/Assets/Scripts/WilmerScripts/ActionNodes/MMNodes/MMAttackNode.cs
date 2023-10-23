//#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
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
        

        if (_meleeMinionScript.NTime > 1.0f)
        {
            _meleeMinionScript.AnimationPlaying = true;
            //_championScript.Anim.SetInteger("Attack Index", 1);
            _meleeMinionScript.Anim.SetTrigger("LightAttack");
            _meleeMinionScript.CanChase = false;

        }


    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {

        if (_meleeMinionScript.NTime > 1.0f)
        {

            // _championScript.AnimationPlaying = false;
            return State.Success;
        }
        else
        {
            return State.Running;
        }

        // Check if enough time has passed since the last attack
        //if (Time.time - _meleeMinionScript.LastAttackTime >= _meleeMinionScript.AttackCooldown)
        //{
        //    // Perform the attack here
        //    // You can add your attack logic or call a method to attack the player
        //    _meleeMinionScript.LastAttackTime = Time.time;


        //    //Call TakeDamage() in player
        //    return State.Success; // Attack successful
        //}
        //else
        //{
        //    return State.Failure; // Attack on cooldown
        //}
    }
}
//#endif
