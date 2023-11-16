using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPBasicCombo : ActionNode
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
        if (_champion1Script.CurrentState == CMP1Script.ChampionState.None)
        {
            if (_champion1Script.PreviousState == CMP1Script.ChampionState.SpecialAttack || _champion1Script.PreviousState == CMP1Script.ChampionState.Taunt)
            {
                if (_champion1Script.DistanceToPlayer <= _champion1Script.AttackRange)
                {
                    _champion1Script.Anim.SetTrigger("BasicCombo1");
                    //_champion1Script.PreviousState = _champion1Script.CurrentState;
                    //_champion1Script.CurrentState = CMP1Script.ChampionState.BasicAttack;
                    return State.Success;
                }
                return State.Failure;
            } 
        }
        return State.Failure;
    }


}
