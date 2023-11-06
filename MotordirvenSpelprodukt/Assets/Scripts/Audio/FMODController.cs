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
    EntertainmentManager _entertainmentManager;

    public EventInstance _fmodEventInstance;


    float _intensity;
    float _health;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _entertainmentManager = EntertainmentManager.Instance;

        _fmodEventInstance = GetComponent<FMODUnity.StudioEventEmitter>().EventInstance;


        _health = 1;
        _intensity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIntensityParameter();
        UpdateHealthParameter();
        //Debug.Log("Float etp: " + _intensity);

        // Debug print to check current paramValue for fmodproject intensity
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _intensity = 0;
            _fmodEventInstance.getParameterByName("Intensity", out float changedParamValue);

            Debug.Log($"ChangedParamValue: {changedParamValue}");

            Debug.Log(_fmodEventInstance);
            //Debug.Log(result);
        }

        //float etp = _entertainmentManager.GetETP();
        //Debug.Log("ETP: " + etp);

        if (Input.GetKeyDown(KeyCode.X))
        {
            _intensity -= 33.33f;
            _fmodEventInstance.getParameterByName("Intensity", out float changedParamValue);

            Debug.Log($"ChangedParamValue: {changedParamValue}");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _intensity += 33.33f;
            _fmodEventInstance.getParameterByName("Intensity", out float changedParamValue);

            Debug.Log($"ChangedParamValue: {changedParamValue}");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _intensity = _entertainmentManager.GetETPThreshold();
            _fmodEventInstance.getParameterByName("Intensity", out float changedParamValue);

            Debug.Log($"ChangedParamValue: {changedParamValue}");
        }
    }

    private void UpdateIntensityParameter()
    {
        //_intensity = _entertainmentManager.GetETP();
        _fmodEventInstance.setParameterByName("Intensity", _intensity);

        _fmodEventInstance.getParameterByName("Intensity", out float changedParamValue);

        //Debug.Log($"ChangedParamValue: {changedParamValue}");
    }

    private void UpdateHealthParameter()
    {
        //_health = healthSlider.value;
        _fmodEventInstance.setParameterByName("Health", _health);

    }



}
