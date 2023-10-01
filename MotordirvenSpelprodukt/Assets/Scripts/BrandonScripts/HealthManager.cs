using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour,IHasProgress
{
    public float MaxHealthPoints;
    public float CurrentHealthPoints;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

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
            OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs { progressNormalized = CurrentHealthPoints/MaxHealthPoints});
            
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
