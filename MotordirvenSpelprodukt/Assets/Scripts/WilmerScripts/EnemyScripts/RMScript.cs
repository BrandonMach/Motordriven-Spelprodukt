using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RMScript : MinionScript
{
    [SerializeField] private Transform _fireArrowPos;
    [SerializeField] private float _startFleeRange;
    [SerializeField] private ArrowManager _arrowManager;
    [SerializeField] private float _agentRotationSpeed;
    private NavMeshAgent _navMesh;

    [Header("SFX EventReferences")]
    public EventReference bowLoadEventRef;
    public EventReference bowShootEventRef;


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


    public float StartFleeRange 
    { 
        get { return _startFleeRange; } 
        private set { _startFleeRange = value; } 
    } 


    protected override void Start()
    {
        base.Start();
        AttackRange = 20;
        _navMesh = GetComponent<NavMeshAgent>();
        _navMesh.speed = MovementSpeed;
        _navMesh.angularSpeed = _agentRotationSpeed;
        //_agentRotationSpeed = _navMesh.angularSpeed;
        _arrowManager = GameLoopManager.Instance.gameObject.GetComponent<ArrowManager>();


        healthManager = GetComponent<HealthManager>();
        healthManager.PlayReciveDamageSoundEvent += PlayRandomReciveDamageSound;
        healthManager.PlayDoDamageSoundEvent += PlayRandomDoDamageSound;
        healthManager.PlayDeathSoundEvent += PlayRandomDeathSound;

    }


    protected override void Update()
    {
        if (gameObject.GetComponent<HealthManager>().Dead)
        {
            ResetTriggers();
        }


        base.Update();
        CheckCanMove("Shoot");
        if (CurrentState == EnemyState.fleeing && !gameObject.GetComponent<HealthManager>().Dead)
        {
            _navMesh.enabled = true;
        }
        else _navMesh.enabled = false;
    }


    public void StandupAnimFinished()
    {
        Anim.SetBool("GettingUp", false);
    }


    public void StandUpAnimStarted()
    {
        Anim.SetBool("GettingUp", true);
    }


    protected override void HandleFleeing()
    {
        //base.HandleFleeing();
        
        ResetTriggers();
        Anim.SetTrigger("Backpaddle");
        //Anim.SetTrigger("Walking");
        if (CurrentState == EnemyState.fleeing && CanMove)
        {
            Vector3 dir = transform.position - Player.Instance.transform.position;
            Vector3 goTo = transform.position + dir.normalized * 10;
            _navMesh.SetDestination(goTo);
            FacePlayer();
        }
    }

    protected override void OnDestroy()
    {
        _navMesh.enabled = false;
        base.OnDestroy();
    }


    protected override void ResetTriggers()
    {
        base.ResetTriggers();

        Anim.ResetTrigger("Shoot");
        Anim.ResetTrigger("GettingUp");
        Anim.ResetTrigger("Backpaddle");
    }


    //protected override void HandleChase()
    //{
    //    ResetTriggers();
    //    Anim.SetTrigger("Walking");
    //    _agent.SetDestination(Player.Instance.transform.position);
    //}


    public void FireArrowAnimEvent()
    {
        Attack attack = new Attack
        {
            AttackSO = _attackSODictionary[_normalAttackString],
            Damage = _weapon.GetDamage()
        };

        _arrowManager.FireArrowFromPool(attack, _fireArrowPos, transform.forward);
        PlayBowSound(bowShootEventRef);
    }

    protected override void HandleAttack()
    {
        base.HandleAttack();
        ResetTriggers();
        Anim.SetTrigger("Shoot");
       
    }

    




    #region SFX

    private void PlayBowSound(EventReference bowSoundRef)
    {
        FMOD.Studio.EventInstance bowSound = FMODUnity.RuntimeManager.CreateInstance(bowSoundRef);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(bowSound, this.transform, this.GetComponent<Rigidbody>());
        bowSound.start();
        bowSound.release();
    }


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


    public void PlayRandomDoDamageSound(object sender, EventArgs e)
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
