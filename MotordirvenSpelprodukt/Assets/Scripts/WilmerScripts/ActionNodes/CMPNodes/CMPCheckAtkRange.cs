using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPCheckAtkRange : ActionNode
{
    Vector3 lockposition;
    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();
        lockposition = _championScript.transform.position;
    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {
        if(_playerScript != null /*&& _championScript.CanAttack*/)
        {
            float distanceToPlayer = Vector3.Distance(_championScript.transform.position, _playerScript.transform.position);
            Vector3 direction = _playerScript.transform.position - _championScript.transform.position;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            //Rotate the Champion towards the players position
            _championScript.transform.rotation = Quaternion.Slerp(_championScript.transform.rotation, Quaternion.LookRotation(direction), 5 * Time.deltaTime);


            //if (distanceToPlayer <= _championScript.AttackRange)
            //{
            //    return State.Success;
            //}
            //else
            //{
            //    return State.Failure; //Player is not in attack range
            //}
        }
        //else
        //{
        //    return State.Success; //no player is found
        //}
        return State.Success;
    }
}
