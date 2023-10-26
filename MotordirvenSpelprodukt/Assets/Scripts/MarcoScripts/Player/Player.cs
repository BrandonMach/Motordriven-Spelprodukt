using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, ICanAttack, IDamagable
{

    public static Player Instance;

    public GameInput GameInput { get { return _gameInput; } }


    public event EventHandler ChangeControllerTypeButtonPressed;
    public event EventHandler StartEvade;
    public event EventHandler DisableMovement;
    public event EventHandler EnableMovement;

    public event EventHandler ComboBroken;
    public event EventHandler<OnAttackPressedEventArgs> RegisterAttack;
    public event EventHandler<OnAttackPressedAnimationEventArgs> ChangeAttackAnimation;

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
    [SerializeField] private CurrentAttackSO[] _AttackSOArray;
    [SerializeField] private Weapon _currentWeapon;

    [Header("Health settings")]
    [SerializeField] float _maxHealth;

    private HealthManager _healthManager;
    private PlayerDash _playerDash;
    private PlayerMovement _playerMovement;
    private EntertainmentManager _entertainmentManager;


    private string _input;
    private bool _canAttack = true;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

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
        _entertainmentManager = EntertainmentManager.Instance;
    }

    private void GameInput_OnEvadeButtonPressed(object sender, EventArgs e)
    {

        if (_playerMovement.IsMoving() && _playerDash.IsDashAvailable())
        {
            OnDisableMovement();
            OnStartEvade();
        }      
    }

    //public void Knockbacked(object sender, EventArgs e)
    //{
    //    OnDisableMovement();
    //}

    //public void KnockbackedFinish(object sender, EventArgs e)
    //{
    //    OnEnableMovement();
    //}

    private void PlayerDash_OnEvadePerformed(object sender, EventArgs e)
    {
        OnEnableMovement();
        OnComboBroken();
    }

  

    void Update()
    {
        
    }

    
    private CurrentAttackSO GetCurrentAttackSO(string name)
    {
        foreach (CurrentAttackSO currentAttackSO in _AttackSOArray)
        {
            if (currentAttackSO.name.ToLower() == name.ToLower())
            {
                if (currentAttackSO.Last && _entertainmentManager != null)
                {
                    _entertainmentManager.increaseETP(20);
                }
                return currentAttackSO;
            }
        }
        return null;
    }


    private void OnDestroy()
    {
        _gameInput.OnInteractActionPressed -= GameInput_OnInteractActionPressed;
        _gameInput.OnLightAttackButtonPressed -= GameInput_OnLightAttackButtonPressed;
        _gameInput.OnHeavyAttackButtonPressed -= GameInput_OnHeavyAttackButtonPressed;
        _gameInput.OnEvadeButtonPressed -= GameInput_OnEvadeButtonPressed;
        _playerDash.EvadePerformed -= PlayerDash_OnEvadePerformed;

    }

    public void TakeDamage(Attack attack)
    {
        _healthManager.ReduceHealth(attack.Damage);
    }


    private void AnimationEvent_ComboBroken(string combo)
    {

        if (_input == combo)
        {
            OnComboBroken();
        }
    }

    private void GameInput_OnLightAttackButtonPressed(object sender, EventArgs e)
    {
        if (!_canAttack)
        {
            return;
        }
       

        if (GetCurrentAttackSO(_input + "L") != null)
        {
            _input += "L";
            OnAttackButtonPressed(OnAttackPressedAnimationEventArgs.AttackType.Light);
        }
    }

    private void GameInput_OnHeavyAttackButtonPressed(object sender, EventArgs e)
    {
        if (!_canAttack)
        {
            return;
        }

        if (GetCurrentAttackSO(_input + "H") != null)
        {
            _input += "H";
            OnAttackButtonPressed(OnAttackPressedAnimationEventArgs.AttackType.Heavy);

        }
    }

    // Will be called by animation event
    private void AnimationEvent_OnPlayerAttacked()
    {
        if (!_playerDash.IsDashing)
        {
            OnEnableMovement();
            RegisterAttack?.Invoke(this, new OnAttackPressedEventArgs { CurrentAttackSO = GetCurrentAttackSO(_input), weaponSO = _currentWeapon });
            _canAttack = true;
        }

    }

    private void GameInput_OnInteractActionPressed(object sender, System.EventArgs e)
    {
        ChangeControllerTypeButtonPressed?.Invoke(this, e);

    }
    private void OnDisableMovement()
    {
        DisableMovement?.Invoke(this, EventArgs.Empty);
    }

    private void OnEnableMovement()
    {
        EnableMovement?.Invoke(this, EventArgs.Empty);
    }

    private void OnStartEvade()
    {
        StartEvade?.Invoke(this, EventArgs.Empty);
    }


    private void OnComboBroken()
    {
        _input = "";
        ComboBroken?.Invoke(this, EventArgs.Empty);
        _canAttack = true;
    }

    private void OnAttackButtonPressed(OnAttackPressedAnimationEventArgs.AttackType attack)
    {
        if (!_playerDash.IsDashing)
        {
            OnDisableMovement();
            ChangeAttackAnimation?.Invoke(this, new OnAttackPressedAnimationEventArgs { attackType = attack });
            _canAttack = false;
        }
        
    }
}
