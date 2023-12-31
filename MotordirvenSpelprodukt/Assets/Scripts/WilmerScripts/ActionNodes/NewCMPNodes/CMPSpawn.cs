using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPSpawn : ActionNode
{
    protected override void OnStart()
    {
        _champion1Script = _enemyObject.GetComponent<CMP1Script>();
        _playerScript = Player.Instance;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (_champion1Script.CurrentState == CMP1Script.ChampionState.Enter )
        {

            return State.Failure;
        }
        return State.Success;
    }
}
