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

    [Header("Pool sizes")]
    [SerializeField] private int _slamPoolSize;
    [SerializeField] private int _stunPoolSize;
    [SerializeField] private int _bloodPoolSize;

    [Header("Particle settings")]
    [SerializeField] private float _slamLifeTime;
    [SerializeField] private float _stunLifeTime;
    [SerializeField] private float _bloodLifeTime;

    private int _slamPoolIndex = 0;
    private int _stunPoolIndex = 0;
    private int _bloodPoolIndex = 0;

    private List<GameObject> _slamPool;
    private List<GameObject> _stunPool;
    private List<GameObject> _bloodPool;


    public enum ParticleEffects { Slam, Stun, Blood };

    
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

        AddObjectsToPool(_slamPool, _slamEffectPrefab, _slamPoolSize);
        AddObjectsToPool(_stunPool, _stunPrefab, _stunPoolSize);
        AddObjectsToPool(_bloodPool, _bloodPrefab, _bloodPoolSize);
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
                newPos = new Vector3(newPos.x, newPos.y + _transform.localScale.y + 2, newPos.z);
                //newTransform.rotation = Quaternion.Euler(-90, 0, 0);
                _stunPoolIndex = (_stunPoolIndex + 1) % _stunPoolSize;
                currentEffect = _stunPool[_stunPoolIndex];
                coroutineTime = _stunLifeTime;
                break;

            case ParticleEffects.Blood:
                _bloodPoolIndex = (_bloodPoolIndex + 1) % _bloodPoolSize;
                currentEffect = _bloodPool[_bloodPoolIndex];
                coroutineTime = _bloodLifeTime;
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
}
