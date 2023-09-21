using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ActionNode : Node
{
    protected Player playerScript;
    protected GameObject player;
    protected GameObject enemy;
    //protected MinionScript minionScript;

    public void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("EnemyTesting");
        //minionScript.GetComponent<MinionScript>().enabled = true;
        
    }
}
