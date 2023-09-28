using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera _vcamPlayer;
    [SerializeField] private CinemachineVirtualCamera _vcamKing;


    private bool playerCamera = true;

    private void Update()
    {      
        if (Input.GetKeyDown(KeyCode.L))
        {
            //SwitchCameraPriority();
            StartCoroutine(ChangeCameraAndDoSomething());
        }
       
    }

    void SwitchCameraPriority()
    {
        if (playerCamera)
        {
            _vcamPlayer.Priority = 0;
            _vcamKing.Priority = 1;        
        }
        else
        {           
            _vcamPlayer.Priority = 1;
            _vcamKing.Priority = 0;         
        }
        playerCamera = !playerCamera;
    }


    private IEnumerator ChangeCameraAndDoSomething()
    {
        Time.timeScale = 0;
        SwitchCameraPriority();

        if (playerCamera)
        {
            // cause IsBlending has little bit delay so it's need to wait
            yield return new WaitUntil(() => _cinemachineBrain.IsBlending);

            // wait until blending is finished

            yield return new WaitUntil(() => !_cinemachineBrain.IsBlending);

            Time.timeScale = 1;
        }
       
    }
}
