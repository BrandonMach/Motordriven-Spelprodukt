using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPScript : EnemyScript
{
    

    public float DamageBuff;

    public float ChaseDistance;

    public Animator Anim;

    public int AttackIndex;

    public bool CanAttack;

    void Start()
    {
        _movementSpeed = 10;
        _attackRange = 4;
        _attackCooldown = 2;
        ChaseDistance = 2;
        _currentHealth = 100;
        _maxHealth = 100;
        CanAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
