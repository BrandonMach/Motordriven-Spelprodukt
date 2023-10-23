using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, ICanAttack, IDamagable
{
    // Placeholder för nuvarande vapnets värden
    //-----------------------------------------
    float damage = 1;
    float range = 1;
    //-----------------------------------------

    public GameInput GameInput { get { return _gameInput; } }


    public event EventHandler OnChangeControllerTypeButtonPressed;
    public event EventHandler OnStartEvade;
    public event EventHandler OnDisableMovement;
    public event EventHandler OnEnableMovement;

    public event EventHandler ComboBroken;
    public event EventHandler<OnAttackPressedEventArgs> OnRegisterAttack;
    public event EventHandler<OnAttackPressedAnimationEventArgs> OnAttackAnimationPressed;





    public class OnAttackPressedAnimationEventArgs : EventArgs
    {
        public enum AttackType
        {
            Light,
            Heavy
        }
        public AttackType attackType;
    }


    [SerializeField] private GameInput _gameInput;
    [SerializeField] private float _timeBetweenInputs = 0f;
    [SerializeField] private CurrentAttackSO[] _AttackSOArray;
    [SerializeField] private Weapon _currentWeapon;

    [Header("Health settings")]
    [SerializeField] float _maxHealth;
    HealthManager _healthManager;

    private CurrentAttackSO _currentAttackSO;
    private PlayerDash _playerDash;
    private PlayerMovement _playerMovement;

    private float _inputTimer = 0;

    private string _input;

    EntertainmentManager _etpManager;

    private void Awake()
    {
        _healthManager = GetComponent<HealthManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerDash = GetComponent<PlayerDash>();
    }
    void Start()
    {
        _gameInput.OnInteractActionPressed += GameInput_OnInteractActionPressed;
        _gameInput.OnLightAttackButtonPressed += GameInput_OnLightAttackButtonPressed;
        _gameInput.OnHeavyAttackButtonPressed += GameInput_OnHeavyAttackButtonPressed;
        _gameInput.OnEvadeButtonPressed += GameInput_OnEvadeButtonPressed;

        _playerDash.EvadePerformed += PlayerDash_OnEvadePerformed;

        _input = "";


        _etpManager = GameObject.FindGameObjectWithTag("ETPManager").GetComponent<EntertainmentManager>();
    }

    private void GameInput_OnEvadeButtonPressed(object sender, EventArgs e)
    {

        if (_playerMovement.IsMoving() && _playerDash.IsDashAvailable())
        {
            OnDisableMovement?.Invoke(this, EventArgs.Empty);

            OnStartEvade?.Invoke(this, EventArgs.Empty);
        }      
    }

    //public void Knockbacked(object sender, EventArgs e)
    //{
    //    OnDisableMovement?.Invoke(this, EventArgs.Empty);
    //}

    //public void KnockbackedFinish(object sender, EventArgs e)
    //{
    //    OnEnableMovement?.Invoke(this, EventArgs.Empty);
    //}

    private void PlayerDash_OnEvadePerformed(object sender, EventArgs e)
    {
        OnEnableMovement?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnLightAttackButtonPressed(object sender, EventArgs e)
    {
        if (_inputTimer >= _timeBetweenInputs)
        {
            
            if (GetCurrentAttackSO(_input + "L") != null)
            {
                _input += "L";
                OnAttackAnimationPressed?.Invoke(this, new OnAttackPressedAnimationEventArgs { attackType = OnAttackPressedAnimationEventArgs.AttackType.Light });
                _inputTimer = 0;
            }
           
        }
    }

    private void GameInput_OnHeavyAttackButtonPressed(object sender, EventArgs e)
    {
  

        if (_inputTimer >= _timeBetweenInputs)
        {
            if (GetCurrentAttackSO(_input + "H") != null)
            {
                _input += "H";
                OnAttackAnimationPressed?.Invoke(this, new OnAttackPressedAnimationEventArgs { attackType = OnAttackPressedAnimationEventArgs.AttackType.Heavy });
                _inputTimer = 0;
            }
        }
    }

    // Will be called by animation event
    private void OnPlayerAttack()
    {
        OnRegisterAttack?.Invoke(this, new OnAttackPressedEventArgs { CurrentAttackSO = GetCurrentAttackSO(_input), weaponSO = _currentWeapon });
    }

    private void GameInput_OnInteractActionPressed(object sender, System.EventArgs e)
    {
        OnChangeControllerTypeButtonPressed?.Invoke(this, e);
        
    }

    void Update()
    {
        if (_inputTimer < _timeBetweenInputs)
        {
            _inputTimer += Time.deltaTime;
        }
    }

    
    private CurrentAttackSO GetCurrentAttackSO(string name)
    {
        foreach (CurrentAttackSO currentAttackSO in _AttackSOArray)
        {
            if (currentAttackSO.name.ToLower() == name.ToLower())
            {
                if (currentAttackSO.Last)
                {
                    _etpManager.increaseETP(20);
                }
                return currentAttackSO;
            }
        }
        return null;
    }

    //Function will be called using animation events
    private void OnComboBroken(string combo)
    {
       
        if (_input == combo)
        {
            _input = "";
            ComboBroken?.Invoke(this, EventArgs.Empty);
        }
    }

    public void TakeDamage(Attack attack)
    {
        _healthManager.ReduceHealth(damage);  
    }


    private void OnDestroy()
    {
        _gameInput.OnInteractActionPressed -= GameInput_OnInteractActionPressed;
        _gameInput.OnLightAttackButtonPressed -= GameInput_OnLightAttackButtonPressed;
        _gameInput.OnHeavyAttackButtonPressed -= GameInput_OnHeavyAttackButtonPressed;
        _gameInput.OnEvadeButtonPressed -= GameInput_OnEvadeButtonPressed;
        _playerDash.EvadePerformed -= PlayerDash_OnEvadePerformed;

    }
}
