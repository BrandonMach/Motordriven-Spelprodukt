using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNode : Node
{
    protected GameObject player;
    protected GameObject enemy;

    public void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("EnemyTesting");
    }
}
