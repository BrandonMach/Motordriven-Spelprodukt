using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamagable
{
    protected float _currentHealth;
    protected float _maxHealth;
    protected float _movementSpeed;
    protected float _attackSpeed;
    protected float _attackRange;
    protected float _attackCooldown;
    protected float _lastAttackTime;

    public float Currenthealth { get { return _currentHealth; } }
    public float MaxHealth { get { return _maxHealth; } }
    public float MovementSpeed { get { return _movementSpeed; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float AttackRange { get { return _attackRange; } }
    public float AttackCooldown { get { return _attackCooldown; } }
    public float LastAttackTime { get { return _lastAttackTime; } set { _lastAttackTime = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        Debug.Log(this.name + "Took damage from player");
    }

    public void GetPushedBack()
    {

    }

    public void TakeBleedDamage()
    {

    }
}
