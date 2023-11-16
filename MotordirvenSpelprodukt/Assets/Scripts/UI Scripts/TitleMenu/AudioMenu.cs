using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMenu : MenuAbstract, IMenu
{

    public AudioMixer audioMixer;
    public FMODController FMODController;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
