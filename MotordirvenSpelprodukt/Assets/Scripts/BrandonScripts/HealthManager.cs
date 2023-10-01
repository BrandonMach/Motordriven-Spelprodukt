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

    public void TakeDamage(float damage)
    {
        CurrentHealthPoints -= damage;
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
