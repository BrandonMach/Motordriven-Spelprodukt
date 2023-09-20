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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = _playerPos.position /*+ new Vector3(0, 10, 0)*/;

        if (_etManager.GetETP() > _etManager.GetETPThreshold() && !_playCheering) //Bool för att den bara sla spelas en gång
        {
            _playCheering = true;
            PlayCheer();
        }
        if(_etManager.GetETP() < _etManager.GetETPThreshold() && !_playBooing)
        {
            _playBooing = true;
            PlayBooo();
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(fallingObject, _playerPos.position + new Vector3(0, 10, 0), transform.rotation);
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
