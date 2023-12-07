using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, ICanAttack, IDamagable, IHasDamageVFX
{

    public static Player Instance;


    [SerializeField] private GameInput _gameInput;
    public GameInput GameInput { get { return _gameInput; } }


    public event EventHandler ChangeControllerTypeButtonPressed;
    public event EventHandler StartEvade;
    public event EventHandler DisableMovement;
    public event EventHandler EnableMovement;

    public event EventHandler ComboBroken;
    public event EventHandler<OnAttackPressedEventArgs> RegisterAttack;
    public event EventHandler<OnAttackPressedAnimationEventArgs> StartAttackAnimation;
    public event EventHandler PlayerInterrupted;

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

    //  [SerializeField] private GameInput _gameInput;
    [SerializeField] private CurrentAttackSO[] _AttackSOArray;

    [SerializeField] private PlayerWeaponHolder _playerWeaponHolder;

    //[SerializeField] private Weapon _currentWeapon;
    //[SerializeField] private GameObject weaponHand;
    //[SerializeField] private GameObject weaponObject;
    [SerializeField] private List<GameObject> _damageEffects = new List<GameObject>();


    //Combo
    float _tempComboChecker;

    [Header("Health settings")]

    private HealthManager _healthManager;
    private PlayerDash _playerDash;
    private PlayerMovement _playerMovement;
    private EntertainmentManager _entertainmentManager;

    private CapsuleCollider _collider;
    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody _rb;
    private string _input;
    public bool _canAttack = true;
    private PlayerInputSpamChecker _playerInputSpamChecker;
    [SerializeField] private Transform _shockwavePosition;

    /// <summary>
    /// Used for testing challenge "KillStreak"
    /// </summary>
    public bool HasTakenDamage { get; set; }
   // public Weapon CurrentWeapon { get => _currentWeapon; set => _currentWeapon = value; }

    private bool _invulnerable = false;
    private bool _interruptable = false;
    

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
        }


        _healthManager = GetComponent<HealthManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerDash = GetComponent<PlayerDash>();

        _collider = GetComponent<CapsuleCollider>();
        _playerInputSpamChecker = GetComponent<PlayerInputSpamChecker>();
       
        
    }


    void Start()
    {
       

        _gameInput = GameInput.Instance;
        _gameInput.OnInteractActionPressed += GameInput_OnInteractActionPressed;
        _gameInput.OnLightAttackButtonPressed += GameInput_OnLightAttackButtonPressed;
        _gameInput.OnHeavyAttackButtonPressed += GameInput_OnHeavyAttackButtonPressed;
        _gameInput.OnEvadeButtonPressed += GameInput_OnEvadeButtonPressed;

        _playerDash.EvadePerformed += PlayerDash_OnEvadePerformed;

        _input = "";
        _entertainmentManager = EntertainmentManager.Instance;

        GetComponent<AttackManager>().EnemyHit += AttackLanded;
        GetComponent<AttackManager>().AttackMissed += ResetComboChecker;
        _playerWeaponHolder = GetComponent<PlayerWeaponHolder>();
        
        if (TransferableScript.Instance.GetWeapon() != null)
        {
            _playerWeaponHolder.SetWeapon(TransferableScript.Instance.GetWeapon());
        }
           

        //_shockwavePosition = transform.Find("ShockwavePosition");

        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();   
        PlayerWeaponHolder.Instance.SetWeapon(TransferableScript.Instance.GetWeapon());
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
        // _gameInput = GameManager.Instance.gameObject.GetComponent<GameInput>();
        //if (GameManager.Instance._currentScen != GameManager.CurrentScen.ArenaScen)
        //{
        //    _canAttack = false;

        //}
       
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
            if (!IsDashing && _input != "" && _interruptable)
            {
                //If we get here the player got damaged while attacking, we reset the player combo with OnComboBroken()
                OnComboBroken();
                PlayerInterrupted?.Invoke(this, EventArgs.Empty);
            }

            PlayDamageVFX(attack.AttackerPosition);
            _healthManager.ReduceHealth(attack.Damage);
            HasTakenDamage = true;

            Debug.Log(attack.AttackSO.CurrentAttackEffect);
            if (attack.AttackSO.CurrentAttackEffect == CurrentAttackSO.AttackEffect.Pushback)
            {
                Debug.Log("Push back player");
                GetPushedback(attack.AttackerPosition, 100);//attack.AttackSO.Force);
                OnDisableMovement();
            }
        }
        else
        {
            Debug.Log("invulnerable");
        }
    }


    //public void GetUpAnimEvent()
    //{
    //    OnEnableMovement();

    //}


    /// <summary>
    /// Calls Pushback on enemy. Only for minions.
    /// </summary>
    private void GetPushedback(Vector3 attackerPos, float knockBackForce)
    {
        //PushBack(attackerPos, knockBackForce);
        Debug.Log("In GetPushedBack method, setting anim trigger");
        Debug.Log("Pushback force: " + knockBackForce + " AttackerPos: " + attackerPos);
        _anim.SetTrigger("PushedBack");
        Debug.Log(this.GetType().ToString() + "Player knocked back with force: " + knockBackForce);
        Vector3 direction = attackerPos - transform.position;
        _rb.AddForce(-direction * 5, ForceMode.Impulse);
        _rb.AddForce(transform.up * 2, ForceMode.Impulse);
        StartFacingExplosion(attackerPos);
    }


    private IEnumerator FaceExplosion(Vector3 targetPos)
    {
        while(true)
        {
            Vector3 direction = targetPos - transform.position;
            direction.y = 0;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10);
            //transform.Translate(-direction * 13 * Time.deltaTime, Space.World);
            //_rb.AddForce(-direction * 10, ForceMode.Impulse);
            if (Quaternion.Angle(transform.rotation, targetRot) < 0.1f)
            {
                yield break;
            }

            yield return null;
        }
    }

    private void StartFacingExplosion(Vector3 explosionPos)
    {
        StartCoroutine(FaceExplosion(explosionPos));
    }


    /// <summary>
    /// Adds force to enemy backwards. Only for minions.
    /// </summary>
    private void PushBack(Vector3 attackerPos, float knockBackForce)
    {
        Vector3 knockbackDirection = (transform.position - attackerPos).normalized;
        _rb.AddForce(knockbackDirection * knockBackForce, ForceMode.Impulse);
        //_rb.AddExplosionForce(1000, attackerPos, range);
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
                _entertainmentManager?.increaseETP(GetCurrentAttackSO(_input).ETPChange);
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
            SetInterruptable(true);
            OnEnableMovement();
            RegisterAttack?.Invoke(this, new OnAttackPressedEventArgs { CurrentAttackSO = GetCurrentAttackSO(_input), weaponSO =_playerWeaponHolder.CurrentWeapon});

            if (_playerWeaponHolder.CurrentWeapon == null)
            {
                Debug.LogWarning("Player Weapon is null");
            }

            _canAttack = true;
        }
    }


    public void PlaySlamEffect()
    {
        Debug.Log("Play Slam Effect");
        if (ParticleSystemManager.Instance == null)
        {
            Debug.Log("PSManager is null");
        }
        ParticleSystemManager.Instance.PlayParticleFromPool(ParticleSystemManager.ParticleEffects.Slam, _shockwavePosition);
        
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
        SetInterruptable(false);
        _playerInputSpamChecker?.AddInputSequence(_input);
        _input = "";
        ComboBroken?.Invoke(this, EventArgs.Empty);
        _canAttack = true;
    }

    private void OnAttack(OnAttackPressedAnimationEventArgs.AttackType attack)
    {
        if (!_playerDash.IsDashing)
        {
            SetInterruptable(false);
            _playerMovement.AttackDash();
            OnDisableMovement();
            StartAttackAnimation?.Invoke(this, new OnAttackPressedAnimationEventArgs { attackType = attack });
                       
            _canAttack = false;
        }
    }

    private void SetInterruptable(bool interruptable)
    {
        _interruptable = interruptable;
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
    //public void SetWeapon(Weapon _weapon)
    //{
    //    CurrentWeapon = _weapon;
    //    ReplaceWeapon();

    //}
    //public void ReplaceWeapon()
    //{
    //    if(CurrentWeapon != null)
    //    {
    //        Debug.Log(CurrentWeapon.GetPath());
            
    //        GameObject weaponnew = (GameObject)Instantiate(Resources.Load("WeaponResources/"+CurrentWeapon.GetPath()));
    //        weaponnew.transform.parent = weaponHand.transform;
    //        weaponnew.transform.position = weaponObject.transform.position;
    //        weaponnew.transform.rotation = weaponObject.transform.rotation;
    //        weaponnew.transform.localScale = weaponObject.transform.localScale;
    //        weaponObject.transform.GetChild(0).parent = weaponnew.transform;
    //        WeaponVisualEffects wve = gameObject.GetComponent<WeaponVisualEffects>();
    //        wve.SetNewTrail(weaponnew.transform.GetChild(0));
    //        Destroy(weaponObject);
    //        weaponObject = weaponnew;

    //    }
        
    //}
    
}
