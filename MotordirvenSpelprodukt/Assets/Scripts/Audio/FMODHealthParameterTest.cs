using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FMODHealthParameterTest : MonoBehaviour
{

    [SerializeField] Slider healthSlider;
    [SerializeField] float refillSpeed;
    [SerializeField] bool refilling;


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

            if (healthSlider.value >= 1)
            {
                refilling = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            healthSlider.value = healthSlider.value - 0.1f;
        }
        if (Input.GetMouseButtonDown(1))
        {
            refilling = true;
        }

    }
}
