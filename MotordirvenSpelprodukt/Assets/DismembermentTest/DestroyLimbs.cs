using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLimbs : MonoBehaviour
{
    float _timeAlive =2.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeAlive -= Time.deltaTime;
        if (_timeAlive <= 0)
        {
            Destroy(gameObject);
        }
    }
}
