using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour,IHasProgress, IDamagable
{
    public float MaxHealthPoints;
    public float CurrentHealthPoints;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [Header("Dismembrent")]
    private DismemberentEnemyScript _dismembrentScript;
    public bool HasDismembrent;

    public bool Dead;
    public float _destroydelay = 2.5f;

    public bool GodMode;

    public bool hasSlowMo;
    public SlowMo _slowMo;
    
    void Start()
    {

        CurrentHealthPoints = MaxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GodMode && Dead)
        {
            _destroydelay -= Time.deltaTime;
            if(_destroydelay <= 0)
            {
                
                Destroy(gameObject);
                
            }
        }
    }

    public void TakeDamage(float damage, Vector3 hitDirection, float knockBackForce)
    {
        //CurrentHealthPoints -= damage;
        //if(this.gameObject.tag == "Player")
        //{
        //    PlayerKnockback _playerKnockback = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerKnockback>();
        //    _playerKnockback.Knockback(hitDirection, knockBackForce);
        //    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs { progressNormalized = CurrentHealthPoints/MaxHealthPoints});
            
        //}
       
    }

    public void HealDamage(float damageHealed)
    {
        CurrentHealthPoints += damageHealed;

        if(CurrentHealthPoints > MaxHealthPoints) //No Overheal
        {
            CurrentHealthPoints = MaxHealthPoints;
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealthPoints -= damage;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = CurrentHealthPoints / MaxHealthPoints });


        if (CurrentHealthPoints <= 0)
        {
            if (HasDismembrent)
            {
                _dismembrentScript = GetComponent<DismemberentEnemyScript>();
                _dismembrentScript.GetKilled();
            }

            Die();
        }
    }

    public void GetStunned(float stunDuration)
    {
        if (this.gameObject.tag == "EnemyTesting")
        {
            this.gameObject.GetComponent<EnemyScript>().Stunned = true;
            this.gameObject.GetComponent<EnemyScript>().StunDuration = stunDuration;
            

        }
    }

    public void Die()
    {
        if (hasSlowMo)
        {
            _slowMo.DoSlowmotion();//Only do slow mo when you kill Champion
        }
        
        Dead = true;

        GameManager.Instance.KillCount++;
        Debug.Log("Killer count: " + GameManager.Instance.KillCount);

    }

    public void Knockback(Vector3 hitDirection, float knockBackForce)
    {
        if (this.gameObject.tag == "Player")
        {
            PlayerKnockback _playerKnockback = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerKnockback>();
            _playerKnockback.Knockback(hitDirection, knockBackForce);
           
        }
        
    }
}
