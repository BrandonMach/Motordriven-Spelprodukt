using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamekazeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    int tempDamage = 20;
    
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
            KMScript _kmScript = gameObject.GetComponentInParent<KMScript>();
            _kmScript.PlayerInpact = true;
            Instantiate(_kmScript._explosion, transform.parent);
            //other.GetComponent<HealthManager>().TakeDamage(tempDamage, new Attack { AttackEffect = CurrentAttackSO.AttackEffect.} );
            //this.GetComponentInParent<HealthManager>().TakeDamage(100);
           
        }
        
    }
}
