using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMScript : EnemyScript
{
    // Start is called before the first frame update

    public float ChaseDistance;
    void Start()
    {
        _movementSpeed = 10;
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
