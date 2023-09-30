using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MMScript : EnemyScript
{
    // Start is called before the first frame update

    public float ChaseDistance;
    void Start()
    {
        _movementSpeed = 5;
        _attackRange = 4;
        _attackCooldown = 2;
        ChaseDistance = 2;
        _currentHealth = 100;
        _maxHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        //Death animation
    }
    

}
