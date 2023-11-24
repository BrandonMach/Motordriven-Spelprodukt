using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETPArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("White Diamond"))
        {
            Debug.Log("white diamond");
        }
        else if (other.gameObject.CompareTag("Red Diamond"))
        {
            Debug.Log("Red diamond");
        }
        else if (other.gameObject.CompareTag("Green Diamond"))
        {
            Debug.Log("Green diamond");
        }
        else if (other.gameObject.CompareTag("Yellow Diamond"))
        {
            Debug.Log("Yellow diamond");
        }
    }
}
