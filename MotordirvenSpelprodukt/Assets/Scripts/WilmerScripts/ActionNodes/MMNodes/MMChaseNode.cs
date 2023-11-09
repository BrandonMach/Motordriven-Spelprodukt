using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMChaseNode : ActionNode
{
    public float lerpSpeed = 200.0f;

    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        _playerScript = Player.Instance;

        _meleeMinionScript.ShouldMove = true;

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        //Vector3 direction = _playerScript.transform.position - _meleeMinionScript.transform.position;
       

        if (_meleeMinionScript.CurrentImpairement == EnemyScript.Impairement.none)
        {
            _meleeMinionScript.ResetTriggers();
            _meleeMinionScript.Anim.SetTrigger("Walking");

            if (_meleeMinionScript.ShouldMove)
            {
                _meleeMinionScript.RB.velocity = _meleeMinionScript.transform.forward * _meleeMinionScript.MovementSpeed;
                //_meleeMinionScript.RB.Move(_meleeMinionScript.MovementSpeed * Time.deltaTime * _meleeMinionScript.transform.forward, Quaternion.identity);
                //Vector3 direction = _meleeMinionScript.transform.forward * 1f;//_meleeMinionScript.MovementSpeed;
                //_meleeMinionScript.CharacterController.Move(direction * Time.deltaTime);
            }
        }

        
        //// Normalize the direction to get a unit vector
        //direction.Normalize();

        ////Rotate the Champion towards the players position

        //_meleeMinionScript.transform.LookAt(_playerScript.transform);



        // Move the AI forward, since the AI is facing towards the player
        //_meleeMinionScript.transform.position += _meleeMinionScript.transform.forward * _meleeMinionScript.MovementSpeed * Time.deltaTime;


        //_meleeMinionScript.Rigidbody.velocity = new Vector3(_meleeMinionScript.transform.forward.x * _meleeMinionScript.MovementSpeed, _meleeMinionScript.Rigidbody.velocity.y, _meleeMinionScript.transform.forward.z * _meleeMinionScript.MovementSpeed);
        //_meleeMinionScript.Rigidbody.Move =  _meleeMinionScript.Rigidbody.velocity + _meleeMinionScript.transform.forward * _meleeMinionScript.MovementSpeed * Time.deltaTime;



        return State.Success;
    }

    
}
