using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMTaunt : ActionNode
{
    // MeleeMinionTaunt
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

        // Taunt 
        //_meleeMinionScript.AIMovementScript.GoTowardsPlayer = false;
        //_meleeMinionScript.Anim.SetTrigger("Idle");
        return State.Success;
    }
}
