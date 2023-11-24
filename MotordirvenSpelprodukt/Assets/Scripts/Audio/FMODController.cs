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

    GameLoopManager _gameManager;
    HealthManager _healthManager;
    EntertainmentManager _entertainmentManager;

    public EventInstance _fmodEventInstance;

    private StudioEventEmitter eventEmitter;
    private static FMODController _instance;

    bool firstTime;

    public float _intensity;
    public float _health;

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
        firstTime = true;
        _intensity = 50;
        _gameManager = GameLoopManager.Instance;
        //_entertainmentManager = EntertainmentManager.Instance;

        //if (GameObject.Find("Transferables").GetComponent<TransferableScript>().GetFMODAM() != null)
        //    SetFMOD(GameObject.Find("Transferables").GetComponent<TransferableScript>().GetFMODAM());

        _fmodEventInstance = GetComponent<FMODUnity.StudioEventEmitter>().EventInstance;
        volumeSlider.value = 0.3f;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameLoopManager.Instance != null && firstTime)
        {
            firstTime = false;
            ChangeEvent("event:/Music");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            gameObject.GetComponent<StudioEventEmitter>().EventInstance.start();
        }

        UpdateIntensityParameter();
        UpdateHealthParameter();

        DontDestroyOnLoad(gameObject);
    }

    public void SetFMOD(FMODController fmod)
    {
        Instance = fmod;
    }

    public void ChangeEvent(string newEventPath)
    {
        // Stop and release the current instance
        _fmodEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        //_fmodEventInstance.release();

        // Create a new instance with the updated event path
        _fmodEventInstance = FMODUnity.RuntimeManager.CreateInstance(newEventPath);
        _fmodEventInstance.start();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public float GetVolume()
    {
        if (_fmodEventInstance.isValid())
        {
            float volume;
            _fmodEventInstance.getVolume(out volume);
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
        if (_fmodEventInstance.isValid())
        {
            _fmodEventInstance.setVolume(volume);
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
