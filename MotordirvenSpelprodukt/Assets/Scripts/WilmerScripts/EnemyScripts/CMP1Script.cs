using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMP1Script : EnemyScript
{
    public enum ChampionState { Enter, Taunt, SpecialAttack, BasicAttack }
    public ChampionState CurrentState = ChampionState.Enter;
    public ChampionState PreviousState = ChampionState.Enter;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Face player

        switch (CurrentState)
        {
            case ChampionState.Enter:
                //Landing animaiton
                break;
            case ChampionState.Taunt:
                //Taunt animation
                break;
            case ChampionState.SpecialAttack:
                //Randomise between special attacks
                //Perform special attack
                break;
            case ChampionState.BasicAttack:
                //Perform basic attack combo
                // (animation not in place)
                break;
            default:
                break;
        }
    }

    //private override void TakeDamage(Attack attack)
    //{
    //    base.TakeDamage();
    //}

    //Reset triggers method
}
