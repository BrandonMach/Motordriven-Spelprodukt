using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMStartManualExplode : ActionNode
{
    protected override void OnStart()
    {
        _kamikazeScript = _enemyObject.GetComponent<KMScript>();
        _playerScript = Player.Instance;


    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (Vector3.Distance(_kamikazeScript.transform.position, _playerScript.transform.position) <= _kamikazeScript._activateManualExplodeRange)
        {
            _kamikazeScript.Anim.Play("Dive");
            Debug.Log("Kamikaze");
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
