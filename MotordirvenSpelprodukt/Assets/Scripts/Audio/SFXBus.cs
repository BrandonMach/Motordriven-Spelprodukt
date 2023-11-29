using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXBus : MonoBehaviour
{
    FMOD.Studio.Bus bus;

    [SerializeField]
    [Range(-80f, 10f)]
    private float busVolume;

    public Slider volumeSlider;


    // Start is called before the first frame update
    void Start()
    {
        bus = FMODUnity.RuntimeManager.GetBus("bus:/SFXBus");

        if (volumeSlider != null)
        {
            volumeSlider.value = busVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (volumeSlider != null)
        {
            busVolume = volumeSlider.value;
            bus.setVolume(DecibelToLinear(busVolume));
        }
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }
}
