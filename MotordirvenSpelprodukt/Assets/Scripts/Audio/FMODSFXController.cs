using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FMODSFXController : MonoBehaviour
{
    private static FMODSFXController _instance;


    [SerializeField] Slider SFXVolumeSlider;
    List<EventInstance> _sfxEventInstances;
     
    EventInstance bowLoad;
    EventInstance bowRelease;
    EventInstance championSlam;
    EventInstance crowdBoo;
    EventInstance crowdCheer;
    EventInstance itemEquip;
    EventInstance jailDoorClose;
    EventInstance minionHit;
    EventInstance minionHit2;
    EventInstance minionHit3;
    EventInstance swordHit;
    EventInstance tomatoSplash;
    EventInstance tomatoSplash2;
    EventInstance coinDrop;

    public EventReference swordHitEventPath;
    public EventReference bowLoadEventPath;
    public EventReference bowReleaseEventPath;
    public EventReference championSlamEventPath;
    public EventReference crowdBooEventPath;
    public EventReference crowdCheerEventPath;
    public EventReference itemEquipEventPath;
    public EventReference jailDoorCloseEventPath;
    public EventReference minionHitEventPath;
    public EventReference minionHit2EventPath;
    public EventReference minionHit3EventPath;
    public EventReference tomatoSplashEventPath;
    public EventReference tomatoSplash2EventPath;
    public EventReference coinDropEventPath;

    public static FMODSFXController Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            // If no instance exists, make this the instance and mark as persistent
            Instance = this;
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
        CreateSFXInstances();
        //InitializeEventInstancesList();
        SFXVolumeSlider.value = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

    public float GetVolume()
    {
        foreach (EventInstance item in _sfxEventInstances)
        {
            if (item.isValid())
            {
                float volume;
                item.getVolume(out volume);
                return volume;
            }
            else
            {
                //Debug.LogWarning("FMOD event instance is not valid.");
                return 0f;
            }
            
        }
        return 0f;
        
    }

    public void SetVolume(float volume)
    {
        foreach (EventInstance item in _sfxEventInstances)
        {
            if (item.isValid())
            {
                item.setVolume(volume);
            }
            else
            {
                //Debug.Log("fmodEventInstance is not valid");
            }
        }
      
    }

    void InitializeEventInstancesList()
    {
        _sfxEventInstances = new List<EventInstance>
        {
            bowLoad,
            bowRelease,
            championSlam,
            crowdBoo,
            crowdCheer,
            itemEquip,
            jailDoorClose,
            minionHit,
            minionHit2,
            minionHit3,
            swordHit,
            tomatoSplash,
            tomatoSplash2,
            coinDrop
        };
    }

    /// <summary>
    /// Create instances for each SFX event
    /// </summary>
    void CreateSFXInstances()
    {
        _sfxEventInstances = new List<EventInstance>
        {
            bowLoad,
            bowRelease,
            championSlam,
            crowdBoo,
            crowdCheer,
            itemEquip,
            jailDoorClose,
            minionHit,
            minionHit2,
            minionHit3,
            swordHit,
            tomatoSplash,
            tomatoSplash2,
            coinDrop
        };

        bowLoad = FMODUnity.RuntimeManager.CreateInstance(bowLoadEventPath);
        bowRelease = FMODUnity.RuntimeManager.CreateInstance(bowReleaseEventPath);
        championSlam = FMODUnity.RuntimeManager.CreateInstance(championSlamEventPath);
        crowdBoo = FMODUnity.RuntimeManager.CreateInstance(crowdBooEventPath);
        crowdCheer = FMODUnity.RuntimeManager.CreateInstance(crowdCheerEventPath);
        itemEquip = FMODUnity.RuntimeManager.CreateInstance(itemEquipEventPath);
        jailDoorClose = FMODUnity.RuntimeManager.CreateInstance(jailDoorCloseEventPath);
        minionHit = FMODUnity.RuntimeManager.CreateInstance(minionHitEventPath);
        minionHit2 = FMODUnity.RuntimeManager.CreateInstance(minionHit2EventPath);
        minionHit3 = FMODUnity.RuntimeManager.CreateInstance(minionHit3EventPath);
        swordHit = FMODUnity.RuntimeManager.CreateInstance(swordHitEventPath);
        tomatoSplash = FMODUnity.RuntimeManager.CreateInstance(tomatoSplashEventPath);
        tomatoSplash2 = FMODUnity.RuntimeManager.CreateInstance(tomatoSplash2EventPath);
        coinDrop = FMODUnity.RuntimeManager.CreateInstance(coinDropEventPath);
    }

    public void PlayBowLoad() => bowLoad.start();
    public void PlayBowRelease() => bowRelease.start();
    public void PlayChampionSlam() => championSlam.start();
    public void PlayCrowdBoo() => crowdBoo.start();
    public void PlayCrowdCheer() => crowdCheer.start();
    public void PlayItemEquip() => itemEquip.start();
    public void PlayJailDoorClose() => jailDoorClose.start();
    public void PlaySwordHit() => swordHit.start();
    public void PlayTomatoSplash() => tomatoSplash.start();
    public void PlayTomatoSplash2() => tomatoSplash2.start();
    public void PlayCoinDrop() => coinDrop.start();
    public void PlayMinionHit() => minionHit.start();
    public void PlayMinionHit2() => minionHit2.start();
    public void PlayMinionHit3() => minionHit3.start();

    public void PlayRandomMinionHit()
    {
        int randomNumber = Random.Range(1, 4);

        if (randomNumber == 1)
        {
            PlayMinionHit();
        }
        else if (randomNumber == 2)
        {
            PlayMinionHit2();
        }
        else if (randomNumber == 3)
        {
            PlayMinionHit3();
        }
    }
}
