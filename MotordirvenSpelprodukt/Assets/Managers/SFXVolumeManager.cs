using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeManager : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        // Find the persistent FMODController
        FMODSFXController fmodSFXController = FindObjectOfType<FMODSFXController>();

        if (fmodSFXController != null)
        {
            // Use the FMODController to set the initial volume
            SetVolume(fmodSFXController.GetVolume());
        }
    }

    public void SetVolume(float volume)
    {
        // Update the volume slider
        if (volumeSlider != null)
        {
            volumeSlider.value = volume;
        }

        // Find the persistent FMODController
        FMODSFXController fmodSFXController = FindObjectOfType<FMODSFXController>();

        if (fmodSFXController != null)
        {
            // Use the FMODController to set the volume
            fmodSFXController.SetVolume(volume);
        }
    }
}
