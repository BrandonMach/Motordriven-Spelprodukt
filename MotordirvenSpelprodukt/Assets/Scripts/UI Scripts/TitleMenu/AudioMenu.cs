using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenu : MenuAbstract, IMenu
{
    public Slider masterVolumeSlider;
    public AudioMixer audioMixer;
    public FMODController FMODController;

    // Start is called before the first frame update
    void Start()
    {
        //FMODController.SetFMOD(GameObject.Find("Transferables").GetComponent<TransferableScript>().GetFMODAM());

        FMODController = FMODController.Instance;

        masterVolumeSlider.value = FMODController.GetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void SetVolume(float volume)
    {
        //audioMixer.SetFloat("volume", volume);
        FMODController.SetVolume(volume);
        Debug.Log("float volume:" + volume);
    }

    public void ClickESC()
    {
        base.ClickESC();
    }
    public override void ClickBack()
    {
        base.ClickBack();
    }
    public void ClickMenuOption1()
    {
        throw new System.NotImplementedException();
    }

    public void ClickMenuOption2()
    {
        throw new System.NotImplementedException();
    }

    public void ClickMenuOption3()
    {
        _prevMenu.SetActive(true);
        gameObject.SetActive(false);

        Debug.Log(_prevMenu.name + _prevMenu.activeSelf);
    }


}
