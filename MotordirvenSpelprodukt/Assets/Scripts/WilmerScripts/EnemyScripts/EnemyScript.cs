using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour, IDamagable, ICanAttack
{
    //[SerializeField] private ParticleSystem stunEffect;
    //[SerializeField] private Transform sphereCheck;
    //protected bool _canChase;
    //protected float _lastAttackTime;
    //protected float _attackSpeed; //?
    //public float AttackSpeed { get { return _attackSpeed; } }
    //public float LastAttackTime { get { return _lastAttackTime; } set { _lastAttackTime = value; } }
    //public bool CanChase { get { return _canChase; } set { _canChase = value; } }


    [SerializeField] protected float _movementSpeed = 20;
    [SerializeField] protected HealthManager _healthManager;
    [SerializeField] protected Animator anim;
    [SerializeField] private CurrentAttackSO[] _attackSOArray;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _landOffsetCheck;
    [SerializeField] protected bool _onGround = true;
    protected float _attackRange;
    //protected float _timeBetweenAttacks;
    protected float _stunDuration;
    //protected float _timeSinceLastAttack;
    protected Rigidbody _rb;

    protected bool _isImpaired;
    protected bool _outOfCombat;
    protected bool _canMove = true;
    protected CharacterController _characterController;
    private float startBleedTime;
    private bool _shouldCheckOnGround;
    private float groundCheckTimer;
    protected float _distanceToPlayer;
    public string currentAnim;
    public enum EnemyState { none, stunned, airborne, inAttack, pushed, chasing }
    public EnemyState CurrentState = EnemyState.chasing;
    public EnemyState PreviousState = EnemyState.none;


    #region Properties
    public Rigidbody RB { get { return _rb; } set { _rb = value; } }
    public CharacterController CharacterController { get { return _characterController; } set { _characterController = value; } }
    public AIMovement AIMovementScript { get; protected set; }
    public float MovementSpeed { get { return _movementSpeed; } }
    public float AttackRange { get { return _attackRange; } }
    public float StunDuration { get { return _stunDuration; } set { _stunDuration = value; } }
    //public float TimeBetweenAttacks { get { return _timeBetweenAttacks; } }
    //public float TimeSinceLastAttack { get { return _timeSinceLastAttack; } set { _timeSinceLastAttack = value; } }
    public bool IsImpaired { get { return _isImpaired; } set { _isImpaired = value; } }
    public bool OnGround { get { return _onGround; } set { _onGround = value; } }
    public bool OutOfCombat { get { return _outOfCombat; } set { _outOfCombat = value; } }
    public bool CanMove { get { return _canMove; } set { _canMove = value; } } 
    public Animator Anim { get { return anim; } set { anim = value; } }
    public float DistanceToPlayer { get { return _distanceToPlayer; } set { _distanceToPlayer = value; } }
    #endregion


    public event EventHandler<OnAttackPressedEventArgs> RegisterAttack;



    private void Awake()
    {
        AIMovementScript = GetComponent<AIMovement>();
    }


    protected virtual void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.useGravity = true;
        CharacterController = GetComponent<CharacterController>();
        EntertainmentManager.Instance.OutOfCombat += Instance_OnOutOfCombat;
        EntertainmentManager.Instance.InCombat += Instance_OnInCombat;
    }


    private void Instance_OnInCombat(object sender, EventArgs e)
    {
        _outOfCombat = true;
        Debug.Log("OutOfCombat");
    }


    private void Instance_OnOutOfCombat(object sender, EventArgs e)
    {
        Debug.Log("InCombat");
        _outOfCombat = false;
    }


    protected virtual void Update()
    {
        RB.AddForce(Vector3.down * RB.mass * 9.81f, ForceMode.Force);
        OnGround = Physics.Raycast(_groundCheck.position, Vector3.down, 0.2f);
        _distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        CheckCanMove();

        switch (CurrentState)
        {
            case EnemyState.none:
                break;

            case EnemyState.stunned:
                // Add timer for stun
                break;

            case EnemyState.airborne:
                groundCheckTimer += Time.deltaTime;
                if (_shouldCheckOnGround)
                {
                    if (Physics.Raycast(transform.position, Vector3.down, _landOffsetCheck))
                    {
                        ResetTriggers();
                        anim.SetTrigger("Land");
                        //CurrentState = EnemyState.landing;
                        groundCheckTimer = 0;
                        _shouldCheckOnGround = false;
                    }
                }
                break;

            case EnemyState.inAttack:
                FacePlayer();
                int randomValue = Mathf.FloorToInt(Random.Range(1.0f, 3.0f));
                ResetTriggers();
                if (randomValue == 1)
                {
                    Anim.SetTrigger("LightAttack");
                }
                else
                {
                    Anim.SetTrigger("HeavyAttack");
                }

                break;

            case EnemyState.pushed:
                break;

            case EnemyState.chasing:
                ResetTriggers();
                Anim.SetTrigger("Walking");

                if (CanMove)
                {
                    RB.velocity = transform.forward * MovementSpeed;
                    RB.AddForce(Vector3.down * RB.mass * 9.81f, ForceMode.Force);
                    FacePlayer();
                }
                break;

            default:
                break;
        }
    }


    private void FacePlayer()
    {
        //Vector3 direction = transform.position - transform.position;
        Vector3 direction = Player.Instance.transform.position - transform.position;
        direction.y = 0;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        //Rotate the Champion towards the players position
        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10);
    }


    public void SetState(string state)
    {

    }

    //public void SetAirBorn()
    //{
    //    CurrentState = EnemyState.airborne;
    //}


    public void ShouldCheckForGround()
    {
        _shouldCheckOnGround = true;
    }




    //protected virtual void Update()
    //{

    //}

    public void TakeDamage(Attack attack)
    {
        switch (attack.AttackSO.CurrentAttackEffect)
        {
            case CurrentAttackSO.AttackEffect.None:
                break;

            case CurrentAttackSO.AttackEffect.Pushback:
                //TODO: Calculate direction
                GetPushedback(Player.Instance.transform.position, attack.AttackSO.Force);
                break;

            case CurrentAttackSO.AttackEffect.StunAndPushback:
                //GetStunned(StunDuration, attack.AttackerPosition);
                GetPushedback(Player.Instance.transform.position, attack.AttackSO.Force);
                break;

            case CurrentAttackSO.AttackEffect.Knockup:
                GetKnockedUp(Player.Instance.transform.position, attack.AttackSO.Force);
                break;

            case CurrentAttackSO.AttackEffect.Bleed:
                //StartBleedCoroutine(attack);
                break;

            case CurrentAttackSO.AttackEffect.Stun:
                GetStunned(attack.AttackSO.StunDuration, Player.Instance.transform.position);
                break;
            default:
                break;
        }

        _healthManager.ReduceHealth(attack.Damage);
    }


    private void StartBleedCoroutine(Attack attack)
    {
        float bleedDamage = attack.Damage * attack.AttackSO.DamageMultiplier; // Temporary, will be replaced by weapon bleed multiplier
        startBleedTime = Time.time;
        StartCoroutine(_healthManager.Bleed(bleedDamage, startBleedTime));
    }


    protected void GetStunned(float stunDuration, Vector3 attackerPos)
    {
        _stunDuration = stunDuration;
        if (CurrentState != EnemyState.stunned)
        {
            PreviousState = CurrentState;
        }
        CurrentState = EnemyState.stunned;

        Vector3 pos = transform.position;
        pos = new Vector3(pos.x, pos.y + transform.localScale.y + 1, pos.z);
        //Instantiate(stunEffect, pos, Quaternion.Euler(-90, 0, 0), transform);


        //ParticleSystemManager.Instance.PlayStunEffect(pos, Quaternion.Euler(-90, 0, 0), transform);
        //ParticleSystemManager.Instance.PlayShockWaveEffect(attackerPos);
    }


    protected void GetKnockedUp(Vector3 attackerPos, float force)
    {
        RB.AddForce(Vector3.up * force, ForceMode.Impulse);
        PushBack(attackerPos, force / 4);
        if (CurrentState != EnemyState.airborne)
        {
            PreviousState = CurrentState;
        }
        CurrentState = EnemyState.airborne;
        ResetTriggers();
        anim.SetTrigger("KnockUp");
    }


    protected void GetPushedback(Vector3 attackerPos, float knockBackForce)
    {
        PushBack(attackerPos, knockBackForce);
        PreviousState = CurrentState;
        CurrentState = EnemyState.pushed;

        Debug.Log(this.GetType().ToString() + "Enemy knocked back with force: " + knockBackForce);
        ResetTriggers();
        Anim.SetTrigger("PushedBack");
    }


    protected void PushBack(Vector3 attackerPos, float knockBackForce)
    {
        Vector3 knockbackDirection = (transform.position - attackerPos).normalized;
        RB.AddForce(knockbackDirection * knockBackForce, ForceMode.Impulse);
    }


    public void TriggerGetUpAnim()
    {
        ResetTriggers();
        anim.SetTrigger("GetUp");
    }


    public virtual void ResetTriggers()
    {
        Anim.ResetTrigger("Walking");
        Anim.ResetTrigger("LightAttack");
        Anim.ResetTrigger("HeavyAttack");
        Anim.ResetTrigger("Stunned");
        Anim.ResetTrigger("PushedBack");
        Anim.ResetTrigger("KnockUp");
        Anim.ResetTrigger("Land");
        Anim.ResetTrigger("Taunt");
    }


    protected virtual void OnAttack()
    {
        RegisterAttack?.Invoke(this, new OnAttackPressedEventArgs { CurrentAttackSO = _attackSOArray[0], weaponSO = _weapon });
    }


    private void CheckCanMove()
    {
        AnimatorStateInfo anim = Anim.GetCurrentAnimatorStateInfo(0);
        //currentAnim = anim.IsName()
       
        if (anim.IsName("Heavy Attack") || anim.IsName("Light Attack"))
        {
            CanMove = false;
        }
        else CanMove = true;
        
    }


    protected virtual void ExitAttackAnimEvent()
    {
        // Check if stunned?
        ResetTriggers();
        //CanMove = true;
        //PreviousState = CurrentState;
        CurrentState = EnemyState.none;
    }





    public void EnterAttackAnimEvent()
    {
        //CanMove = false;
    }


    protected virtual void OnDestroy()
    {
        EntertainmentManager.Instance.OutOfCombat -= Instance_OnOutOfCombat;
        EntertainmentManager.Instance.InCombat -= Instance_OnInCombat;
    }
}


