using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMChaseRange : ActionNode
{
    protected override void OnStart()
    {
        _kamikazeScript = _enemyObject.GetComponent<KMScript>();
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        float distanceToPlayer = Vector3.Distance(_kamikazeScript.transform.position, _playerScript.transform.position);
        if(_playerScript != null && _kamikazeScript != null && distanceToPlayer > _kamikazeScript.AttackRange)
        {

            if(distanceToPlayer > _kamikazeScript.ChaseDistance)
            {
                return State.Success;
            }
            else
            {
                return State.Running;
            }
        } 
        else
        {
            return State.Failure;
        }
    }
}
