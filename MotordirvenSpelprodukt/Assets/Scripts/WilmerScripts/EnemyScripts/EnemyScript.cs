using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamagable, ICanAttack
{
    [SerializeField] protected HealthManager _healthManager;
    [SerializeField] private CurrentAttackSO[] _attackSOArray;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private ParticleSystem stunEffect;

    protected float _movementSpeed;
    protected float _attackSpeed; //?
    protected float _attackRange;
    protected float _attackCooldown;
    protected float _lastAttackTime;
    protected float _stunDuration;
    protected bool _canChase;
    protected bool _onGround;
    protected bool _impaired;
    protected bool _isAttacking;

    private float startBleedTime;
    

    public event EventHandler<OnAttackPressedEventArgs> OnRegisterAttack;


    public float MovementSpeed { get { return _movementSpeed; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float AttackRange { get { return _attackRange; } }
    public float StunDuration { get { return _stunDuration; } set { _stunDuration = value; } }
    public float AttackCooldown { get { return _attackCooldown; } }
    public float LastAttackTime { get { return _lastAttackTime; } set { _lastAttackTime = value; } }
    public bool CanChase { get { return _canChase; } set { _canChase = value; } }
    public bool Impaired { get { return _impaired; } set { _impaired = value; } }
    public bool OnGround { get { return _onGround; } set { _onGround = value; } }



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
                float knockUpForce = 15;
                GetKnockedUp(knockUpForce);
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
        _canChase = true;
        _stunDuration = stunDuration;

        //enemy.TakeDamage(2.0f, new Attack { AttackEffect = , Position = transform.position });
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x, pos.y + transform.localScale.y + 1, pos.z);
        Instantiate(stunEffect, pos, Quaternion.Euler(-90, 0, 0), transform);
    }


    protected void GetKnockedUp(float force)
    {
        _canChase = true;
        _onGround = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * force, ForceMode.Impulse);
    }

    protected void GetPushedback(Vector3 attackerPos, float knockBackForce)
    {
        _canChase = true;
        _impaired = true;
        Vector3 knockbackDirection = (transform.position - attackerPos).normalized;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(knockbackDirection * knockBackForce, ForceMode.Impulse);
        Debug.Log(this.GetType().ToString() + "Enemy knocked back with force: " + knockBackForce);
    }


    protected virtual void OnAttack()
    {
        OnRegisterAttack?.Invoke(this, new OnAttackPressedEventArgs { CurrentAttackSO = _attackSOArray[0], weaponSO = _weapon });                       
    }
}