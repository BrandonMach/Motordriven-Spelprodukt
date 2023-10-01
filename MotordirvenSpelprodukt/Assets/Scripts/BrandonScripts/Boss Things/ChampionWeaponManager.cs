using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionWeaponManager : MonoBehaviour
{
    
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            float kbForce = 500;

            Debug.Log("Player got hit buy ChampionAttack");

            other.GetComponent<HealthManager>().TakeDamage(10, hitDirection,kbForce);
           

        }
    }
}
