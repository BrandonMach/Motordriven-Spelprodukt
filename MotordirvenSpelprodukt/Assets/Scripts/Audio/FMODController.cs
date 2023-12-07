using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FMODController : MonoBehaviour
{
    private static FMODController _instance;

    public float _intensity;
    public float _health;

    public EventInstance _musicEventInstance;

    bool _firstTime = true;

    public static FMODController Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if (_instance == null)
        {
            // If no instance exists, make this the instance and mark as persistent
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this GameObject
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnMainMenuEnter += HandleMainMenuEnter;
        GameManager.OnArenaEnter += HandleOnArenaEnter;
        GameManager.OnAfterArenaEnter += HandleOnAfterArenaEnter;
        GameManager.OnOpenWorldEnter += HandleOnOpenWorldEnter;

        _intensity = 50;
        
        _musicEventInstance.getVolume(out float vol);
        Debug.Log("EeventVol: " + vol);
        _musicEventInstance.setVolume(100);

        _musicEventInstance.getVolume(out float vol2);

        Debug.Log("EeventVol: " + vol2);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIntensityParameter();
        UpdateHealthParameter();

        //DontDestroyOnLoad(gameObject);
    }

    private void HandleMainMenuEnter()
    {
        ChangeEvent("event:/introMusic");
    }

    private void HandleOnArenaEnter()
    {
        ChangeEvent("event:/music");
    }

    private void HandleOnAfterArenaEnter()
    {
        ChangeEvent("event:/AfterArenaMusic");
    }

    private void HandleOnOpenWorldEnter()
    {
        ChangeEvent("event:/introMusic");
    }

    public void ChangeEvent(string newEventPath)
    {
        _musicEventInstance.getVolume(out float vol);

        _musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _musicEventInstance.release();

        _musicEventInstance = RuntimeManager.CreateInstance(newEventPath);

        if (_firstTime)
        {
            vol = 1f;
            _firstTime = false;
        }

        _musicEventInstance.setVolume(vol);
        _musicEventInstance.start();
    }

    public void SetFMOD(FMODController fmod)
    {
        Instance = fmod;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public float GetVolume()
    {
        if (_musicEventInstance.isValid())
        {
            _musicEventInstance.getVolume(out float volume);
            return volume;
        }
        else
        {
            Debug.LogWarning("FMOD event instance is not valid.");
            return 0f;
        }
    }

    public void SetVolume(float volume)
    {
        if (_musicEventInstance.isValid())
        {
            _musicEventInstance.setVolume(volume);
        }
        else
        {
            Debug.Log("fmodEventInstance is not valid");
        }
    }

    
    private void UpdateIntensityParameter()
    {

        if (EntertainmentManager.Instance != null)
        {           
            _intensity = EntertainmentManager.Instance.GetETP();
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Entertainment", _intensity);
        }
       

        //_fmodEventInstance.getParameterByName("Intensity", out float changedParamValue);
        //Debug.Log($"ChangedParamValue: {changedParamValue}");
    }

    
    private void UpdateHealthParameter()
    {

        if (Player.Instance != null)
        {
            _health = Player.Instance.GetComponent<HealthManager>().CurrentHealthPoints;

            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Health", _health);
        }

        //_fmodEventInstance.getParameterByName("Health", out float changedParamValue);
        //Debug.Log($"ChangedParamValue: {changedParamValue}");
    }



}
