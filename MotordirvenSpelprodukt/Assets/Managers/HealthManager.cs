using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private DismemberentEnemyScript _dismembrentScript;
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

    [Header("SFX EventReferences")]
    public EventReference minionHitEventPath;
    public EventReference minionHit2EventPath;
    public EventReference minionHit3EventPath;

    public EventReference bloodSoundEventPath;
    public EventReference bloodSound2EventPath;
    public EventReference bloodSound3EventPath;

    public EventReference deathSoundEventPath;
    public EventReference deathSound2EventPath;
    public EventReference deathSound3EventPath;
    public EventReference deathSound4EventPath;
    public EventReference deathSound5EventPath;

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
    }

    private void OnDestroy()
    {
        GameLoopManager.Instance?.UpdateEnemyList();
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

        if(CurrentHealthPoints > _maxHealthPoints) //No Overheal
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
                PlayRandomMinionHit();
                PlayRandomBloodSound();
            }


            CurrentHealthPoints -= damage;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = CurrentHealthPoints /*/ _maxHealthPoints*/ });


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
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            GameLoopManager.Instance.TotalDeaths++;
        }
        else if (!IsDeadOnce)
        {
            //Only increase killcount on npc
            GameLoopManager.Instance.KillCount++;
            Debug.Log("Killcount: " + GameLoopManager.Instance.KillCount);
            IsDeadOnce = true;
            PlayRandomDeathSound();
        }

        if (hasSlowMo)
        {
            _slowMo.DoSlowmotion();//Only do slow mo when you kill Champion
        }
        
        Dead = true;

    }

    #region FmodSFX

    public void PlayDeathSound(EventReference deathSoundRef)
    {
        FMOD.Studio.EventInstance deathSound = FMODUnity.RuntimeManager.CreateInstance(deathSoundRef);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(deathSound, this.transform, this.GetComponent<Rigidbody>());
        deathSound.start();
        deathSound.release();
    }

    public void PlayBloodSound(EventReference bloodSoundRef)
    {
        FMOD.Studio.EventInstance bloodSound = FMODUnity.RuntimeManager.CreateInstance(bloodSoundRef);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(bloodSound, this.transform, this.GetComponent<Rigidbody>());
        bloodSound.getVolume(out float volume);
        bloodSound.setVolume(volume / 3);
        bloodSound.start();
        bloodSound.release();
    }


    public void PlayMinionHit(EventReference minionHitRef)
    {
        FMOD.Studio.EventInstance minionHit = FMODUnity.RuntimeManager.CreateInstance(minionHitRef);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(minionHit, this.transform, this.GetComponent<Rigidbody>());
        minionHit.start();
        minionHit.release();
    }

    public void PlayRandomMinionHit()
    {
        int randomNumber = UnityEngine.Random.Range(1, 4);

        if (randomNumber == 1)
        {
            PlayMinionHit(minionHitEventPath);
        }
        else if (randomNumber == 2)
        {
            PlayMinionHit(minionHit2EventPath);
        }
        else if (randomNumber == 3)
        {
            PlayMinionHit(minionHit3EventPath);
        }
    }

    public void PlayRandomDeathSound()
    {
        int randomNumber = UnityEngine.Random.Range(1, 6);

        if (randomNumber == 1)
        {
            PlayDeathSound(deathSoundEventPath);
        }
        else if (randomNumber == 2)
        {
            PlayDeathSound(deathSound2EventPath);
        }
        else if (randomNumber == 3)
        {
            PlayDeathSound(deathSound3EventPath);
        }
        else if (randomNumber == 4)
        {
            PlayDeathSound(deathSound4EventPath);
        }
        else if (randomNumber == 5)
        {
            PlayDeathSound(deathSound5EventPath);
        }
    }


    public void PlayRandomBloodSound()
    {
        int randomNumber = UnityEngine.Random.Range(1, 4);

        if (randomNumber == 1)
        {
            PlayBloodSound(bloodSoundEventPath);
        }
        else if (randomNumber == 2)
        {
            PlayBloodSound(bloodSound2EventPath);
        }
        else if (randomNumber == 3)
        {
            PlayBloodSound(bloodSound3EventPath);
        }
    }

    #endregion


}
