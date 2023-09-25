using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MMAttackNode : ActionNode
{
    // MeleeMinionAttackNode
    // Start is called before the first frame update
    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        // Check if enough time has passed since the last attack
        if (Time.time - _meleeMinionScript.LastAttackTime >= _meleeMinionScript.AttackCooldown)
        {
            // Perform the attack here
            // You can add your attack logic or call a method to attack the player
            _meleeMinionScript.LastAttackTime = Time.time;
            Debug.Log($"AAAAAAAAttack!");
            Debug.Log($"AAAAAAAAttack!");
            Debug.Log($"AAAAAAAAttack!");
            Debug.Log($"AAAAAAAAttack!");
            Debug.Log($"AAAAAAAAttack!");
            Debug.Log($"AAAAAAAAttack!");
            Debug.Log($"AAAAAAAAttack!");
            Debug.Log($"AAAAAAAAttack!");
            Debug.Log($"AAAAAAAAttack!");
            //Call TakeDamage() in player
            return State.Success; // Attack successful
        }
        else
        {
            return State.Failure; // Attack on cooldown
        }
    }
}

