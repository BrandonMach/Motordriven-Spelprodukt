using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinionScript : EnemyScript
{
    [SerializeField] private ParticleSystem stunEffect;
    
    [SerializeField] private float _landOffsetCheck;

    private bool _shouldCheckOnGround;

    public enum EnemyState { none, stunned, airborne, inAttack, pushed, chasing, fleeing, taunt }
    public EnemyState CurrentState = EnemyState.chasing;
    public EnemyState PreviousState = EnemyState.none;


    #region Properties
    public float StunDuration { get; set; }
    //public bool OutOfCombat { get; set; }

    public float LandOffsetCheck
    {
        get { return _landOffsetCheck; }
        set { _landOffsetCheck = value; }
    }
    #endregion

    protected override void Start()
    {
        // Call Start() on EnemyScript
        base.Start();

        EntertainmentManager.Instance.OutOfCombat += Instance_OnOutOfCombat;
        EntertainmentManager.Instance.InCombat += Instance_OnInCombat;
    }



    protected override void Update()
    {
        // Call Update() on EnemyScript
        base.Update();


        OnGround = Physics.Raycast(_groundCheck.position, Vector3.down, 0.3f);

        switch (CurrentState)
        {
            case EnemyState.none:
                break;

            case EnemyState.stunned:
                HandleStun();
                break;

            case EnemyState.airborne:
                HandleAirborne();
                break;

            case EnemyState.inAttack:
                HandleAttack();
                break;

            case EnemyState.pushed:
                break;

            case EnemyState.chasing:
                HandleChase();
                break;

            case EnemyState.fleeing:
                HandleFleeing();
                break;
            case EnemyState.taunt:
                HandleTaunt();
                break;
            default:
                break;
        }
    }


    #region Events
    /// <summary>
    /// Sets in combat. Only for minions
    /// </summary>
    private void Instance_OnInCombat(object sender, EventArgs e)
    {
        if(CurrentState == EnemyState.chasing || CurrentState == EnemyState.taunt)
        {
            CurrentState = EnemyState.none;
        }
        
        Debug.Log("OutOfCombat");
    }


    /// <summary>
    /// Sets out of combat. Only for minions
    /// </summary>
    private void Instance_OnOutOfCombat(object sender, EventArgs e)
    {
        Debug.Log("InCombat");
        if (CurrentState == EnemyState.chasing)
        {
            CurrentState = EnemyState.taunt;
            ResetTriggers();
            Anim.SetTrigger("Taunt");
        }

    }


    // Event for out/in combat. Only for minions
    protected virtual void OnDestroy()
    {
        EntertainmentManager.Instance.OutOfCombat -= Instance_OnOutOfCombat;
        EntertainmentManager.Instance.InCombat -= Instance_OnInCombat;
    }
    #endregion

    #region Animation Events
    /// <summary>
    /// Animation event when to check for ground. Only for minions.
    /// </summary>
    public void ShouldCheckForGround()
    {
        _shouldCheckOnGround = true;
    }


    /// <summary>
    /// Animation event to trigger get up animaion. Only for minions.
    /// </summary>
    public void TriggerGetUpAnim()
    {
        ResetTriggers();
        Anim.SetTrigger("GetUp");
    }


    /// <summary>
    /// Animation event on end of attack animation. Only for minions.
    /// </summary>
    protected virtual void ExitAttackAnimEvent()
    {
        // Check if stunned?
        ResetTriggers();
        //CanMove = true;
        //PreviousState = CurrentState;
        CurrentState = EnemyState.none;
    }
    #endregion

    #region EnemyState Virtual Methods
    protected virtual void HandleChase()
    {
        ResetTriggers();
        Anim.SetTrigger("Walking");

        if (CanMove)
        {
            RB.velocity = transform.forward * MovementSpeed;
            RB.AddForce(Vector3.down * RB.mass * 9.81f, ForceMode.Force);
            FacePlayer();
        }
    }

    protected virtual void HandleHit()
    {
        ResetTriggers();
        Anim.SetTrigger("Hit");
    }

    protected virtual void HandleTaunt()
    {
        //ResetTriggers();
        //Anim.SetTrigger("Taunt");
    }

    protected virtual void HandleStun()
    {
        //ResetTriggers();
        //Anim.SetTrigger("Stunned");
    }



    protected virtual void HandleAirborne()
    {
        RB.AddForce(Vector3.down * RB.mass * 9.81f, ForceMode.Force);
        if (_shouldCheckOnGround)
        {
            if (Physics.Raycast(transform.position, Vector3.down, _landOffsetCheck))
            {
                ResetTriggers();
                Anim.SetTrigger("Land");
                _shouldCheckOnGround = false;
            }
            else if (OnGround)
            {
                ResetTriggers();
                Anim.SetTrigger("Land");
                _shouldCheckOnGround = false;
            }
        }
    }


    protected virtual void HandleFleeing()
    {
    }


    /// <summary>
    /// Should be overrided by each minion type.
    /// Attack logic is to be added in each class.
    /// </summary>
    protected virtual void HandleAttack()
    {
        FacePlayer();
        ResetTriggers();
        // Call base and add apply attack logic.
    }
    #endregion

    #region Receive Attack Methods
    public override void TakeDamage(Attack attack)
    {
        base.TakeDamage(attack);

        switch (attack.AttackSO.CurrentAttackEffect)
        {
            case CurrentAttackSO.AttackEffect.None:
                GetHit();
                break;

            case CurrentAttackSO.AttackEffect.Pushback:
                GetPushedback(Player.Instance.transform.position, attack.AttackSO.Force);
                break;

            case CurrentAttackSO.AttackEffect.StunAndPushback:
                //GetStunned(StunDuration, attack.AttackerPosition);
                GetPushedback(Player.Instance.transform.position, attack.AttackSO.Force);
                break;

            case CurrentAttackSO.AttackEffect.Knockup:
                GetKnockedUp(Player.Instance.transform.position, attack.AttackSO.Force);
                break;

            case CurrentAttackSO.AttackEffect.Bleed:
                StartBleedCoroutine(attack);
                break;

            case CurrentAttackSO.AttackEffect.Stun:
                GetStunned(attack.AttackSO.StunDuration, Player.Instance.transform.position);
                break;
            default:
                break;
        }
    }



    protected void GetStunned(float stunDuration, Vector3 attackerPos)
    {
        StunDuration = stunDuration;
        if (CurrentState != EnemyState.stunned)
        {
            PreviousState = CurrentState;
        }
        CurrentState = EnemyState.stunned;

        //Vector3 pos = transform.position;
        //pos = new Vector3(pos.x, pos.y + transform.localScale.y + 1, pos.z);
        ResetTriggers();
        Anim.SetTrigger("Stunned");
        //Instantiate(stunEffect, pos, Quaternion.Euler(-90, 0, 0), transform);       //Funkar inte?

        ParticleSystemManager.Instance
            .PlayParticleFromPool(ParticleSystemManager.ParticleEffects.Stun, transform);
    }

    private void GetHit()
    {
        ResetTriggers();
        Anim.SetTrigger("Hit");
    }

    /// <summary>
    /// Adds force to enemy upwards. Only for minions.
    /// </summary>
    protected void GetKnockedUp(Vector3 attackerPos, float force)
    {
        GameLoopManager.Instance.KnockedUpCount++;
        Debug.Log("KnockUps: " + GameLoopManager.Instance.KnockedUpCount);

        RB.AddForce(Vector3.up * force, ForceMode.Impulse);
        PushBack(attackerPos, force / 4);
        if (CurrentState != EnemyState.airborne)
        {
            PreviousState = CurrentState;
        }
        CurrentState = EnemyState.airborne;
        ResetTriggers();
        Anim.SetTrigger("KnockUp");
    }


    /// <summary>
    /// Calls Pushback on enemy. Only for minions.
    /// </summary>
    protected void GetPushedback(Vector3 attackerPos, float knockBackForce)
    {
        PushBack(attackerPos, knockBackForce);
        PreviousState = CurrentState;
        CurrentState = EnemyState.pushed;

        Debug.Log(this.GetType().ToString() + "Enemy knocked back with force: " + knockBackForce);
        ResetTriggers();
        Anim.SetTrigger("PushedBack");
    }


    /// <summary>
    /// Adds force to enemy backwards. Only for minions.
    /// </summary>
    private void PushBack(Vector3 attackerPos, float knockBackForce)
    {
        Vector3 knockbackDirection = (transform.position - attackerPos).normalized;
        RB.AddForce(knockbackDirection * knockBackForce, ForceMode.Impulse);
    }
    #endregion


    protected virtual void ResetTriggers()
    {
        // Call ResetTriggers on EnemyScript
        //base.ResetTriggers();

        // For all minions
        Anim.ResetTrigger("Walking");
        Anim.ResetTrigger("Stunned");
        Anim.ResetTrigger("PushedBack");
        Anim.ResetTrigger("KnockUp");
        Anim.ResetTrigger("Land");
        Anim.ResetTrigger("Taunt");


    }

}