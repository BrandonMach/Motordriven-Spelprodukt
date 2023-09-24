using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ActionNode : Node
{
    protected Player playerScript;
    
    //protected GameObject enemy;
    //protected MinionScript minionScript;
    

    public void OnEnable()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();

    }
}
