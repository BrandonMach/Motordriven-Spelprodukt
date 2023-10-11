using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMScript : EnemyScript
{
    public float ChaseDistance;
    void Start()
    {
        _movementSpeed = 5; //Ramp up speed kan testas
        _attackRange = 0.2f; //?? kanske inte  behövs
        _attackCooldown = 2; //Kanske inte behövs
        ChaseDistance = 2;
        _currentHealth = 100;
        _maxHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
