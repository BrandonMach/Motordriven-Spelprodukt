using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Unity.Mathematics;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UIElements;

public class GolemCMPScript : ChampionScript
{
    public string ChampionName;
    private bool _shouldCheckForGround;
    private bool hasTakenDamage;

    public enum ChampionState { Enter, Taunt, SpecialAttack, BasicAttack, None }
    public ChampionState CurrentState = ChampionState.Enter;
    public ChampionState PreviousState = ChampionState.Enter;

    [SerializeField] protected CurrentAttackSO _jumpAttack;

    protected static string _jumpAttackStringKey = "jumpAttack";

    [Header("SFX EventReferences")]
    public EventReference tauntScreamEventPath;
    public EventReference championSlamEventPath;
    public EventReference championFirstHitEventPath;
    public EventReference championSecondHitEventPath;
    public EventReference championThirdHitEventPath;
    public EventReference championTakeDamage1;
    public EventReference championTakeDamage2;
    public EventReference hitSoundEventPath;
    public EventReference hitSound2EventPath;
    public EventReference hitSound3EventPath;

    HealthManager _healthManager;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AttackRange = 5;
        Anim = GetComponent<Animator>();

        _attackSODictionary.Add(_jumpAttackStringKey, _jumpAttack);

        _healthManager = GetComponent<HealthManager>();
        _healthManager.PlayReciveDamageSoundEvent += PlayRecieveDamageSound;
        _healthManager.PlayDoDamageSoundEvent += PlayRandomDoDamageSound;
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (CurrentState != ChampionState.SpecialAttack)
        {

            FacePlayer();
        }
        ///////
        if (_healthManager.Dead)
        {
            DeathAnimation();
        }
        ///////
        switch (CurrentState)
        {
            case ChampionState.Enter:
                //Landing animaiton
                Anim.SetTrigger("Landing");
                break;
            case ChampionState.Taunt:
                //Taunt animation
                Anim.SetTrigger("Taunt");
                break;
            case ChampionState.SpecialAttack:
                //Randomise between special attacks
                //Perform special attack
                Anim.SetTrigger("JumpAttack");
                break;
            case ChampionState.BasicAttack:
                //Perform basic attack combo
                Anim.SetTrigger("BasicCombo1");
                break;
            default:
                break;
        }

        if (_shouldCheckForGround)
        {
            OnGround = Physics.Raycast(_groundCheck.position, Vector3.down, 0.3f);
            if (OnGround)
            {
                ParticleSystemManager.Instance.PlayParticleFromPool
                    (ParticleSystemManager.ParticleEffects.JumpCrack, transform);

                _shouldCheckForGround = false;
            }
        }

        if (CurrentState == ChampionState.SpecialAttack
            && OnGround)
        {

        }
    }


    public override void TakeDamage(Attack attack)
    {
        base.TakeDamage(attack);

        switch (CurrentState) { }
    }


    public void EnterBasicAttackState()
    {
        CurrentState = ChampionState.BasicAttack;

        _currentAttackSO = _attackSODictionary[_normalAttackString];
    }


    public void EnterSpecialState()
    {
        CurrentState = ChampionState.SpecialAttack;
        _currentAttackSO = _attackSODictionary[_jumpAttackStringKey];
    }


    public void EnterTauntState()
    {
        CurrentState = ChampionState.Taunt;
    }


    public void EnterNoneState()
    {
        PreviousState = CurrentState;
        CurrentState = ChampionState.None;
        Anim.ResetTrigger("BasicCombo1");
        Anim.ResetTrigger("JumpAttack");
        Anim.ResetTrigger("Taunt");
        Anim.ResetTrigger("Landing");
    }


    //Reset triggers method
    protected override void OnAttack()
    {
        base.OnAttack();

        if (CurrentState == ChampionState.SpecialAttack || CurrentState == ChampionState.Enter)
        {
            PlayChampionSlam();
            PlayerDamageHUD.Instance.ShakeScreen();

        }
    }


    public void StartCheckForGround()
    {
        _shouldCheckForGround = true;
    }

    public void DeathAnimation()
    {
        Anim.ResetTrigger("BasicCombo1");
        Anim.ResetTrigger("JumpAttack");
        Anim.ResetTrigger("Taunt");
        Anim.SetTrigger("Death");
    }

    #region SFX
    private void PlaySound(EventReference eventReference)
    {
        if (!eventReference.IsNull)
        {
            FMOD.Studio.EventInstance firstHit = FMODUnity.RuntimeManager.CreateInstance(eventReference);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(firstHit, this.transform, this.GetComponent<Rigidbody>());
            firstHit.start();
            firstHit.release();
        }
    }

    private void PlayHurtSound(EventReference eventReference)
    {
        if (!eventReference.IsNull)
        {
            FMOD.Studio.EventInstance firstHit = FMODUnity.RuntimeManager.CreateInstance(eventReference);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(firstHit, this.transform, this.GetComponent<Rigidbody>());
            firstHit.start();
        }
    }

    public void PlayRandomDoDamageSound(object sender, EventArgs e)
    {
        int randomNumber = UnityEngine.Random.Range(1, 4);

        if (randomNumber == 1)
        {
            PlaySound(hitSoundEventPath);
        }
        else if (randomNumber == 2)
        {
            PlaySound(hitSound2EventPath);
        }
        else if (randomNumber == 3)
        {
            PlaySound(hitSound3EventPath);
        }
    }

    private void PlayRecieveDamageSound(object sender, EventArgs e)
    {

        int rand = UnityEngine.Random.Range(1, 3);

        if (rand == 1)
        {
            PlayHurtSound(championTakeDamage1);
        }
        else
        {
            PlayHurtSound(championTakeDamage2);
        }
    }

    private void PlayChampionSlam()
    {
        PlaySound(championSlamEventPath);
    }

    private void PlayTauntScream()
    {
        PlaySound(tauntScreamEventPath);
    }

    private void PlayFirstHitSound()
    {
        PlaySound(championFirstHitEventPath);
    }

    private void PlaySecondHitSound()
    {
        PlaySound(championSecondHitEventPath);
    }

    private void PlayThirdHitSound()
    {
        PlaySound(championThirdHitEventPath);
    }
    #endregion
}
