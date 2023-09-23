using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseNode : ActionNode
{
    

    protected override void OnStart()
    {

        

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Vector3 direction = playerScript.transform.position - enemyScript.transform.position;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        // Move the AI towards the player
        enemyScript.transform.Translate(enemyScript.MovementSpeed  * Time.deltaTime * direction);
        
        return State.Success;

    }

    
}
