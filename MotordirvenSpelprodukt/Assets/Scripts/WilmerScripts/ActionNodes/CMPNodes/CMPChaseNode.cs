using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPChaseNode : ActionNode
{
    protected override void OnStart()
    {
        _campionScript = _enemyObject.GetComponent<CMPScript>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Vector3 direction = _playerScript.transform.position - _campionScript.transform.position;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        // Move the AI towards the player
        _campionScript.transform.Translate(_campionScript.MovementSpeed * Time.deltaTime * direction);

        return State.Success;
    }
}
