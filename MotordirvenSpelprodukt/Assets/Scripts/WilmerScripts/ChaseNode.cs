using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseNode : ActionNode
{

    public int MoveSpeed;
    

    protected override void OnStart()
    {

        

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Vector3 direction = player.transform.position - enemy.transform.position;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        // Move the AI towards the player
        enemy.transform.Translate(MoveSpeed * Time.deltaTime * direction);

        return State.Success;

    }

    
}