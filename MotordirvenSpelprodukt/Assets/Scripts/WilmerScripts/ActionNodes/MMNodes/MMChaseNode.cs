using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MMChaseNode : ActionNode
{
    

    protected override void OnStart()
    {
        meleeMinionScript = enemyObject.GetComponent<MMScript>();


    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Vector3 direction = playerScript.transform.position - meleeMinionScript.transform.position;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        // Move the AI towards the player
        meleeMinionScript.transform.Translate(meleeMinionScript.MovementSpeed  * Time.deltaTime * direction);
        
        return State.Success;

    }

    
}
