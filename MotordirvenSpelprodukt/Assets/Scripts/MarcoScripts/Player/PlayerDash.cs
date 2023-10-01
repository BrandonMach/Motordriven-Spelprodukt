using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public EventHandler EvadePerformed;

    private Player _player;
    private Rigidbody _rigidBody;
    private PlayerMovement _playerMovement;

    [SerializeField] float _dashSpeed;
    [SerializeField] float _dashTime;
    [SerializeField] float _startDashTime;
    [SerializeField] float _rotationSpeed;

    private bool _isDashing;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _player = GetComponent<Player>();
        _rigidBody = GetComponent<Rigidbody>();

        _dashTime = _startDashTime;
    }

    void Start()
    {
        _player.OnStartEvade += Player_OnStartRoll;
    }



    void Update()
    {
     
    }

    private void FixedUpdate()
    {
        HandleRoll();
    }

    private void HandleRoll()
    {
        if (_isDashing)
        {
            _rigidBody.rotation = Quaternion.Slerp(_rigidBody.rotation, Quaternion.LookRotation(_rigidBody.velocity.normalized),_rotationSpeed * Time.fixedDeltaTime);

            if (_dashTime <= 0)
            {
                _dashTime = _startDashTime;
                _rigidBody.velocity = Vector3.zero;
                EvadePerformed?.Invoke(this, EventArgs.Empty);
                _isDashing = false;
            }
            else
            {
                _dashTime -= Time.deltaTime;
                _rigidBody.velocity = _rigidBody.velocity.normalized * _dashSpeed;
            }
        }
   
    }

    private void Player_OnStartRoll(object sender, System.EventArgs e)
    {
        _isDashing = true;

    }
}
