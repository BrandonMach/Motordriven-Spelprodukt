using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETPArrow : MonoBehaviour
{
    

    [Header("SFX EventReferences")]
    public EventReference whiteDiamondSound;
    public EventReference redDiamondSound;
    public EventReference yellowDiamondSound;
    public EventReference greenDiamondSound;
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
            
            PlayDiamondSounds(whiteDiamondSound);
        }
        else if (other.gameObject.CompareTag("Red Diamond"))
        {
            
            PlayDiamondSounds(redDiamondSound);
            Debug.Log("Red diamond");
        }
        else if (other.gameObject.CompareTag("Green Diamond"))
        {
            PlayDiamondSounds(greenDiamondSound);
            Debug.Log("Green diamond");
        }
        else if (other.gameObject.CompareTag("Yellow Diamond"))
        {
            PlayDiamondSounds(yellowDiamondSound);
            Debug.Log("Yellow diamond");
        }
    }

    private void PlayDiamondSounds(EventReference sound)
    {
        FMOD.Studio.EventInstance soundInstance = FMODUnity.RuntimeManager.CreateInstance(sound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundInstance, this.transform, this.GetComponent<Rigidbody>());
        soundInstance.start();
        soundInstance.release();
    }
}