//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class EnemyScript : MonoBehaviour, IDamagable, ICanAttack
//{
//    //[SerializeField] private ParticleSystem stunEffect;
//    //[SerializeField] private Transform sphereCheck;
//    //protected bool _canChase;
//    //protected float _lastAttackTime;
//    //protected float _attackSpeed; //?
//    //public float AttackSpeed { get { return _attackSpeed; } }
//    //public float LastAttackTime { get { return _lastAttackTime; } set { _lastAttackTime = value; } }
//    //public bool CanChase { get { return _canChase; } set { _canChase = value; } }


//    [SerializeField] protected float _movementSpeed = 20;
//    [SerializeField] protected HealthManager _healthManager;
//    [SerializeField] protected Animator anim;
//    [SerializeField] private CurrentAttackSO[] _attackSOArray;
//    [SerializeField] private Weapon _weapon;
//    [SerializeField] private Transform _groundCheck;
//    [SerializeField] private float _landOffsetCheck;
//    [SerializeField] protected bool _onGround = true;
//    protected float _attackRange;
//    //protected float _timeBetweenAttacks;
//    protected float _stunDuration;
//    //protected float _timeSinceLastAttack;
//    protected Rigidbody _rb;

//    protected bool _isImpaired;
//    protected bool _outOfCombat;
//    protected bool _shouldMove;
//    protected CharacterController _characterController;
//    private float startBleedTime;
//    private bool _shouldCheckOnGround;
//    private float groundCheckTimer;
//    protected float _distanceToPlayer;
//    public enum Impairement { none, stunned, airborne, inAttack, pushed }
//    public Impairement CurrentImpairement = Impairement.none;


