using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceAnimationScipt : MonoBehaviour
{
    [SerializeField] Animator _anim;
    CrowdBehaviour.CrowdEmotion _emotion;
    void Start()
    {
        _emotion = CrowdBehaviour.CrowdEmotion.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        if(_emotion== CrowdBehaviour.CrowdEmotion.Angry)
        {
            Debug.Log("Angry");
        }
    }
}
