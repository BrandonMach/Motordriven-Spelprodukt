using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionWeaponManager : MonoBehaviour
{
    [SerializeField] CMPScript _championScript;
    EntertainmentManager _etpmanager;
    void Start()
    {
        _etpmanager = GameObject.FindGameObjectWithTag("ETPManager").GetComponent<EntertainmentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {         
            

            float kbForce = 50;          
            Vector3 playerPos = other.transform.position;
            other.GetComponent<HealthManager>().ReduceHealth(_championScript.Damage);
            _etpmanager.DecreseETP(_championScript.ETPDecreaseValue);
            Debug.Log("Player got hit buy ChampionAttack");
        }
    }
}
