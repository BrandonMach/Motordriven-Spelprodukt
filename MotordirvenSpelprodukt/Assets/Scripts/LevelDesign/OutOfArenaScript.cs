using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfArenaScript : MonoBehaviour
{

    Type hpClass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject == Player.Instance.gameObject)
        {
            other.gameObject.GetComponent<HealthManager>().ReduceHealth(300);
        }

        //if (other.gameObject.GetType()== hpClass)
        //{
        //    other.gameObject.GetComponent<HealthManager>().Die();
        //}

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<HealthManager>().Dead = true;


        //if (other.gameObject == Player.Instance.gameObject)
        //{
        //    other.gameObject.GetComponent<HealthManager>().Die();
        //}
    }
}
