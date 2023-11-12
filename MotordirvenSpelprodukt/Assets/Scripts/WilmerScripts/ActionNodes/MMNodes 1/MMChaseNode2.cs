using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMChaseNode2 : ActionNode
{
    public float lerpSpeed = 200.0f;

    protected override void OnStart()
    {
        _meleeMinionScript = _enemyObject.GetComponent<MMScript>();
        _playerScript = Player.Instance;

        //_meleeMinionScript.ShouldMove = true;

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        // Should always chase if attack or taunt fails.

            if (_meleeMinionScript.CurrentState != MinionScript.EnemyState.chasing)
            {
                _meleeMinionScript.PreviousState = _meleeMinionScript.CurrentState;
            }
            _meleeMinionScript.CurrentState = MinionScript.EnemyState.chasing;
            //Debug.Log("Enemy is Chasing");
            return State.Success; 

    }


    //protected override State OnUpdate()
    //{
    //    //Vector3 direction = _playerScript.transform.position - _meleeMinionScript.transform.position;


    //    if (_meleeMinionScript.currentState == EnemyScript.EnemyState.none)
    //    {
    //        _meleeMinionScript.ResetTriggers();
    //        _meleeMinionScript.Anim.SetTrigger("Walking");

    //        if (_meleeMinionScript.ShouldMove)
    //        {
    //            _meleeMinionScript.RB.velocity = _meleeMinionScript.transform.forward * _meleeMinionScript.MovementSpeed;
    //            _meleeMinionScript.RB.AddForce(Vector3.down * _meleeMinionScript.RB.mass * 9.81f, ForceMode.Force);


    //            //Vector3 direction = transform.position - transform.position;
    //            Vector3 direction = _playerScript.transform.position - _meleeMinionScript.transform.position;
    //            direction.y = 0;
    //            // Normalize the direction to get a unit vector
    //            direction.Normalize();
    //            //Rotate the Champion towards the players position
    //            //_meleeMinionScript.transform.LookAt(Player.Instance.transform);


    //            Quaternion targetRot = Quaternion.LookRotation(direction);
    //            _meleeMinionScript.transform.rotation = Quaternion.Slerp(_meleeMinionScript.transform.rotation, targetRot, Time.deltaTime * 1);



    //            //_meleeMinionScript.RB.Move(_meleeMinionScript.MovementSpeed * Time.deltaTime * _meleeMinionScript.transform.forward, Quaternion.identity);
    //            //Vector3 direction = _meleeMinionScript.transform.forward * 1f;//_meleeMinionScript.MovementSpeed;
    //            //_meleeMinionScript.CharacterController.Move(direction * Time.deltaTime);
    //        }
    //    }


    //    //// Normalize the direction to get a unit vector
    //    //direction.Normalize();

    //    ////Rotate the Champion towards the players position

    //    //_meleeMinionScript.transform.LookAt(_playerScript.transform);



    //    // Move the AI forward, since the AI is facing towards the player
    //    //_meleeMinionScript.transform.position += _meleeMinionScript.transform.forward * _meleeMinionScript.MovementSpeed * Time.deltaTime;


    //    //_meleeMinionScript.Rigidbody.velocity = new Vector3(_meleeMinionScript.transform.forward.x * _meleeMinionScript.MovementSpeed, _meleeMinionScript.Rigidbody.velocity.y, _meleeMinionScript.transform.forward.z * _meleeMinionScript.MovementSpeed);
    //    //_meleeMinionScript.Rigidbody.Move =  _meleeMinionScript.Rigidbody.velocity + _meleeMinionScript.transform.forward * _meleeMinionScript.MovementSpeed * Time.deltaTime;



    //    return State.Success;
    //}


}
