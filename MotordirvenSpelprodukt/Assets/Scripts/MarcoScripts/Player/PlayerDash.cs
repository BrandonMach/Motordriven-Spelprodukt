using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public EventHandler EvadePerformed;

  
    [SerializeField] float _dashSpeed;
    [SerializeField] float _dashTime;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _cooldownTime;

    private Player _player;
    private Rigidbody _rigidBody;
    private PlayerMovement _playerMovement;


    private float _currentDashTime;
    private float _currentCooldownTime;
    private bool _isDashing;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _player = GetComponent<Player>();
        _rigidBody = GetComponent<Rigidbody>();

        _currentDashTime = _dashTime;
    }

    void Start()
    {
        _currentCooldownTime = 0;

        _player.OnStartEvade += Player_OnStartRoll;
    }



    void Update()
    {
        HandleTimers();
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

            if (_currentDashTime <= 0)
            {
                _currentDashTime = _dashTime;
                _rigidBody.velocity = Vector3.zero;
                EvadePerformed?.Invoke(this, EventArgs.Empty);
                _isDashing = false;
            }
            else
            {
                _rigidBody.velocity = _rigidBody.velocity.normalized * _dashSpeed;
            }
        }
    }

    private void HandleTimers()
    {
        if (_isDashing)
        {
            if (_currentDashTime > 0)
            {
                _currentDashTime -= Time.deltaTime;
            }
        }
        else
        {
            if (_currentCooldownTime > 0)
            {
                _currentCooldownTime -= Time.deltaTime;
            }
        }
    }

    private void Player_OnStartRoll(object sender, System.EventArgs e)
    {
        if (_currentCooldownTime <= 0)
        {
            _isDashing = true;
            _currentCooldownTime = _cooldownTime;
        }
  

    }

    public bool IsDashAvailable()
    {
        if (_currentCooldownTime <= 0)
        {
            return true;
        }
        return false;
    }
}
