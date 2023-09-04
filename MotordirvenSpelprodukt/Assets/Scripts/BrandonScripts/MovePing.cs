using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePing : MonoBehaviour
{
    public float timeAlive = 3;
    float startTime = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        if (startTime >= timeAlive)
        {
            Destroy(gameObject);
        }
    }
}
