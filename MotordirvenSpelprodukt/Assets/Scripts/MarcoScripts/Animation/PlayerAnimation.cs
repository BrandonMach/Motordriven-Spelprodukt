using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public float _timeBetweenInputs = 0f;

    [SerializeField] private Player _player;

    [Header("Combo Sequence")]
    [SerializeField] private bool _startComboWindowTimer;


    private float _comboWindowTimer = 0;
    private string _currentCombo = "";
    private float _desiredWeight = 0;
    private float _weight = 0;
    private float _weightChanger = -0.025f;
    
    private Animator _animator;

    private bool _wasMoving = false;



    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        _player = GetComponent<Player>();
        _player.OnAttackPressed += Player_OnAttack;
        _player.OnStartEvade += Player_OnStartEvade;
        _player.OnEnableMovement += Player_OnEnableMovement;
        _player.ComboBroken += Player_OnComboBroken;
    }

    private void Player_OnComboBroken(object sender, EventArgs e)
    {
        EndCombo();
    }

    private void Player_OnEnableMovement(object sender, EventArgs e)
    {
        //_animator.SetTrigger("StopEvade");

    }

    private void Player_OnStartEvade(object sender, EventArgs e)
    {
        _animator.SetTrigger("Evade");
    }

    private void Player_OnAttack(object sender, Player.OnAttackPressedEventArgs e)
    {
        _startComboWindowTimer = true;
        HandleInput(e.attackType);
        _comboWindowTimer = 0;
    }

    private void HandleInput(Player.OnAttackPressedEventArgs.AttackType attackType)
    {
        // If combat layer is disabled, allow new input.

        if (_startComboWindowTimer)
        {
            switch (attackType)
            {
                case Player.OnAttackPressedEventArgs.AttackType.Light:
                    _animator.SetTrigger("Trigger_L");
                    break;
                case Player.OnAttackPressedEventArgs.AttackType.Heavy:
                    _animator.SetTrigger("Trigger_H");
                    break;
                default:
                    break;
            }
        }
    }

    private void Update()
    {
        HandleAnimationLayers();
    }

    private void LateUpdate()
    {
        StartComboWindowCheck();
    }

    private void HandleAnimationLayers()
    {
        if (_weight != _desiredWeight)
        {
            _weight -= (1 * _weightChanger);

            if (_weight < 0f)
            {
                _weight = 0.01f;
            }
            else if (_weight > 1f)
            {
                _weight = 0.99f;
            }

            _animator.SetLayerWeight(_animator.GetLayerIndex("CombatTree"), _weight);
        }
    }

    public void DisableCombatLayer()
    {
        Debug.Log("DisableCombatLayer : Combo=" + _currentCombo);
        _desiredWeight = 0.01f;
        _weightChanger = 0.025f;
        _weight = 0.99f;
    }

    public void EnableCombatLayer()
    {
        Debug.Log("EnableCombatLayer: Combo=" + _currentCombo);
        _desiredWeight = 0.99f;
        _weightChanger = -0.025f;
        _weight = 0.01f;
    }

    void StartComboWindowCheck()
    {

        if (_startComboWindowTimer)
        {
            _comboWindowTimer += Time.deltaTime;
            if (_comboWindowTimer > _timeBetweenInputs)
            {
                //EndCombo();
            }
        }
    }

    void EndCombo()
    {
        _comboWindowTimer = 0;
        _animator.SetTrigger("ComboBroken");
        _animator.ResetTrigger("Trigger_L");
        _animator.ResetTrigger("Trigger_H");
        _startComboWindowTimer = false;
    }

    public void Locomotion(Vector3 moveDirection, Vector3 rotateDirection)
    {
        _animator.SetFloat("VelocityZ", Vector3.Dot(moveDirection, transform.forward), 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", Vector3.Dot(moveDirection, transform.right), 0.1f, Time.deltaTime);

        bool isMoving = Mathf.Abs(moveDirection.x) >= float.Epsilon && Mathf.Abs(moveDirection.z) >= float.Epsilon;

        if (_wasMoving != isMoving)
        {
            _animator.SetBool("isWalking", isMoving);
            _wasMoving = isMoving;
        }
    }
}
