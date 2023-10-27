using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    // Singleton
    public static ParticleSystemManager Instance { get; private set; }

    [Header("Weapon effects")]
    [SerializeField] private ParticleSystem _weaponTrail;

    [Header("Shockwave effects")]
    [SerializeField] private int _shockWaveEffectPoolSize;
    [SerializeField] private ParticleSystem _shockWaveCircle;
    [SerializeField] private ParticleSystem _debree;
    [SerializeField] private ParticleSystem _crack;
    [SerializeField] private ParticleSystem _shockWaveFill;

    [Header("Bleed effects")]
    [SerializeField] private ParticleSystem _hitBloodEffect; // Initial blood spray from normal hits.
    [SerializeField] private ParticleSystem _stabbedBloodEffect; // Initial blood spray from stab
    [SerializeField] private ParticleSystem _bleedEffect;
    [SerializeField] private ParticleSystem _heavyBleedEffect;

    [Header("Misc effects")]
    [SerializeField] private int stunEffectPoolSize;
    [SerializeField] private ParticleSystem _stunEffect;


    private List<ShockwaveEffect> shockwaveEffectsList = new List<ShockwaveEffect>(); // Done

    private List<ParticleSystem> _stunEffectList = new List<ParticleSystem>(); // Done
    private List<ParticleSystem> _hitBloodEffectList = new List<ParticleSystem>();
    private List<ParticleSystem> _stabbedBloodEffectList = new List<ParticleSystem>();
    private List<ParticleSystem> _bleedEffectList = new List<ParticleSystem>();
    private List<ParticleSystem> _heavyEffectList = new List<ParticleSystem>();
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        InitiateShockWavePool();
    }


    private void InitiateShockWavePool()
    {
        for (int i = 0; i < _shockWaveEffectPoolSize; i++)
        {
            shockwaveEffectsList.Add(new ShockwaveEffect
            {
                ShockWaveCircle = _shockWaveCircle,
                Debree = _debree,
                Crack = _crack,
                ShockWaveFill = _shockWaveFill
            });
        }
    }

    private void InitiateStunEffectPool()
    {
        for (int i = 0; i < stunEffectPoolSize; i++)
        {
            //_stunEffectList
        }
    }
}
