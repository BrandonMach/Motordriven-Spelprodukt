using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    
    [SerializeField] private CinemachineVirtualCamera _vcamKing;
    [SerializeField] private CinemachineVirtualCamera _vcamExecute;
    

    public GameObject Player;


    private bool playerCamera = true;

    
   

    [SerializeField] private CinemachineVirtualCamera[] _vCamsPlayers;

    private void Start()
    {
        

        foreach (var playerCams in _vCamsPlayers)
        {
            playerCams.Priority = 0;
        }

    }

    private void Update()
    {
        if (playerCamera && !PauseMenu.GameIsPaused)
        {
            if (GameManager.Instance.OverTheSholderCamActive)
            {
                _vCamsPlayers[0].Priority = 0;
                _vCamsPlayers[1].Priority = 1;
                

            }
            else
            {
                _vCamsPlayers[1].Priority = 0;
                _vCamsPlayers[0].Priority = 1;
            }
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

        foreach (var playerCams in _vCamsPlayers)
        {
            playerCams.Priority = 0;
        }

        //_vcamPlayer.Priority = 0;
        _vcamKing.Priority = 0;
        _vcamExecute.Priority = 1;
        

    }


    void SwitchCameraPriority()
    {
        if (playerCamera)//Switch to king Cam
        {
            foreach (var playerCams in _vCamsPlayers)
            {
                playerCams.Priority = 0;
            }
           
            _vcamKing.Priority = 1;        
        }
        else //Switch to player cam
        {           
            //_vcamPlayer.Priority = 1;

            if (GameManager.Instance.OverTheSholderCamActive)
            {
                _vCamsPlayers[1].Priority = 1;

            }
            else
            {
                _vCamsPlayers[0].Priority = 1;
            }


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