//    #region Properties
//    public Rigidbody RB { get { return _rb; } set { _rb = value; } }
//    public CharacterController CharacterController { get { return _characterController; } set { _characterController = value; } }
//    public AIMovement AIMovementScript { get; protected set; }
//    public float MovementSpeed { get { return _movementSpeed; } }
//    public float AttackRange { get { return _attackRange; } }
//    public float StunDuration { get { return _stunDuration; } set { _stunDuration = value; } }
//    //public float TimeBetweenAttacks { get { return _timeBetweenAttacks; } }
//    //public float TimeSinceLastAttack { get { return _timeSinceLastAttack; } set { _timeSinceLastAttack = value; } }
//    public bool IsImpaired { get { return _isImpaired; } set { _isImpaired = value; } }
//    public bool OnGround { get { return _onGround; } set { _onGround = value; } }
//    public bool OutOfCombat { get { return _outOfCombat; } set { _outOfCombat = value; } }
//    public bool ShouldMove { get { return _shouldMove; } set { _shouldMove = value; } }
//    public Animator Anim { get { return anim; } set { anim = value; } }
//    public float DistanceToPlayer { get { return _distanceToPlayer; } set { _distanceToPlayer = value; } }
//    #endregion


//    public event EventHandler<OnAttackPressedEventArgs> RegisterAttack;



