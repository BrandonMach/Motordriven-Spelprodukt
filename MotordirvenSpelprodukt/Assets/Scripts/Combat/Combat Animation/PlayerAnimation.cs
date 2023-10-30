using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public float _timeBetweenInputs = 0f;

    [SerializeField] private Player _player;

    [Header("Combo Sequence")]
    //[SerializeField] private bool _startComboWindowTimer;


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
        _player.ChangeAttackAnimation += Player_ChangeAttackAnimation;
        _player.StartEvade += Player_StartEvade;
        _player.EnableMovement += Player_OnEnableMovement;
        _player.ComboBroken += Player_OnComboBroken;

    }

    private void Player_OnComboBroken(object sender, EventArgs e)
    {
        EndCombo();
    }

    private void Player_OnEnableMovement(object sender, EventArgs e)
    {

    }

    private void Player_StartEvade(object sender, EventArgs e)
    {
        _animator.SetTrigger("Evade");
    }

    private void Player_ChangeAttackAnimation(object sender, Player.OnAttackPressedAnimationEventArgs e)
    {
        //_startComboWindowTimer = true;
        HandleAttackAnimation(e.attackType);
    }

    private void HandleAttackAnimation(Player.OnAttackPressedAnimationEventArgs.AttackType attackType)
    {
    
        switch (attackType)
        {
            case Player.OnAttackPressedAnimationEventArgs.AttackType.Light:
                _animator.SetTrigger("Trigger_L");
                break;
            case Player.OnAttackPressedAnimationEventArgs.AttackType.Heavy:
                _animator.SetTrigger("Trigger_H");
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        HandleAnimationLayers();
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

    void EndCombo()
    {
        _animator.SetTrigger("ComboBroken");
        _animator.ResetTrigger("Trigger_L");
        _animator.ResetTrigger("Trigger_H");
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
