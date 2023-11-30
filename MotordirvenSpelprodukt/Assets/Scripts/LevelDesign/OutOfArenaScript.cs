using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfArenaScript : MonoBehaviour
{

    public EventReference wilhelmScreamEventRef;

    private void OnTriggerEnter(Collider other)
    {


        if(other.gameObject.GetComponent<HealthManager>() != null)
        {
            other.gameObject.GetComponent<HealthManager>().ReduceHealth(300);
            GameManager.Instance.KnockedOutOfArena++;
            GameLoopManager.Instance.KnockedOutOfArena++;

            Debug.Log("Knocked out of arena: " + GameManager.Instance.KnockedOutOfArena + " " + GameLoopManager.Instance.KnockedOutOfArena);

            PlayWilhelmScream();
        }
    }

    private void PlayWilhelmScream()
    {
        if (!wilhelmScreamEventRef.IsNull)
        {
            FMOD.Studio.EventInstance wilhelmScream = FMODUnity.RuntimeManager.CreateInstance(wilhelmScreamEventRef);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(wilhelmScream, this.transform, this.GetComponent<Rigidbody>());
            wilhelmScream.start();
            wilhelmScream.release();
        }
    }
}
