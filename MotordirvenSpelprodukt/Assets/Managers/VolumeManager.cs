using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        // Find the persistent FMODController
        FMODController fmodController = FindObjectOfType<FMODController>();

        if (fmodController != null)
        {
            // Use the FMODController to set the initial volume
            SetVolume(fmodController.GetVolume());
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
        FMODController fmodController = FindObjectOfType<FMODController>();

        if (fmodController != null)
        {
            // Use the FMODController to set the volume
            fmodController.SetVolume(volume);
        }
    }
}
