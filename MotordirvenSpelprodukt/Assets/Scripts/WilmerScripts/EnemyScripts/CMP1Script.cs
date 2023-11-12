using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMP1Script : ChampionScript
{
    public enum ChampionState { Enter, Taunt, SpecialAttack, BasicAttack }
    public ChampionState CurrentState = ChampionState.Enter;
    public ChampionState PreviousState = ChampionState.Enter;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        AttackRange = 5;
        Anim = GetComponent<Animator>();
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

    public void EnterTauntState()
    {
        CurrentState = ChampionState.Taunt; 
    }

    //Reset triggers method
}
