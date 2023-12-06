using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXBus : MonoBehaviour
{
    FMOD.Studio.Bus sfxBus;

    private float busVolume;

    public Slider volumeSlider;


    // Start is called before the first frame update
    void Start()
    {
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFXBus");

        //if (volumeSlider != null)
        //{
        //    volumeSlider.value = busVolume;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxBus.setVolume(volume);
        sfxBus.getVolume(out float asd);

        Debug.Log("SFXbus volume: " + asd);
    }
}
