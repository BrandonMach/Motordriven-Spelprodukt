using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MMChaseNode : ActionNode
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
        Vector3 direction = _playerScript.transform.position - _meleeMinionScript.transform.position;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        // Move the AI towards the player
        _meleeMinionScript.transform.Translate(_meleeMinionScript.MovementSpeed  * Time.deltaTime * direction);
        
        return State.Success;

    }

    
}
