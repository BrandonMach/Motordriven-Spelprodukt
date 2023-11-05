using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamagable, ICanAttack
{
    [SerializeField] protected HealthManager _healthManager;
    [SerializeField] private CurrentAttackSO[] _attackSOArray;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private ParticleSystem stunEffect;
    [SerializeField] private Transform sphereCheck;

    [SerializeField] protected float _movementSpeed = 5;
    protected float _attackSpeed; //?
    protected float _attackRange;
    protected float _attackCooldown;
    protected float _lastAttackTime;
    protected float _stunDuration;
    protected float _attackCooldownTimer;
    //protected bool _canChase;
    protected bool _onGround = true;
    protected bool _impaired;
    //[SerializeField]
    protected bool _outOfCombat;

    public Rigidbody Rigidbody { get; protected set; }
    public AIMovement AIMovementScript { get; protected set; }

 
    private float startBleedTime;
    private float groundCheckTimer;

    public event EventHandler<OnAttackPressedEventArgs> RegisterAttack;
    public enum Impairement { none, stunned, airborne, inAttack, pushed }
    public Impairement CurrentImpairement = Impairement.none;

    public float MovementSpeed { get { return _movementSpeed; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float AttackRange { get { return _attackRange; } }
    public float StunDuration { get { return _stunDuration; } set { _stunDuration = value; } }
    public float AttackCooldown { get { return _attackCooldown; } }
    public float AttackCooldownTimer { get { return _attackCooldownTimer; } set { _attackCooldownTimer = value; } }
    public float LastAttackTime { get { return _lastAttackTime; } set { _lastAttackTime = value; } }
    //public bool CanChase { get { return _canChase; } set { _canChase = value; } }
    public bool Impaired { get { return _impaired; } set { _impaired = value; } }
    public bool OnGround { get { return _onGround; } set { _onGround = value; } }
    public bool OutOfCombat { get { return _outOfCombat; } set { _outOfCombat = value; } }

    private void Awake()
    {
        AIMovementScript = GetComponent<AIMovement>();
    }
    protected virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
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

        AttackCooldownTimer += Time.deltaTime;

        if (CurrentImpairement == EnemyScript.Impairement.none && AttackCooldown < AttackCooldownTimer)
        {
            Vector3 direction = transform.position - transform.position;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            //Rotate the Champion towards the players position

            transform.LookAt(Player.Instance.transform);

            this.Rigidbody.velocity = new Vector3(transform.forward.x * MovementSpeed, this.Rigidbody.velocity.y, transform.forward.z * MovementSpeed);

            

        }
        else if (CurrentImpairement == Impairement.airborne)
        {
            
            groundCheckTimer += Time.deltaTime;
            if (groundCheckTimer > 1f)
            {
                
                _onGround = Physics.Raycast(transform.position, Vector3.down, 0.1f);
                if(_onGround)
                {
                    CurrentImpairement = Impairement.none;
                    groundCheckTimer = 0;
                }
            }
        }
        

    }

    public void TakeDamage(Attack attack)
    {        
        switch (attack.AttackSO.CurrentAttackEffect)
        {
            case CurrentAttackSO.AttackEffect.None:
                break;

            case CurrentAttackSO.AttackEffect.Pushback:
                //TODO: Calculate direction
                GetPushedback(attack.Position, attack.AttackSO.Force);
                break;

            case CurrentAttackSO.AttackEffect.StunAndPushback:
                GetStunned(StunDuration);
                GetPushedback(attack.Position, attack.AttackSO.Force);
                break;

            case CurrentAttackSO.AttackEffect.Knockup:
                GetKnockedUp(attack.AttackSO.Force);
                break;

            case CurrentAttackSO.AttackEffect.Bleed:
                StartBleedCoroutine(attack);
                break;

            case CurrentAttackSO.AttackEffect.Stun:
                GetStunned(attack.AttackSO.StunDuration);
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

    protected void GetStunned(float stunDuration)
    {
        _stunDuration = stunDuration;

        CurrentImpairement = Impairement.stunned;

        Vector3 pos = transform.position;
        pos = new Vector3(pos.x, pos.y + transform.localScale.y + 1, pos.z);
        Instantiate(stunEffect, pos, Quaternion.Euler(-90, 0, 0), transform);
    }

    protected void GetKnockedUp(float force)
    {
        Rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        CurrentImpairement = Impairement.airborne;
    }

    protected void GetPushedback(Vector3 attackerPos, float knockBackForce)
    {
        Vector3 knockbackDirection = (transform.position - attackerPos).normalized;
        Rigidbody.AddForce(knockbackDirection * knockBackForce, ForceMode.Impulse);
        CurrentImpairement = Impairement.pushed;
        Debug.Log(this.GetType().ToString() + "Enemy knocked back with force: " + knockBackForce);
    }


    protected virtual void OnAttack()
    {
        RegisterAttack?.Invoke(this, new OnAttackPressedEventArgs { CurrentAttackSO = _attackSOArray[0], weaponSO = _weapon });                       
    }

    protected virtual void OnDestroy()
    {
        EntertainmentManager.Instance.OutOfCombat -= Instance_OnOutOfCombat;
        EntertainmentManager.Instance.InCombat -= Instance_OnInCombat;
    }
}