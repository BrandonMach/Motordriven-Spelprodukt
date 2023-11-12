using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPTaunt : ActionNode
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
        if(_champion1Script.CurrentState == CMP1Script.ChampionState.Enter || _champion1Script.CurrentState == CMP1Script.ChampionState.BasicAttack)
        {
            _champion1Script.PreviousState = _champion1Script.CurrentState;
            _champion1Script.CurrentState = CMP1Script.ChampionState.Taunt;
            return State.Success;
        }
        return State.Failure;
    }
}
