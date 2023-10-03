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
        _movementSpeed = 2;
        _attackRange = 2;
        _attackCooldown = 2;
        ChaseDistance = 1;
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
