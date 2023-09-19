using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseNode : ActionNode
{
    private GameObject _player;
    public int MoveSpeed;
    private GameObject Enemy;

    protected override void OnStart()
    {
        _player = GameObject.FindWithTag("Player");
        Enemy = GameObject.FindWithTag("EnemyTesting");

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (_player != null && Enemy != null)
        {
            Vector3 distance = _player.transform.position - Enemy.transform.position;
            int distanceInt = (int)Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.z, 2));
            if (Mathf.Abs(distanceInt) > 10)
            {
                // Calculate the direction from AI to player
                Vector3 direction = _player.transform.position - Enemy.transform.position;

                // Normalize the direction to get a unit vector
                direction.Normalize();

                // Move the AI towards the player
                Enemy.transform.Translate(direction * MoveSpeed * Time.deltaTime);

                return State.Running;
            }
            else
            {

                return State.Success;
            }
        }
        else 
        {

            return State.Failure; 
        }
        
    }

    
}
