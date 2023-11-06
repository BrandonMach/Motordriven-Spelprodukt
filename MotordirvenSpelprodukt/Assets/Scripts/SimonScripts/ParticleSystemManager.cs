using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    // Singleton
    public static ParticleSystemManager Instance { get; private set; }

    [Header("Weapon effects")]
    //[SerializeField] private ParticleSystem _weaponTrail;

    [Header("Shockwave effects")]
    [SerializeField] private Vector3 _shockwavePositionOffset;
    //[SerializeField] private Quaternion _shockwaveRotation;
    [SerializeField] private int _shockWaveEffectPoolSize = 5;
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


    public List<ShockwaveEffect> _shockwaveEffectsPool = new List<ShockwaveEffect>(); // Done
    public List<ParticleSystem> _stunEffectPool = new List<ParticleSystem>(); // Done

    //private List<ParticleSystem> _weaponTrailPool = new List<ParticleSystem>();

    //private List<ParticleSystem> _hitBloodEffectList = new List<ParticleSystem>();
    //private List<ParticleSystem> _stabbedBloodEffectList = new List<ParticleSystem>();
    //private List<ParticleSystem> _bleedEffectList = new List<ParticleSystem>();
    //private List<ParticleSystem> _heavyEffectList = new List<ParticleSystem>();
    

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

        InitializeShockWavePool();
        InitializeStunEffectPool();
    }
    

    private void InitializeShockWavePool()
    {
        //for (int i = 0; i < _shockWaveEffectPoolSize; i++)
        //{
        //    ShockwaveEffect e = new ShockwaveEffect
        //    {
        //        ShockWaveCircle = _shockWaveCircle,
        //        Debree = _debree,
        //        Crack = _crack,
        //        ShockWaveFill = _shockWaveFill
        //    };
            
        //    _shockwaveEffectsPool.Add(Instantiate(e, transform.position, Quaternion.identity));
        //}
    }


    private void InitializeStunEffectPool()
    {
        for (int i = 0; i < stunEffectPoolSize; i++)
        {
            _stunEffectPool.Add(_stunEffect);
        }
    }


    public void PlayStunEffect(Vector3 particlePos, Quaternion rot, Transform parent)
    {
        _stunEffectPool[0].transform.SetPositionAndRotation(particlePos, rot);
        _stunEffectPool[0].transform.parent = parent;
        _stunEffectPool[0].Play();
    }

    public void PlayShockWaveEffect(Vector3 attackerPos)
    {
        Vector3 particlePos = attackerPos + _shockwavePositionOffset;
        _shockwaveEffectsPool[0].SetTransform(particlePos);
        _shockwaveEffectsPool[0].Play();
    }
}
