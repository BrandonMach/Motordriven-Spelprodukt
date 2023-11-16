using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class FMODController : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIntensityParameter();
        UpdateHealthParameter();
    }

    public void SetVolume(float volume)
    {
        if (_fmodEventInstance.isValid())
        {
            _fmodEventInstance.setVolume(volume);
        }
    }

    
    private void UpdateIntensityParameter()
    {
        _intensity = _entertainmentManager.GetETP();
        _fmodEventInstance.setParameterByName("Intensity", _intensity);


        //_fmodEventInstance.getParameterByName("Intensity", out float changedParamValue);
        //Debug.Log($"ChangedParamValue: {changedParamValue}");
    }

    
    private void UpdateHealthParameter()
    {
        _health = healthSlider.value;
        _fmodEventInstance.setParameterByName("Health", _health);


        //_fmodEventInstance.getParameterByName("Health", out float changedParamValue);
        //Debug.Log($"ChangedParamValue: {changedParamValue}");
    }



}
