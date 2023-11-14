using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class CMP1Script : ChampionScript
{
    public enum ChampionState { Enter, Taunt, SpecialAttack, BasicAttack, None }
    public ChampionState CurrentState = ChampionState.Enter;
    public ChampionState PreviousState = ChampionState.Enter;
    private ArcMotion arcMotion;

    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AttackRange = 5;
        Anim = GetComponent<Animator>();
        arcMotion = GetComponent<ArcMotion>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        FacePlayer();

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
                // (animation not in place)
                Anim.SetTrigger("BasicCombo1");
                break;
            default:
                break;
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
    }
    public void EnterSpecialState()
    {
        CurrentState = ChampionState.SpecialAttack;

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


    public void StartArcMotion()
    {
        arcMotion.EndPoint = Player.Instance.transform;
        arcMotion.journeyLength = Vector3.Distance(arcMotion.startPoint.position, arcMotion.EndPoint.position);
        arcMotion.startTime = Time.time;

        // Enable a flag to start the arc motion
        arcMotion.isRunning = true;
    }
    public void StopArcMotion()
    {

        arcMotion.isRunning = false;
    }
}
