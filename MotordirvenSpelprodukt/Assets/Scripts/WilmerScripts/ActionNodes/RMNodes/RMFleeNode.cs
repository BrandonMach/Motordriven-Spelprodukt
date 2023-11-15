using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMFleeNode : ActionNode
{
    // Start is called before the first frame update
    protected override void OnStart()
    {
        _rangedMinionScript = _enemyObject.GetComponent<RMScript>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        // If player is close enough then flee
        if (_rangedMinionScript.DistanceToPlayer < _rangedMinionScript.StartFleeRange)
        {
            if (_rangedMinionScript.CurrentState != MinionScript.EnemyState.fleeing)
            {
                _rangedMinionScript.PreviousState = _rangedMinionScript.CurrentState;
            }
            _rangedMinionScript.CurrentState = MinionScript.EnemyState.fleeing;
            //Debug.Log("Enemy is in fleeing");
            return State.Success;
        }
        //Debug.Log("Enemy is NOT in fleeing");
        return State.Failure;
    }
}
