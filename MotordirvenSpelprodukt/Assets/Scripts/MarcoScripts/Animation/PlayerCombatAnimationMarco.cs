using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatAnimationMarco : MonoBehaviour
{
    public float _timeBetweenInputs = 0f;

    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;

    [Header("Combo Sequence")]
    [SerializeField] private float _comboWindowTimer = 0;
    [SerializeField] private bool _startComboWindowTimer;
    [SerializeField] private float comboWindow = 2;



    private string _currentCombo = ""; //
    private float _desiredWeight = 0;
    private float _weight = 0;
    private float _weightChanger = -0.025f;
    private float _inputTimer = 0;




    private void Awake()
    {
        
    }
    void Start()
    {
        _player = GetComponent<Player>();
        _player.OnAttackPressed += Player_OnAttack;

        _inputTimer = _timeBetweenInputs;
    }

    private void Player_OnAttack(object sender, Player.OnAttackPressedEventArgs e)
    {
        Debug.Log("ATTACK");

        HandleInput(e.attackType);
        _startComboWindowTimer = true;
        _comboWindowTimer = 0;
    }

    private void HandleInput(Player.OnAttackPressedEventArgs.AttackType attackType)
    {
        // If combat layer is disabled, allow new input.

        if (_inputTimer >= _timeBetweenInputs)
        {
            switch (attackType)
            {
                case Player.OnAttackPressedEventArgs.AttackType.Light:
                    //_currentCombo += "L";
                    _animator.SetTrigger("Trigger_L");
                    break;
                case Player.OnAttackPressedEventArgs.AttackType.Heavy:
                    //_currentCombo += "H";
                    _animator.SetTrigger("Trigger_H");
                    break;
                default:
                    break;
            }
            _inputTimer = 0;
        }
    }

    private void Update()
    {
        _inputTimer += Time.deltaTime;

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

        _comboWindowTimer += Time.deltaTime;
        if (_startComboWindowTimer && _comboWindowTimer > 1.5)
        {
            EndCombo();
        }
        if (_comboWindowTimer >= comboWindow)
        {
        }
    }

    void EndCombo()
    {
        Debug.Log("EndingCombo");
        _comboWindowTimer = 0;
        _animator.SetTrigger("ComboBroken");
        _startComboWindowTimer = false;
    }
}
