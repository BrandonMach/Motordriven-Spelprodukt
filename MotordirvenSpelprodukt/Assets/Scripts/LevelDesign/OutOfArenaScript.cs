using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfArenaScript : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {


        if(other.gameObject.GetComponent<HealthManager>() != null)
        {
            other.gameObject.GetComponent<HealthManager>().ReduceHealth(300);
            GameManager.Instance.KnockedOutOfArena++;
        }


        
    }
}
