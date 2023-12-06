using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    // Singleton
    public static ParticleSystemManager Instance { get; private set; }

    [Header("Particle effects")]
    [SerializeField] private GameObject _slamEffectPrefab;
    [SerializeField] private GameObject _stunPrefab;
    [SerializeField] private GameObject _bloodPrefab;
    [SerializeField] private GameObject _jumpCrackPrefab;
    [SerializeField] private GameObject _explosionPrefab;

    [Header("Pool sizes")]
    [SerializeField] private int _slamPoolSize;
    [SerializeField] private int _stunPoolSize;
    [SerializeField] private int _bloodPoolSize;
    [SerializeField] private int _jumpCrackPoolSize;
    [SerializeField] private int _explosionPoolSize;

    [Header("Particle settings")]
    [SerializeField] private float _slamLifeTime;
    [SerializeField] private float _stunLifeTime;
    [SerializeField] private float _bloodLifeTime;
    [SerializeField] private float _jumpCrackLifeTime;
    [SerializeField] private float _explosionLifeTime;

    [Header("Particle Offsets")]
    [SerializeField] private float _stunHeightOffset = 2.0f;
    [SerializeField] private float _crackYvalue = 0.02f;
    [SerializeField] private float _explosionYvalue = 1.5f;

    private int _slamPoolIndex = 0;
    private int _stunPoolIndex = 0;
    private int _bloodPoolIndex = 0;
    private int _jumpCrackPoolIndex = 0;
    private int _explosionPoolIndex = 0;

    private List<GameObject> _slamPool;
    private List<GameObject> _stunPool;
    private List<GameObject> _bloodPool;
    private List<GameObject> _jumpCrackPool;
    private List<GameObject> _explosionPool;


    public enum ParticleEffects { Slam, Stun, Blood, JumpCrack, Explosion };

    
    private void Awake()
    {
        // Make sure there is only one instance, otherwhise remove the other one.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
  
        InitializePools();
    }
    

    /// <summary>
    /// Creates each pool and calls method to fill them with gameobjects.
    /// </summary>
    private void InitializePools()
    {
        _slamPool = new List<GameObject>();
        _stunPool = new List<GameObject>();
        _bloodPool = new List<GameObject>();
        _jumpCrackPool = new List<GameObject>();
        _explosionPool = new List<GameObject>();

        AddObjectsToPool(_slamPool, _slamEffectPrefab, _slamPoolSize);
        AddObjectsToPool(_stunPool, _stunPrefab, _stunPoolSize);
        AddObjectsToPool(_bloodPool, _bloodPrefab, _bloodPoolSize);
        AddObjectsToPool(_jumpCrackPool, _jumpCrackPrefab, _jumpCrackPoolSize);
        AddObjectsToPool(_explosionPool, _explosionPrefab, _explosionPoolSize);
    }



    /// <summary>
    /// Adds particle effects objects into desired pool.
    /// </summary>
    private void AddObjectsToPool(List<GameObject> pool, GameObject prefab, int poolsize)
    {
        for (int i = 0; i < poolsize; i++)
        {
            GameObject particleEffect = Instantiate(prefab, this.transform);
            particleEffect.SetActive(false);
            pool.Add(particleEffect);
        }
    }



    /// <summary>
    /// Retrieves the PE that should be played from its pool and activates it and moves it where it should be played.
    /// </summary>
    public void PlayParticleFromPool(ParticleEffects effect, Transform _transform)
    {
        GameObject currentEffect = null;
        Vector3 newPos = _transform.position;
        float coroutineTime = 0;

        switch (effect) 
        {
            case ParticleEffects.Slam:
                _slamPoolIndex = (_slamPoolIndex + 1) % _slamPoolSize;
                currentEffect = _slamPool[_slamPoolIndex];
                coroutineTime = _slamLifeTime;
                break;

            case ParticleEffects.Stun:
                newPos = new Vector3(newPos.x, 
                    newPos.y + _transform.localScale.y + _stunHeightOffset, newPos.z);

                _stunPoolIndex = (_stunPoolIndex + 1) % _stunPoolSize;
                currentEffect = _stunPool[_stunPoolIndex];
                coroutineTime = _stunLifeTime;
                break;

            case ParticleEffects.Blood:
                _bloodPoolIndex = (_bloodPoolIndex + 1) % _bloodPoolSize;
                currentEffect = _bloodPool[_bloodPoolIndex];
                coroutineTime = _bloodLifeTime;
                break;

            case ParticleEffects.JumpCrack:
                newPos = new Vector3(newPos.x, _crackYvalue, newPos.z);
                _jumpCrackPoolIndex = (_jumpCrackPoolIndex + 1) % _jumpCrackPoolSize;
                currentEffect = _jumpCrackPool[_jumpCrackPoolIndex];
                coroutineTime = _jumpCrackLifeTime;
                break;

            case ParticleEffects.Explosion:
                newPos = new Vector3(newPos.x, _explosionYvalue, newPos.z);
                _explosionPoolIndex = (_explosionPoolIndex + 1) % _explosionPoolSize;
                currentEffect = _explosionPool[_explosionPoolIndex];
                coroutineTime = _explosionLifeTime;
                PlayExplosionSound();
                break;

            default:
                break;
        }

        // Activate and place particle effect
        // Set timer (coroutine) to return to pool (set to pool pos and deactivate)
        currentEffect.SetActive(true);
        currentEffect.transform.position = newPos;
        StartCoroutine(StartReturnToPool(currentEffect, Time.time, coroutineTime));
    }



    /// <summary>
    /// Starts a coroutine that will reset and de-activate the particle effect when its done playing.
    /// </summary>
    private IEnumerator StartReturnToPool(GameObject particleEffect, float startTime, float coroutineTime)
    {
        // Start timer for particle life time
        while((Time.time - startTime) < coroutineTime)
        {
            yield return null;
        }

        // Particle is finished, return it to pool (reset position and de-activate)
        particleEffect.transform.position = this.transform.position;
        particleEffect.SetActive(false);
    }


    #region FMOD

    private void PlayExplosionSound()
    {
        string eventPath;
        int random = Random.Range(1, 4);

        if (random == 1)
        {
            eventPath = "event:/explosion";
        }
        if (random == 2)
        {
            eventPath = "event:/explosion2";
        }
        else
        {
            eventPath = "event:/explosion3";
        }

        FMOD.Studio.EventInstance explosion = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(explosion, this.transform);
        explosion.start();
        explosion.release();

        Debug.Log("Explosionsound played");
    }


    #endregion
}
