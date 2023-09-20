using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AttackNode : ActionNode
{




    

    public float AttackCooldown = 1.0f;
    private float _lastAttackTime;
    // Start is called before the first frame update
    protected override void OnStart()
    {


    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        // Check if enough time has passed since the last attack
        if (Time.time - _lastAttackTime >= AttackCooldown)
        {
            // Perform the attack here
            // You can add your attack logic or call a method to attack the player
            _lastAttackTime = Time.time;
            Debug.Log($"AAAAAAAAttack!");
            return State.Success; // Attack successful
        }
        else
        {
            return State.Failure; // Attack on cooldown
        }
    }
}

