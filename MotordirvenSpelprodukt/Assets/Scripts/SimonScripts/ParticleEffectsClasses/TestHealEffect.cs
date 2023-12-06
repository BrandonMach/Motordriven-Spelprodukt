using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TestHealEffect : MonoBehaviour
{
    //[SerializeField] VisualEffect effect1;
    [SerializeField] ParticleSystem effect2;
    [SerializeField] ParticleSystem effect3;
    [SerializeField] ParticleSystem effect4;
    [SerializeField] ParticleSystem effect5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //effect1.Play();
            effect2.Play();
            effect3.Play();
            effect4.Play();
            effect5.Play();
        }
    }
}
