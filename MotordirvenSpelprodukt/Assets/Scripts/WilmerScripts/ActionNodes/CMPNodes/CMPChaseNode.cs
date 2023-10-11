using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPChaseNode : ActionNode
{
    protected override void OnStart()
    {
        _championScript = _enemyObject.GetComponent<CMPScript>();
        //_championScript.Anim.SetTrigger("Walking");
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Vector3 direction = _playerScript.transform.position - _championScript.transform.position;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        //Rotate the Champion towards the players position
        _championScript.transform.rotation = Quaternion.Slerp(_championScript.transform.rotation, Quaternion.LookRotation(direction), 5*Time.deltaTime);


        // Move the AI forward, since the AI is facing towards the player
        _championScript.transform.position += _championScript.transform.forward * _championScript.MovementSpeed * Time.deltaTime;

       


        return State.Success;
    }
}
