using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{


    public Color startColor = Color.green;
    public Color endColor = Color.black;
    [Range(0, 10)]
    public float speed = 1;

    Renderer renderer;

    public bool startBlink;
    void Start()
    {
        renderer = GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (startBlink)
        {
            renderer.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
        }
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.CompareTag("EtpArrow"))
    //    {
    //        startBlink = true;
    //    }     
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("EtpArrow"))
    //    {
    //        startBlink = false;
    //    }

    //}
}
