using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMAutomaticExplode : ActionNode
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
        if (Vector3.Distance(_kamikazeScript.transform.position, _playerScript.transform.position) <= _kamikazeScript._automaticExplodeRange && !_kamikazeScript._maunalExplodeActive)
        {
            _kamikazeScript._maunalExplodeActive = true;
            Debug.Log("Automatic explode is active");
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
