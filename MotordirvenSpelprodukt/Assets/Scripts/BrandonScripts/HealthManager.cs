using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float MaxHealthPoints;
    public float CurrentHealthPoints;


    void Start()
    {
       
        CurrentHealthPoints = MaxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage, Vector3 hitDirection, float knockBackForce)
    {
        CurrentHealthPoints -= damage;
        if(this.gameObject.tag == "Player")
        {
            PlayerKnockback _playerKnockback = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerKnockback>();
            _playerKnockback.Knockback(hitDirection, knockBackForce);
        }
       
    }

    public void HealDamage(float damageHealed)
    {
        CurrentHealthPoints += damageHealed;

        if(CurrentHealthPoints > MaxHealthPoints) //No Overheal
        {
            CurrentHealthPoints = MaxHealthPoints;
        }
    }
}
