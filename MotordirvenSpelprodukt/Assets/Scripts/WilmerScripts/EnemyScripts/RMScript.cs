using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMScript : EnemyScript
{
    // Start is called before the first frame update
    public float FleeDistance;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDestroy()
    {
        //Death animation
    }

}
