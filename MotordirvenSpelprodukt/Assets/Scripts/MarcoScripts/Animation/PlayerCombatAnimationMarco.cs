using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatAnimationMarco : MonoBehaviour
{
    //[SerializeField] private GameInput gameInput;
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;
    private string _currentCombo = ""; //
    private float _desiredWeight = 0;
    private float _weight = 0;
    private float _weightChanger = -0.025f;
    private float _inputTimer = 0;
    public float _timeBetweenInputs = 1.5f;


    private void Awake()
    {
        
    }
    void Start()
    {
        _player = GetComponent<Player>();
        //_etpManager = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
        _player.OnAttackPressed += Player_OnAttack;
        //player.OnAttackPressed += Player_OnHeavyAttackPressed;

        _inputTimer = _timeBetweenInputs;
    }

    private void Player_OnAttack(object sender, Player.OnAttackPressedEventArgs e)
    {
        HandleInput(e.attackType);
    }
    //private void Player_OnHeavyAttackPressed(object sender, EventArgs e)
    //{
    //    HandleInput("H");
    //}

    //private void Player_OnLightAttackPressed(object sender, EventArgs e)
    //{
    //    HandleInput("L");
    //}

    private void HandleInput(string attackType)
    {
        // If combat layer is disabled, allow new input.

        if (_inputTimer >= _timeBetweenInputs)
        {
            _currentCombo += attackType; // Adds character to string combo
            Debug.Log(_currentCombo);
            AnimateAttack();
            _inputTimer = 0;
        }
    }

    private void AnimateAttack()
    {
        // CurrentCombo = "LL"
        _animator.SetTrigger("Trigger_" + _currentCombo);
        if (_currentCombo.Length == 3 || _currentCombo == "LH" || _currentCombo == "HL")
        {
            Debug.LogError("Combo matched");
            //_etpManager.increaseETP(15);
            _currentCombo = "";
            _timeBetweenInputs = 1.5f;
        }
    }

    private void Update()
    {
        _inputTimer += Time.deltaTime;

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
    //public void Test()
    //{
    //    if (animator.GetFloat("test") == 0)
    //    {
    //        animator.SetFloat("test", 1);
    //    }
    //    else
    //    {
    //        animator.SetFloat("test", 0);
    //    }
    //    //animator.SetFloat("test", 1);
    //}
}