//    private void Awake()
//    {
//        AIMovementScript = GetComponent<AIMovement>();
//    }


//    protected virtual void Start()
//    {
//        RB = GetComponent<Rigidbody>();
//        RB.useGravity = true;
//        CharacterController = GetComponent<CharacterController>();
//        EntertainmentManager.Instance.OutOfCombat += Instance_OnOutOfCombat;
//        EntertainmentManager.Instance.InCombat += Instance_OnInCombat;
//    }


//    private void Instance_OnInCombat(object sender, EventArgs e)
//    {
//        _outOfCombat = true;
//        Debug.Log("OutOfCombat");
//    }


//    private void Instance_OnOutOfCombat(object sender, EventArgs e)
//    {
//        Debug.Log("InCombat");
//        _outOfCombat = false;
//    }


//    protected virtual void Update()
//    {
//        RB.useGravity = true;
//        OnGround = Physics.Raycast(_groundCheck.position, Vector3.down, 0.2f);
//        _distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
//        //_rb.AddForce(Vector3.down * _rb.mass * 9.81f, ForceMode.Force);

//        //TimeSinceLastAttack += Time.deltaTime;

//        if (CurrentImpairement == Impairement.none || CurrentImpairement == Impairement.inAttack)
//        {
//            Vector3 direction = transform.position - transform.position;

//            // Normalize the direction to get a unit vector
//            direction.Normalize();

//            //Rotate the Champion towards the players position

//            transform.LookAt(Player.Instance.transform);

//            //if (_shouldMove)
//            //{
//            //    Rigidbody.velocity = transform.forward * MovementSpeed;
//            //}
//        }
//        else if (CurrentImpairement == Impairement.airborne)
//        {       
//            groundCheckTimer += Time.deltaTime;
//            if (_shouldCheckOnGround)
//            {
//                //_onGround = Physics.Raycast(transform.position, Vector3.down, groundCheck) ;
//                //if (_onGround)
//                //{
//                //    ResetTriggers();
//                //    anim.SetTrigger("Land");
//                //    //CurrentImpairement = Impairement.none;
//                //    groundCheckTimer = 0;
//                //    _shouldCheckOnGround = false;
//                //}

//                if (Physics.Raycast(transform.position, Vector3.down, _landOffsetCheck))
//                {
//                    ResetTriggers();
//                    anim.SetTrigger("Land");
//                    //CurrentImpairement = Impairement.none;
//                    groundCheckTimer = 0;
//                    _shouldCheckOnGround = false;
//                }
//            }
//        }
//    }


//    public void SetAirBorn()
//    {
//        CurrentImpairement = Impairement.airborne;
//    }


//    public void ShouldCheckForGround()
//    {
//        _shouldCheckOnGround = true;
//    }

//    //protected virtual void Update()
//    //{

//    //}

//    public void TakeDamage(Attack attack)
//    {        
//        switch (attack.AttackSO.CurrentAttackEffect)
//        {
//            case CurrentAttackSO.AttackEffect.None:
//                break;

//            case CurrentAttackSO.AttackEffect.Pushback:
//                //TODO: Calculate direction
//                GetPushedback(Player.Instance.transform.position, attack.AttackSO.Force);
//                break;

