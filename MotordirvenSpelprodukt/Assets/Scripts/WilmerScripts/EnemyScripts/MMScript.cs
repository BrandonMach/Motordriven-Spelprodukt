using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System;

public class MMScript : MinionScript
{
    // Start is called before the first frame update

    public float ChaseDistance;
    

   

    [Header("Attacks")]
    [SerializeField] private Transform _weaponHolderTransform;



    public float Damage;
    public float ETPDecreaseValue;
    //public bool CanChase;

    HealthManager healthManager;

    #region SFX

    [Header("SFX EventReferences")]

    public EventReference minionHitEventPath;
    public EventReference minionHit2EventPath;
    public EventReference minionHit3EventPath;

    public EventReference hitSoundEventPath;
    public EventReference hitSound2EventPath;
    public EventReference hitSound3EventPath;

    public EventReference deathSoundEventPath;
    public EventReference deathSound2EventPath;
    public EventReference deathSound3EventPath;
    public EventReference deathSound4EventPath;
    public EventReference deathSound5EventPath;
    #endregion


    protected override void Start()
    {
        base.Start();

        AttackRange = 4f;
        //_timeBetweenAttacks = 2;
        ChaseDistance = 4f;
     
        Anim = GetComponent<Animator>();
        healthManager = GetComponent<HealthManager>();
        healthManager.PlayReciveDamageSoundEvent += PlayRandomReciveDamageSound;
        healthManager.PlayDoDamageSoundEvent += PlayRandomDoDamageSound;
        healthManager.PlayDeathSoundEvent += PlayRandomDeathSound;

        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckCanMove("Heavy Attack");
        CheckCanMove("Light Attack");
        CheckCanMove("MMHit");
        //CheckCanMove("Pushed Back");
        //CheckCanMove("Stand Up");
        //CheckCanMove("Knock Up");
        //CheckCanMove("Stunned");
        //_rb.AddForce(Vector3.down * _rb.mass * 9.81f, ForceMode.Force);
    }

    //protected override void OnAttack()
    //{
    //    base.OnAttack();
    //}

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }


    protected override void ResetTriggers()
    {
        // Call ResetTriggers on MinionScript
        base.ResetTriggers();


        // Only for MM minion
        Anim.ResetTrigger("LightAttack");
        Anim.ResetTrigger("HeavyAttack");
    }

    protected override void HandleAttack()
    {
        // Call base and apply local attack logic.
        base.HandleAttack();

        int randomValue = Mathf.FloorToInt(UnityEngine.Random.Range(1.0f, 3.0f));
        if (randomValue == 1)
        {
            Anim.SetTrigger("LightAttack");
        }
        else
        {
            Anim.SetTrigger("HeavyAttack");
        }
    }
    //public override void TakeDamage(Attack attack)
    //{
    //    base.TakeDamage(attack);
    //    CurrentState = EnemyState.Hit;
    //}



    #region FmodSFX

    public void PlayDeathSound(EventReference deathSoundRef)
    {
        if (!deathSoundRef.IsNull)
        {
            FMOD.Studio.EventInstance deathSound = FMODUnity.RuntimeManager.CreateInstance(deathSoundRef);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(deathSound, this.transform, this.GetComponent<Rigidbody>());
            deathSound.start();
            deathSound.release();
        }
    }

    public void PlayHitSound(EventReference hitSoundRef)
    {
        if (!hitSoundRef.IsNull)
        {
            FMOD.Studio.EventInstance hitSound = FMODUnity.RuntimeManager.CreateInstance(hitSoundRef);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(hitSound, this.transform, this.GetComponent<Rigidbody>());
            hitSound.getVolume(out float volume);
            hitSound.setVolume(volume / 3);
            hitSound.start();
            hitSound.release();
        }
    }


    public void PlayMinionHit(EventReference minionHitRef)
    {
        if (!minionHitRef.IsNull)
        {
            FMOD.Studio.EventInstance minionHit = FMODUnity.RuntimeManager.CreateInstance(minionHitRef);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(minionHit, this.transform, this.GetComponent<Rigidbody>());
            minionHit.start();
            minionHit.release();
        }

    }

    public void PlayRandomReciveDamageSound(object sender, EventArgs e)
    {
        int randomNumber = UnityEngine.Random.Range(1, 6);

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

    public void PlayRandomDeathSound(object sender, EventArgs e)
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


    public void PlayRandomDoDamageSound(object sender, EventArgs e )
    {
        int randomNumber = UnityEngine.Random.Range(1, 4);

        if (randomNumber == 1)
        {
            PlayHitSound(hitSoundEventPath);
        }
        else if (randomNumber == 2)
        {
            PlayHitSound(hitSound2EventPath);
        }
        else if (randomNumber == 3)
        {
            PlayHitSound(hitSound3EventPath);
        }
    }

    #endregion


    

}
