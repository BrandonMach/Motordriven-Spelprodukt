using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{

    public float KnockbackForce;
    public float KnockbackTime;
    private float _knockbackCounter = 0;
    Player _player;
    PlayerMovement _playerMovment;
    public Rigidbody rb;

    void Start()
    {
        _player = GetComponent<Player>();
        _playerMovment = GetComponent<PlayerMovement>();
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_knockbackCounter > 0) //No knockback
        {
            _player.Knockbacked(_player, EventArgs.Empty);
            _knockbackCounter -= Time.deltaTime;
        }
    }

    public void Knockback(Vector3 direction, float knockbackForce)
    {
        _knockbackCounter = KnockbackTime; //Player cant move
        //pMovment. Videon

       // direction = new Vector3(1, 1, 1);
        //_playerMovment._moveDirection = direction * KnockbackForce;
        rb.AddForce(direction.normalized * knockbackForce, ForceMode.Impulse);
        
    }
}
