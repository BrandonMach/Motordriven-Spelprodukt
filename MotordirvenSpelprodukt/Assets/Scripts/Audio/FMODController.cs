using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class FMODController : MonoBehaviour
{
    [SerializeField] Slider healthSlider; // For testing

    GameManager _gameManager;
    HealthManager _healthManager;

    public FMOD.Studio.EventInstance _fmodEventInstance;


    float _intensity;
    float _health;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;

        _fmodEventInstance = GetComponent<FMODUnity.StudioEventEmitter>().EventInstance;

        

    }

    // Update is called once per frame
    void Update()
    {
        _health = healthSlider.value;
        //Debug.Log(healthSlider.value);

        _fmodEventInstance.setParameterByName("Health", _health);


        // Debug print to check current paramValue for Health
        if (Input.GetKey(KeyCode.Space))
        {
            _fmodEventInstance.getParameterByName("Health", out float changedParamValue);

            Debug.Log($"ChangedParamValue: {changedParamValue}");

            Debug.Log(_fmodEventInstance);
            //Debug.Log(result);
        }
    }



}
