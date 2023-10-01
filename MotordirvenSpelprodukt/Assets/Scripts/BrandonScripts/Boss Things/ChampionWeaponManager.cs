using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionWeaponManager : MonoBehaviour
{
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
        if(other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            Debug.Log("Player got hit buy ChampionAttack");
            other.GetComponent<HealthManager>().TakeDamage(10);

        }
    }
}
