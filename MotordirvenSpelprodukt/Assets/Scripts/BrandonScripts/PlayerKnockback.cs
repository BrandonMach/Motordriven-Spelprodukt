using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{

    public float KnockbackForce;
    public float KnockbackTime;
    private float _knockbackCounter;
    PlayerMovement pMovment;

    void Start()
    {
        pMovment = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_knockbackCounter <= 0) //No knockback
        {
            //Can move input bla bla...Måste prat med Marco om detta
            //Borde vara PlayerMovement script
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

        
    }
}
