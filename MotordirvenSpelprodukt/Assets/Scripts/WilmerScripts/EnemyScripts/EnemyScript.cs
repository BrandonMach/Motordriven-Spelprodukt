using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamagable, ICanAttack
{
    [SerializeField] protected HealthManager _healthManager;
    [SerializeField] private CurrentAttackSO[] _attackSOArray;
    [SerializeField] private Weapon _weapon;


    protected float _movementSpeed;
    protected float _attackSpeed; //?
    protected float _attackRange;
    protected float _attackCooldown;
    protected float _lastAttackTime;
    protected float _stunDuration;
    protected bool _stunned;
    private ParticleSystem stunEffect;


    public event EventHandler<OnAttackPressedEventArgs> OnRegisterAttack;


    public float MovementSpeed { get { return _movementSpeed; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float AttackRange { get { return _attackRange; } }
    public float StunDuration { get { return _stunDuration; } set { _stunDuration = value; } }
    public float AttackCooldown { get { return _attackCooldown; } }
    public float LastAttackTime { get { return _lastAttackTime; } set { _lastAttackTime = value; } }
    public bool Stunned { get { return _stunned; } set { _stunned = value; } }
    

    // Start is called before the first frame update
    void Start()
    {

        //if (HasDismembrent)
        //{
        //    _dismembrentScript = GetComponent<DismemberentEnemyScript>();
        //}
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, Attack attack)
    {
        _healthManager.ReduceHealth(damage);
        switch (attack.AttackSO.CurrentAttackEffect)
        {
            case CurrentAttackSO.AttackEffect.None:
                float knockbackForce = 10f;
                GetKnockedback(attack.Position, knockbackForce);
                //enemy.KnockUp(knockbackForce);
                break;
            case CurrentAttackSO.AttackEffect.Pushback:
                //TODO: Calculate direction

                break;
            case CurrentAttackSO.AttackEffect.StunAndPushback:
                break;
            case CurrentAttackSO.AttackEffect.Knockup:
                break;
            case CurrentAttackSO.AttackEffect.Bleed:
                break;
            case CurrentAttackSO.AttackEffect.Stun:
                float knockUpForce = 15;
                //enemy.KnockUp(knockUpForce);
                //HandelStun(enemy);
                break;
            default:
                break;
        }

        
    }

    protected void GetKnockedup(float force)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * force, ForceMode.Impulse);
    }

    protected void GetKnockedback(Vector3 attackerPos, float knockBackForce)
    {
        Vector3 knockbackDirection = (transform.position - attackerPos).normalized;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(knockbackDirection * knockBackForce, ForceMode.Impulse);
        Debug.Log("Knockback Force: " + knockBackForce);
    }

    protected virtual void OnAttack()
    {
        OnRegisterAttack?.Invoke(this, new OnAttackPressedEventArgs { CurrentAttackSO = _attackSOArray[0], weaponSO = _weapon });
    }
}