//            case CurrentAttackSO.AttackEffect.StunAndPushback:
//                //GetStunned(StunDuration, attack.AttackerPosition);
//                GetPushedback(Player.Instance.transform.position, attack.AttackSO.Force);
//                break;

//            case CurrentAttackSO.AttackEffect.Knockup:
//                GetKnockedUp(Player.Instance.transform.position, attack.AttackSO.Force);
//                break;

//            case CurrentAttackSO.AttackEffect.Bleed:
//                //StartBleedCoroutine(attack);
//                break;

//            case CurrentAttackSO.AttackEffect.Stun:
//                GetStunned(attack.AttackSO.StunDuration, Player.Instance.transform.position);
//                break;
//            default:
//                break;
//        }

//        _healthManager.ReduceHealth(attack.Damage);
//    }


//    private void StartBleedCoroutine(Attack attack)
//    {
//        float bleedDamage = attack.Damage * attack.AttackSO.DamageMultiplier; // Temporary, will be replaced by weapon bleed multiplier
//        startBleedTime = Time.time;
//        StartCoroutine(_healthManager.Bleed(bleedDamage, startBleedTime));
//    }


//    protected void GetStunned(float stunDuration, Vector3 attackerPos)
//    {
//        _stunDuration = stunDuration;

//        CurrentImpairement = Impairement.stunned;

//        Vector3 pos = transform.position;
//        pos = new Vector3(pos.x, pos.y + transform.localScale.y + 1, pos.z);
//        //Instantiate(stunEffect, pos, Quaternion.Euler(-90, 0, 0), transform);


//        //ParticleSystemManager.Instance.PlayStunEffect(pos, Quaternion.Euler(-90, 0, 0), transform);
//        //ParticleSystemManager.Instance.PlayShockWaveEffect(attackerPos);
//    }


//    protected void GetKnockedUp(Vector3 attackerPos, float force)
//    {
//        RB.AddForce(Vector3.up * force, ForceMode.Impulse);
//        PushBack(attackerPos, force/4);
//        //CurrentImpairement = Impairement.airborne;
//        ResetTriggers();
//        anim.SetTrigger("KnockUp");
//    }


//    protected void GetPushedback(Vector3 attackerPos, float knockBackForce)
//    {
//        PushBack(attackerPos, knockBackForce);
//        CurrentImpairement = Impairement.pushed;
//        //_isImpaired = true;
//        Debug.Log(this.GetType().ToString() + "Enemy knocked back with force: " + knockBackForce);
//        ResetTriggers();
//        Anim.SetTrigger("PushedBack");      
//    }


//    protected void PushBack(Vector3 attackerPos, float knockBackForce)
//    {
//        Vector3 knockbackDirection = (transform.position - attackerPos).normalized;
//        RB.AddForce(knockbackDirection * knockBackForce, ForceMode.Impulse);
//    }


//    public void TriggerGetUpAnim()
//    {
//        ResetTriggers();
//        anim.SetTrigger("GetUp");
//    }


//    public virtual void ResetTriggers()
//    {
//        Anim.ResetTrigger("Walking");
//        Anim.ResetTrigger("LightAttack");
//        Anim.ResetTrigger("HeavyAttack");
//        Anim.ResetTrigger("Stunned");
//        Anim.ResetTrigger("PushedBack");
//        Anim.ResetTrigger("KnockUp");
//        Anim.ResetTrigger("Land");
//        Anim.ResetTrigger("Taunt");
//    }


//    protected virtual void OnAttack()
//    {
//        RegisterAttack?.Invoke(this, new OnAttackPressedEventArgs { CurrentAttackSO = _attackSOArray[0], weaponSO = _weapon });                       
//    }


//    protected virtual void ExitAttackAnimEvent()
//    {
//        // Check if stunned?
//        ResetTriggers();
//        CurrentImpairement = Impairement.none;
//    }


//    protected virtual void OnDestroy()
//    {
//        EntertainmentManager.Instance.OutOfCombat -= Instance_OnOutOfCombat;
//        EntertainmentManager.Instance.InCombat -= Instance_OnInCombat;
//    }
//}