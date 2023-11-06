using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MMAlwaysUpdate : ActionNode
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
        //_meleeMinionScript.AttackCooldownTimer += Time.deltaTime;
        //if (_meleeMinionScript.CurrentImpairement != EnemyScript.Impairement.airborne)
        //{
        //    Vector3 direction = _playerScript.transform.position - _meleeMinionScript.transform.position;

        //    // Normalize the direction to get a unit vector
        //    direction.Normalize();

        //    //Rotate the Champion towards the players position

        //    _meleeMinionScript.transform.LookAt(_playerScript.transform);
        //    _meleeMinionScript.AIMovementScript.GoTowardsPlayer = true;
        //}
        //else _meleeMinionScript.AIMovementScript.GoTowardsPlayer = true;


        return State.Failure;
    }


}
