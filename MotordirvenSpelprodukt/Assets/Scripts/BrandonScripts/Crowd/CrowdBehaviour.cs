using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdBehaviour : MonoBehaviour
{
    [SerializeField] private EntertainmentManager _etManager;

    [SerializeField] private Transform _playerPos;
    [SerializeField] private AudioSource _cheering;
    [SerializeField] private AudioSource _booing;

    public Rect fallingArea;
    public GameObject fallingObject;


    bool _playCheering;
    bool _playBooing;


    public enum CrowdEmotion
    {
        Normal,
        Excited,
        Angry
    }
    public CrowdEmotion Emotion;
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        fallingArea = new Rect(0, 3, 10, 2);
        this.transform.position = _playerPos.position /*+ new Vector3(0, 10, 0)*/;

        if (_etManager.GetETP() > _etManager.GetExcitedThreshold() && !_playCheering) //Bool för att den bara sla spelas en gång
        {
            _playCheering = true;
            Emotion = CrowdEmotion.Excited;
            //PlayCheer();
        }
        else if(_etManager.GetETP() < _etManager.GetAngryThreshold() && !_playBooing)
        {
            _playBooing = true;
            Emotion = CrowdEmotion.Angry;
            //PlayBooo();
        }
        else
        {
            _playCheering = false;
            _playBooing = false;
            Emotion = CrowdEmotion.Normal;

        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            float randonThrowPosX = Random.Range(-10, 10);
            float randonThrowPosY = Random.Range(-10, 10);
            Instantiate(fallingObject, _playerPos.position + new Vector3(randonThrowPosX, 10, randonThrowPosY), transform.rotation);
        }
        

    }

    void PlayCheer()
    {
        _cheering.Play();
    }

    void PlayBooo()
    {
        _booing.Play();
    }

}
