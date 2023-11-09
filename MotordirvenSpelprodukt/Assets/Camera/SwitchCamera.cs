using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera _vcamPlayer;
    [SerializeField] private CinemachineVirtualCamera _vcamKing;
    [SerializeField] private CinemachineVirtualCamera _vcamExecute;
    [SerializeField] private GameObject _grayTint;

    public GameObject Player;


    private bool playerCamera = true;

    private void Update()
    {      
        if (Input.GetKeyDown(KeyCode.L))
        {
            //SwitchCameraPriority();
            StartCoroutine(ChangeCameraAndDoSomething());
        }
       
    }

 

    public void GoToKingCam()
    {      
        StartCoroutine(ChangeCameraAndDoSomething());
    }
    public void GoToExecute()
    {
        StartCoroutine(executeCam());
    }

    public void SwitchToExecute()
    {
        _vcamPlayer.Priority = 0;
        _vcamKing.Priority = 0;
        _vcamExecute.Priority = 1;
        

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
            
            _grayTint.SetActive(true);
            // cause IsBlending has little bit delay so it's need to wait
            yield return new WaitUntil(() => _cinemachineBrain.IsBlending);

            // wait until blending is finished

            yield return new WaitUntil(() => !_cinemachineBrain.IsBlending);
            _grayTint.SetActive(false);
            Time.timeScale = 1;
        }
    }
    private IEnumerator executeCam()
    {
        Player.transform.position = new Vector3(0, 0, 0);
        Player.transform.rotation = new Quaternion(0, 180, 0, 0);
        Time.timeScale = 0;
        SwitchToExecute();
        // cause IsBlending has little bit delay so it's need to wait
        yield return new WaitUntil(() => _cinemachineBrain.IsBlending);

        // wait until blending is finished

        yield return new WaitUntil(() => !_cinemachineBrain.IsBlending);
        yield return new WaitForSeconds(3);
        
        Time.timeScale = 1;
    }
}
