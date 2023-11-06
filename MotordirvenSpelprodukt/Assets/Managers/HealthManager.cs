using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class HealthManager : MonoBehaviour,IHasProgress
{
    [SerializeField] float _maxHealthPoints;
    [SerializeField] private float _bleedDuration = 6.0f;
    public float CurrentHealthPoints { get; private set; }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [Header("Dismembrent")]
    private DismemberentEnemyScript _dismembrentScript;
    public bool HasDismembrent;

    public bool Dead;
    public float _destroydelay = 2.5f;
    
    public bool GodMode;
    public bool Explode;

    public bool hasSlowMo;
    public SlowMo _slowMo;


    void Start()
    {

        CurrentHealthPoints = _maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GodMode && Dead)
        {
            if (Explode)
            {
                Destroy(gameObject);
            }
            else
            {
                _destroydelay -= Time.deltaTime;
                if (_destroydelay <= 0)
                {
                    Destroy(gameObject);
                    
                }
            }
            
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.UpdateEnemyList();
    }


    public IEnumerator Bleed(float bleedDamage, float startBleedTime)
    {
        while ((Time.time - startBleedTime) < _bleedDuration)
        {
            ReduceHealth(bleedDamage);
            Debug.Log("Health reduced by " + bleedDamage);
            yield return new WaitForSeconds(1.0f);
        }
    }


    public void HealDamage(float damageHealed)
    {
        CurrentHealthPoints += damageHealed;

        if(CurrentHealthPoints > _maxHealthPoints) //No Overheal
        {
            CurrentHealthPoints = _maxHealthPoints;
        }
    }


    public void ReduceHealth(float damage)
    {
        CurrentHealthPoints -= damage;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = CurrentHealthPoints / _maxHealthPoints });


        if (CurrentHealthPoints <= 0)
        {
            if (HasDismembrent)
            {
                _dismembrentScript = GetComponent<DismemberentEnemyScript>();
                _dismembrentScript.GetKilled();
            }


            //if the killing blow remove healthbar
            Die();
        }
    }

    //public void GetStunned(float stunDuration)
    //{
    //    if (this.gameObject.tag == "EnemyTesting")
    //    {
    //        this.gameObject.GetComponent<EnemyScript>().Stunned = true;
    //        this.gameObject.GetComponent<EnemyScript>().StunDuration = stunDuration;
    //    }
    //}

    public void Die()
    {
        
        if (hasSlowMo)
        {
            _slowMo.DoSlowmotion();//Only do slow mo when you kill Champion
        }
        
        Dead = true;

        GameManager.Instance.KillCount++;
        Debug.Log("Killcount: " + GameManager.Instance.KillCount);



    }

  
}
