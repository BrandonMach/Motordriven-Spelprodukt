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
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider volumeSlider;

    private static FMODController _instance;

    public float _intensity;
    public float _health;

    public EventInstance _musicEventInstance;

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

        _intensity = 50;

        volumeSlider.value = 0.3f;
      
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

    public void ChangeEvent(string newEventPath)
    {
        _musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _musicEventInstance.release();

        _musicEventInstance = RuntimeManager.CreateInstance(newEventPath);
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
            float volume;
            _musicEventInstance.getVolume(out volume);
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
        //Debug.Log("ETP: " + _intensity);

    

        if (EntertainmentManager.Instance != null)
        {
            //_entertainmentManager = EntertainmentManager.Instance;
            _intensity = EntertainmentManager.Instance.GetETP();
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Entertainment", _intensity);
        }
        
       


        //_fmodEventInstance.getParameterByName("Intensity", out float changedParamValue);
        //Debug.Log($"ChangedParamValue: {changedParamValue}");
    }

    
    private void UpdateHealthParameter()
    {
        //_health = healthSlider.value;

        if (Player.Instance != null)
        {
            _health = Player.Instance.GetComponent<HealthManager>().CurrentHealthPoints;

            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Health", _health);
        }

        //_fmodEventInstance.getParameterByName("Health", out float changedParamValue);
        //Debug.Log($"ChangedParamValue: {changedParamValue}");
    }



}
