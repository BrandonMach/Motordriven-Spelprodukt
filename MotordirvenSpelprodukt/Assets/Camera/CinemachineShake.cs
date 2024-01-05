using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{

    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera _cinemachineVC;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;
        _cinemachineVC = GetComponent<CinemachineVirtualCamera>();
    }

    public  void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemashineBasicMultiChanellPerlin = _cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemashineBasicMultiChanellPerlin.m_AmplitudeGain = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {

        

        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                //Time over
                CinemachineBasicMultiChannelPerlin cinemashineBasicMultiChanellPerlin = _cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemashineBasicMultiChanellPerlin.m_AmplitudeGain = 0; // Mathf.Lerp(startingIntensity, 0f, (1-shakeTimer / shakeTimerTotal));

            }
        }
    }
}
