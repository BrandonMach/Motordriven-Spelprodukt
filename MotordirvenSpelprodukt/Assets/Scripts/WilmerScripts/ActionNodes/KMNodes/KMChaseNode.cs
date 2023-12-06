using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMChaseNode : ActionNode
{

    public float RotationSpeed = 5;
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
        //Vector3 direction = _playerScript.transform.position - _kamikazeScript.transform.position;
        
        ////Normalize the direction to get a unit vector
        //direction.Normalize();

        ////Rotate the Kamikaze unit towards the players position
        //_kamikazeScript.transform.rotation = Quaternion.Lerp(_kamikazeScript.transform.rotation, Quaternion.LookRotation(direction), RotationSpeed * Time.deltaTime);

        //_kamikazeScript.transform.position += _kamikazeScript.transform.forward * _kamikazeScript.MovementSpeed * Time.deltaTime;

        ////_kamikazeScript.transform.position = Vector3.Lerp(_kamikazeScript.transform.position, _playerScript.transform.position, Time.deltaTime);
        ////_kamikazeScript.transform.forward * _kamikazeScript.MovementSpeed * Time.deltaTime;

        //Debug.Log("Chasing player");
       
        return State.Success;
    }
}
