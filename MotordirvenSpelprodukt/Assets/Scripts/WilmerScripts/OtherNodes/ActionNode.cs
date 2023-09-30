using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ActionNode : Node
{
    protected Player _playerScript;

    public void OnEnable()
    {
        _playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();

    }
}
