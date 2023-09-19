using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AttackNode : ActionNode
{




    public GameObject PlayerCharacter;
    public float AttackRange = 20.0f;
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
        if (player != null)
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);

            // Check if the player is within attack range
            if (distanceToPlayer <= AttackRange)
            {
                // Check if enough time has passed since the last attack
                if (Time.time - _lastAttackTime >= AttackCooldown)
                {
                    // Perform the attack here
                    // You can add your attack logic or call a method to attack the player
                    _lastAttackTime = Time.time;

                    return State.Success; // Attack successful
                }
                else
                {
                    return State.Failure; // Attack on cooldown
                }
            }
            else
            {
                return State.Failure; // Player is out of attack range
            }
        }
        else
        {
            return State.Failure; // Player not found
        }
    }
}

