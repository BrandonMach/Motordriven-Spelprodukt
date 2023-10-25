using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MMChaseNode : ActionNode
{
    

    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        _meleeMinionScript.Anim.SetTrigger("Walking");

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        Vector3 direction = _playerScript.transform.position - _meleeMinionScript.transform.position;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        //Rotate the Champion towards the players position

        _meleeMinionScript.transform.LookAt(_playerScript.transform);


        // Move the AI forward, since the AI is facing towards the player
        _meleeMinionScript.transform.position += _meleeMinionScript.transform.forward * _meleeMinionScript.MovementSpeed * Time.deltaTime;

        return State.Success;

    }

    
}
