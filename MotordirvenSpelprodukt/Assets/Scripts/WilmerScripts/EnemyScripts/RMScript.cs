using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMScript : MinionScript
{
    [SerializeField] private Transform _fireArrowPos;


    public float FleeDistance;

    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {

    }

    public void OnDestroy()
    {
        //Death animation
    }

}
