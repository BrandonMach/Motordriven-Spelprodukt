using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Placeholder för nuvarande vapnets värden
    //-----------------------------------------
    float damage;
    float range;
    //-----------------------------------------


    public GameInput GameInput { get { return _gameInput; } }

    [SerializeField] private GameInput _gameInput;


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

        public AttackType attackType1;
        public float weaponDamage;
        public float weaponRange;
        public string attackType;
    }


    // Start is called before the first frame update
    void Start()
    {
        _gameInput.OnInteractActionPressed += GameInput_OnInteractActionPressed;
        _gameInput.OnLightAttackButtonPressed += GameInput_OnLightAttackButtonPressed;
        _gameInput.OnHeavyAttackButtonPressed += GameInput_OnHeavyAttackButtonPressed;
    }

    private void GameInput_OnLightAttackButtonPressed(object sender, EventArgs e)
    {
        Debug.Log("Light");
        OnAttackPressed?.Invoke(this, new OnAttackPressedEventArgs { weaponDamage = damage, weaponRange = range, attackType1 = OnAttackPressedEventArgs.AttackType.Light});
    }

    private void GameInput_OnHeavyAttackButtonPressed(object sender, EventArgs e)
    {
        OnAttackPressed?.Invoke(this, new OnAttackPressedEventArgs { weaponDamage = damage, weaponRange = range, attackType1 = OnAttackPressedEventArgs.AttackType.Heavy});
    }

    private void GameInput_OnInteractActionPressed(object sender, System.EventArgs e)
    {
        OnChangeControllerTypeButtonPressed?.Invoke(this, e);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
