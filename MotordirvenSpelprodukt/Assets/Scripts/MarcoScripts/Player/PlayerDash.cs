using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerDash : MonoBehaviour
{
    public EventHandler EvadePerformed;

    public bool IsDashing { get; private set; }

    [SerializeField] float _speedMultiplier;
    [SerializeField] float _dashTime;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _cooldownTime;

    private Player _player;
    private Rigidbody _rigidBody;
    private PlayerMovement _playerMovement;
    private Vector3 _rollDirection;


    [SerializeField] private float _currentDashTime;
    private float _currentCooldownTime;

    [Header("For ability HUD cooldown")]
    bool _isOnCooldown;
    public float CooldownDuration { get => _cooldownTime; }
    public float DashCooldown { get => _currentCooldownTime; } 
    public bool IsOnCooldown { get => _isOnCooldown; }


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
        _player.StartEvade += Player_OnStartRoll;
    }



    void Update()
    {
        HandleTimers();
    }

    private void FixedUpdate()
    {
        if (IsDashing)
        {
            HandleRoll();
        }
    }

    private void HandleRoll()
    {

     
        _rigidBody.rotation = Quaternion.Slerp(_rigidBody.rotation, Quaternion.LookRotation(_rigidBody.velocity.normalized),_rotationSpeed * Time.fixedDeltaTime);

        if (_currentDashTime <= 0)
        {
            _currentDashTime = _dashTime;
            _rigidBody.velocity = Vector3.zero;
            EvadePerformed?.Invoke(this, EventArgs.Empty);
            IsDashing = false;
        }
        else
        {
            if (_rigidBody.velocity != Vector3.zero)
            {
                _rigidBody.velocity = _rigidBody.velocity.normalized * _playerMovement.MoveSpeed * _speedMultiplier;
            }
            else
            {
                _rigidBody.velocity = transform.forward * _playerMovement.MoveSpeed * _speedMultiplier;
            }

            if (_currentDashTime <= _dashTime / 2)
            {
                _player.SetPlayerInvulnarableState(false);
            }
        }
        
    }

    private void HandleTimers()
    {
        if (IsDashing)
        {
            if (_currentDashTime > 0)
            {
                _isOnCooldown = true;
                _currentDashTime -= Time.deltaTime;
            }
            else
            {
                _isOnCooldown = false;
            }
        }
        else
        {
            if (_currentCooldownTime > 0)
            {
                _isOnCooldown = true;
                _currentCooldownTime -= Time.deltaTime;
            }
            else
            {
                _isOnCooldown = false;
            }
        }
    }

    private void Player_OnStartRoll(object sender, System.EventArgs e)
    {
        if (_currentCooldownTime <= 0)
        {
            IsDashing = true;
            _player.SetPlayerInvulnarableState(true);
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
