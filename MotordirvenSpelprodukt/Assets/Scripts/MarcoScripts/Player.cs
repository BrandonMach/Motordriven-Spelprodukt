using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Placeholder för nuvarande vapnets värden
    //-----------------------------------------
    float damage = 1;
    float range = 1;
    //-----------------------------------------


    public GameInput GameInput { get { return _gameInput; } }

    [SerializeField] private GameInput _gameInput;
    [SerializeField] float _timeBetweenInputs = 0f;
    [SerializeField] private CurrentAttackSO[] _AttackSOArray;

    private CurrentAttackSO _currentAttackSO;

    private float _inputTimer = 0;

    private string _input;

    public event EventHandler OnChangeControllerTypeButtonPressed;
    public EventHandler<OnAttackPressedEventArgs> OnAttackPressed;
    //public EventHandler <OnLightAttackPressedEventArgs> OnLightAttackPressed;

    public class OnAttackPressedEventArgs : EventArgs
    {
        public enum AttackType
        {
            Light,
            Heavy
        }
        public CurrentAttackSO CurrentAttackSO;
        public AttackType attackType;
        public float weaponDamage;
        public float weaponRange;
    }


    // Start is called before the first frame update
    void Start()
    {
        _gameInput.OnInteractActionPressed += GameInput_OnInteractActionPressed;
        _gameInput.OnLightAttackButtonPressed += GameInput_OnLightAttackButtonPressed;
        _gameInput.OnHeavyAttackButtonPressed += GameInput_OnHeavyAttackButtonPressed;

        _input = "";

    }

    private void GameInput_OnLightAttackButtonPressed(object sender, EventArgs e)
    {
        Debug.Log("Light");
      

        if (_inputTimer >= _timeBetweenInputs)
        {
            _input += "L";
            if (GetCurrentAttackSO(_input) == null)
            {
                _input = "L";
            }
            OnAttackPressed?.Invoke(this, new OnAttackPressedEventArgs {CurrentAttackSO = GetCurrentAttackSO(_input), weaponDamage = damage, weaponRange = range, attackType = OnAttackPressedEventArgs.AttackType.Light });
            _inputTimer = 0;
        }
    }

    private void GameInput_OnHeavyAttackButtonPressed(object sender, EventArgs e)
    {
  

        if (_inputTimer >= _timeBetweenInputs)
        {
            _input += "H";

            if (GetCurrentAttackSO(_input) == null)
            {
                _input = "H";
            }

            OnAttackPressed?.Invoke(this, new OnAttackPressedEventArgs {CurrentAttackSO = GetCurrentAttackSO(_input), weaponDamage = damage, weaponRange = range, attackType = OnAttackPressedEventArgs.AttackType.Heavy });
            _inputTimer = 0;
        }
    }

    private void GameInput_OnInteractActionPressed(object sender, System.EventArgs e)
    {
        OnChangeControllerTypeButtonPressed?.Invoke(this, e);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private CurrentAttackSO GetCurrentAttackSO(string name)
    {
        foreach (CurrentAttackSO currentAttackSO in _AttackSOArray)
        {
            if (currentAttackSO.name.ToLower() == name.ToLower())
            {
                return currentAttackSO;
            }
        }
        return null;
    }
}
