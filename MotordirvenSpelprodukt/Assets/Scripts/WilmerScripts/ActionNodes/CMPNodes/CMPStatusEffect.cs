using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPStatusEffect : ActionNode
{
    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (_playerScript != null && _championScript != null)
        {

            if (!_championScript.CanChase)
            {
                return State.Success;
            }
            else
            {

                return State.Failure;
            }
        }
        else
        {

            return State.Failure;
        }
    }
}
