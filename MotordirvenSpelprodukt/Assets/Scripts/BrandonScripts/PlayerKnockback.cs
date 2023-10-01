using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{

    public float KnockbackForce;
    public float KnockbackTime;
    private float _knockbackCounter;
    Player _player;
    PlayerMovement _playerMovment;

    void Start()
    {
        _player = GetComponent<Player>();
        _playerMovment = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_knockbackCounter <= 0) //No knockback
        {
            _player.Knockbacked(_player, EventArgs.Empty);
        }
        else
        {
            _knockbackCounter -= Time.deltaTime;
        }
    }

    public void Knockback(Vector3 direction)
    {
        _knockbackCounter = KnockbackTime; //Player cant move
        //pMovment. Videon

        direction = new Vector3(1, 1, 1);
        _playerMovment._moveDirection = direction * KnockbackForce;
        
    }
}
