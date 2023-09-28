using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobeTest : MonoBehaviour
{
    public Slider healthSlider;
    public float refillSpeed;
    public bool refilling;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (refilling)
        {
            healthSlider.value = healthSlider.value < 1 ? healthSlider.value + (refillSpeed * Time.deltaTime) : healthSlider.value;

            if (healthSlider.value >=1)
            {
                refilling = false;
            }
        }

        if (Input.GetMouseButton(0))
        {
            healthSlider.value = healthSlider.value - 0.01f;
        }
        if (Input.GetMouseButton(1))
        {
            refilling = true;
        }
    }
}
