using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMExplode : ActionNode
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
        if(Vector3.Distance(_kamikazeScript.transform.position, _playerScript.transform.position) <= _kamikazeScript.ChaseDistance)
        {

            Debug.Log("Kamikaze");
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
