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
            Vector3 hitDirection = other.transform.position - transform.position;
            float kbForce = 50;

            Debug.Log("Player got hit buy ChampionAttack");

            other.GetComponent<HealthManager>().TakeDamage(_championScript.Damage);
            other.GetComponent<HealthManager>().Knockback(hitDirection, kbForce);
            _etpmanager.DecreseETP(_championScript.ETPDecreaseValue);

        }
    }
}
