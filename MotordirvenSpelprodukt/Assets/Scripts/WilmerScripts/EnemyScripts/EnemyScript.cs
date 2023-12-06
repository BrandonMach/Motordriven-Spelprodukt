using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour, IDamagable, ICanAttack
{
    [SerializeField] private float _movementSpeed = 20;
    [SerializeField] private HealthManager _healthManager;
    [SerializeField] private Animator anim;
    [SerializeField] protected Weapon _weapon;
    [SerializeField] protected CurrentAttackSO _normalAttackSO;
    [SerializeField] private bool _onGround = true;
    [SerializeField] protected Transform _groundCheck;
    protected static string _normalAttackString = "normalAttack";

    protected CurrentAttackSO _currentAttackSO;

    protected Dictionary<string, CurrentAttackSO> _attackSODictionary;

    private float startBleedTime;

   


    #region Properties
    public float MovementSpeed 
    { 
        get { return _movementSpeed; } 
        set { _movementSpeed = value; } 
    } 
    public Animator Anim 
    { 
        get { return anim; } 
        set { anim = value; } 
    }
    public bool OnGround
    {
        get { return _onGround; }
        set { _onGround = value; }
    }
    public Rigidbody RB { get; set; }
    public float DistanceToPlayer { get; set; }
    public bool CanMove { get; set; } = true;
    public float AttackRange { get; set; }
    #endregion


    public event EventHandler<OnAttackPressedEventArgs> RegisterAttack;


    protected virtual void Start()
    {
        _attackSODictionary = new Dictionary<string, CurrentAttackSO>();

        _attackSODictionary.Add(_normalAttackString, _normalAttackSO);

        _currentAttackSO = _attackSODictionary[_normalAttackString];

        RB = GetComponent<Rigidbody>();
        RB.useGravity = true;
        AttackRange = _weapon.GetRange();

        
    }


    protected virtual void Update()
    {
        
        RB.AddForce(Vector3.down * RB.mass * 9.81f, ForceMode.Force);    
        if(Player.Instance != null)
        {
            DistanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        }      
    }


    /// <summary>
    /// Rotates towards player. For minions and cmp.
    /// </summary>
    protected void FacePlayer()
    {
        //Vector3 direction = transform.position - transform.position;
        
        if(Player.Instance != null && !_healthManager.Dead)
        {
            Vector3 direction = Player.Instance.transform.position - transform.position;
            direction.y = 0;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            //Rotate the Champion towards the players position
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10);
        }
       
    }


    /// <summary>
    /// 
    /// </summary>
    public virtual void TakeDamage(Attack attack)
    {
        _healthManager.ReduceHealth(attack.Damage);
    }


    /// <summary>
    /// Starts bleed tick. For both minion and cmp.
    /// </summary>
    protected void StartBleedCoroutine(Attack attack)
    {
        float bleedDamage = attack.Damage * attack.AttackSO.DamageMultiplier; // Temporary, will be replaced by weapon bleed multiplier
        startBleedTime = Time.time;
        StartCoroutine(_healthManager.Bleed(bleedDamage, startBleedTime));
    }


    /// <summary>
    /// Event for when enemy attacks player. For both minions and cmp.
    /// </summary>
    protected virtual void OnAttack()
    {
        RegisterAttack?.Invoke(this, new OnAttackPressedEventArgs { CurrentAttackSO = _currentAttackSO, weaponSO = _weapon });
    }


    // For both minions and cmp
    protected void CheckCanMove(string animToCheck)
    {
        AnimatorStateInfo anim = Anim.GetCurrentAnimatorStateInfo(0);

        if (anim.IsName(animToCheck))
        {
            CanMove = false;
        }
        else CanMove = true;


        //if (anim.IsName(animToCheck))
        //{
        //    CanMove = true;
        //    return;
        //}
        //CanMove = false;
    }


    public void EnterAttackAnimEvent()
    {
        //CanMove = false;
    }


}