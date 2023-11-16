using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyScript;

public class MMAttackCheck2 : ActionNode
{

    
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
        //if (_playerScript != null)
        //{
        // Calculate the distance between the enemy and the player
        //float distanceToPlayer = Vector3.Distance(_meleeMinionScript.transform.position, _playerScript.transform.position);
        
        //_meleeMinionScript.distanceToPlayer = distanceToPlayer;
        // Check if the player is within attack range
        if (_meleeMinionScript.DistanceToPlayer <= _meleeMinionScript.AttackRange
            && _meleeMinionScript.CurrentState == MinionScript.EnemyState.none
            && _meleeMinionScript.CurrentState != MinionScript.EnemyState.airborne
            && _meleeMinionScript.OnGround)
        {
            _meleeMinionScript.CanMove = false;
            return State.Failure;
        }
        else
        {
            _meleeMinionScript.CanMove = true;
            return State.Success; // Player is out of attack range
        }
        //}
        //else
        //{
        //    return State.Success; // Player not found
        //}
    }

}
