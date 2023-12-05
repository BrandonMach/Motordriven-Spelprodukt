using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.EventSystems.EventTrigger;


public class HealthManager : MonoBehaviour,IHasProgress
{
    [SerializeField] float _maxHealthPoints;
    public float MaxHP { get => _maxHealthPoints; }
    [SerializeField] private float _bleedDuration = 6.0f;
    public float CurrentHealthPoints { get; private set; }
    [SerializeField] float _currentHealth;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public System.EventHandler OnShakeScreen;

    [Header("Dismembrent")]
    //private DismemberentEnemyScript _dismembrentScript;
    public bool HasDismembrent;

    private bool isBleeding;
    public bool IsDeadOnce;
    public bool Dead;
    public float _destroydelay = 1.5f;

    public bool IsPlayer;
    public bool GodMode;
    public bool Explode;

    public bool hasSlowMo;
    public SlowMo _slowMo;

 

    public System.EventHandler PlayReciveDamageSoundEvent;
    public System.EventHandler PlayDeathSoundEvent;
    public System.EventHandler PlayDoDamageSoundEvent;

    [SerializeField] Animator _damageAnimator;

    void Start()
    {
        
        CurrentHealthPoints = _maxHealthPoints;
       
    }

    // Update is called once per frame
    void Update()
    {
        _currentHealth = CurrentHealthPoints;
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


        if (CurrentHealthPoints <= 0)
        {
            Die();

            if (HasDismembrent)
            {
                GetComponent<DismemberentEnemyScript>().DismemberCharacter();
                HasDismembrent = false;
            }

        }
    }

    private void OnDestroy()
    {
        if (!IsPlayer)
        {
            SpawnEnemy.Instance._waveBattleInformation[SpawnEnemy._currentWaveBattleIndex].waveInfoHolder[SpawnEnemy.Instance._currentWaveIndex].EnemiesLeft--;
            GameLoopManager.Instance.UpdateEnemyList();
        }
        
    }


    public IEnumerator Bleed(float bleedDamage, float startBleedTime)
    {
        while ((Time.time - startBleedTime) < _bleedDuration)
        {
            isBleeding = true;
            ReduceHealth(bleedDamage);
            Debug.Log("Health reduced by " + bleedDamage);
            yield return new WaitForSeconds(1.0f);
        }

        if ((Time.time - startBleedTime) > _bleedDuration)
        {
            isBleeding = false;
        }
    }


    public void HealDamage(float damageHealed)
    {
        CurrentHealthPoints += damageHealed;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = CurrentHealthPoints /*/ _maxHealthPoints*/ });

        if (CurrentHealthPoints > _maxHealthPoints) //No Overheal
        {

            CurrentHealthPoints = _maxHealthPoints;
        }
    }


    public void ReduceHealth(float damage)
    {
        EntertainmentManager.Instance.firstTimeInCombat = true;
        if (!GodMode)
        {
            if (IsPlayer)
            {

                OnShakeScreen?.Invoke(this, EventArgs.Empty);
                
            }
            else if (!IsPlayer && !isBleeding)
            {

                _damageAnimator.SetTrigger("DamageBlink");
                PlayDoDamageSoundEvent?.Invoke(this, EventArgs.Empty);
                PlayReciveDamageSoundEvent?.Invoke(this, EventArgs.Empty);

                //PlayRandomMinionHit();
                //PlayRandomHitSound();

                //FMODSFXController.Instance.PlayMinionHit();
            }


            CurrentHealthPoints -= damage;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = CurrentHealthPoints /*/ _maxHealthPoints*/ });


            
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
        if (IsPlayer)
        {
            GameLoopManager.Instance.volumeProfile.TryGet(out ColorAdjustments colorAdjustments);

            colorAdjustments.saturation.value = -100;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            GameLoopManager.Instance.TotalDeaths++;
        }
        else if (!IsDeadOnce)
        {
            //Only increase killcount on npc
            GameLoopManager.Instance.KillCount++;
            Debug.Log("Killcount: " + GameLoopManager.Instance.KillCount);
            IsDeadOnce = true;
           // PlayRandomDeathSound();
            PlayDeathSoundEvent?.Invoke(this, EventArgs.Empty);
        }

        if (hasSlowMo)
        {
            _slowMo.DoSlowmotion();//Only do slow mo when you kill Champion
        }
        
        Dead = true;

    }

   

}
