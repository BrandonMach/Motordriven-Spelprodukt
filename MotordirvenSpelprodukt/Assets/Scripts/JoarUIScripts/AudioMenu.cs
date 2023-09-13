using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMenu : MenuAbstract, IMenu
{

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    

    public void ClickESC()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
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
