using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, ICanAttack, IDamagable, IHasDamageVFX
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

    public bool IsDashing { get; private set; }

    [SerializeField] private GameInput _gameInput;
    [SerializeField] private CurrentAttackSO[] _AttackSOArray;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private GameObject weaponHand;
    [SerializeField] private GameObject weaponObject;
    [SerializeField] private List<GameObject> _damageEffects = new List<GameObject>();


    //Combo
    float _tempComboChecker;

    [Header("Health settings")]

    private HealthManager _healthManager;
    private PlayerDash _playerDash;
    private PlayerMovement _playerMovement;
    private EntertainmentManager _entertainmentManager;

    private BoxCollider _collider;


    private string _input;
    private bool _canAttack = true;
    private PlayerInputSpamChecker _playerInputSpamChecker;

    /// <summary>
    /// Used for testing challenge "KillStreak"
    /// </summary>
    public bool HasTakenDamage { get; set; }
    public Weapon CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }

    private bool _invulnerable = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


        _healthManager = GetComponent<HealthManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerDash = GetComponent<PlayerDash>();

        _collider = GetComponent<BoxCollider>();
        _playerInputSpamChecker = GetComponent<PlayerInputSpamChecker>();
        
        
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

        GetComponent<AttackManager>().EnemyHit += AttackLanded;
        GetComponent<AttackManager>().AttackMissed += ResetComboChecker;
        if (GameObject.Find("Transferables").GetComponent<TransferableScript>().GetWeapon() != null)
            SetWeapon(GameObject.Find("Transferables").GetComponent<TransferableScript>().GetWeapon());
    }

    private void GameInput_OnEvadeButtonPressed(object sender, EventArgs e)
    {

        if (_playerDash.IsDashAvailable())
        {
            IsDashing = true;
            _input += "E";
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
        IsDashing = false;
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
                    //Görs två gånger
                    //_entertainmentManager.increaseETP(20);
                    //break;
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
        if (!_invulnerable)
        {
            OnComboBroken();
            PlayDamageVFX(attack.AttackerPosition);
            _healthManager.ReduceHealth(attack.Damage);
            HasTakenDamage = true;
        }
        else
        {
            Debug.Log("invulnerable");
        }
    }

    private void AttackLanded(object sender, EventArgs e)
    {
        _tempComboChecker++;
        Debug.LogWarning("Temp Combo Checker: " + _tempComboChecker);

    }
    private void ResetComboChecker(object sender, EventArgs e)
    {
        _tempComboChecker = 0;
        Debug.LogWarning("Temp Combo Checker has been Reseted: " + _tempComboChecker);

    }

    private void AnimationEvent_ComboBroken(string combo)
    {

        if (_input == combo) // Makes sure that it isn't the previous animation that is calling the event
        {

            if(_input.Length <= _tempComboChecker && _input.Length == 3) //Only give ETP if 3 hit-combo is executed
            {
                _entertainmentManager.increaseETP(GetCurrentAttackSO(_input).ETPChange);
            }
            
            OnComboBroken();
        }
    }

    private void GameInput_OnLightAttackButtonPressed(object sender, EventArgs e)
    {

        if (!_canAttack)
        {
            //Kolla om man spammar knappar under attack animation
            return;
        }
       

        if (GetCurrentAttackSO(_input + "L") != null)
        {
            _input += "L";
            OnAttack(OnAttackPressedAnimationEventArgs.AttackType.Light);
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
            OnAttack(OnAttackPressedAnimationEventArgs.AttackType.Heavy);

        }
    }

    // Will be called by animation event
    private void AnimationEvent_OnPlayerAttacked()
    {
        if (!_playerDash.IsDashing)
        {
            OnEnableMovement();
            RegisterAttack?.Invoke(this, new OnAttackPressedEventArgs { CurrentAttackSO = GetCurrentAttackSO(_input), weaponSO = CurrentWeapon });
            _canAttack = true;
        }

    }

    private void GameInput_OnInteractActionPressed(object sender, System.EventArgs e)
    {
        ChangeControllerTypeButtonPressed?.Invoke(this, e);
        TakeDamage(new Attack { });

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
        OnEnableMovement();

        ResetComboChecker(this, EventArgs.Empty);

        _playerInputSpamChecker?.AddInputSequence(_input);
        _input = "";
        ComboBroken?.Invoke(this, EventArgs.Empty);
        _canAttack = true;
    }

    private void OnAttack(OnAttackPressedAnimationEventArgs.AttackType attack)
    {
        if (!_playerDash.IsDashing)
        {
            _playerMovement.AttackDash();
            OnDisableMovement();
            ChangeAttackAnimation?.Invoke(this, new OnAttackPressedAnimationEventArgs { attackType = attack });
                       
            _canAttack = false;
        }
    }

    public void SetPlayerInvulnarableState(bool invulnerable)
    {
        _invulnerable = invulnerable;
    }

    public void PlayDamageVFX(Vector3 attackerPosition)
    {
        if (_damageEffects.Count != 0)
        {
            Instantiate(_damageEffects[0], _collider.ClosestPoint(attackerPosition), transform.rotation);
        }
    }
    public void SetWeapon(Weapon _weapon)
    {
        CurrentWeapon = _weapon;
        ReplaceWeapon();

    }
    public void ReplaceWeapon()
    {
        if(CurrentWeapon != null)
        {
            Debug.Log(CurrentWeapon.GetPath());
            
            GameObject weaponnew = (GameObject)Instantiate(Resources.Load("WeaponResources/"+CurrentWeapon.GetPath()));
            weaponnew.transform.parent = weaponHand.transform;
            weaponnew.transform.position = weaponObject.transform.position;
            weaponnew.transform.rotation = weaponObject.transform.rotation;
            weaponnew.transform.localScale = weaponObject.transform.localScale;
            weaponObject.transform.GetChild(0).parent = weaponnew.transform;
            WeaponVisualEffects wve = gameObject.GetComponent<WeaponVisualEffects>();
            wve.SetNewTrail(weaponnew.transform.GetChild(0));
            Destroy(weaponObject);
            weaponObject = weaponnew;

        }
        
    }
    
}
