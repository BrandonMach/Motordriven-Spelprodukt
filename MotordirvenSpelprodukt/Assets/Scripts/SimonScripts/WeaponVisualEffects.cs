using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualEffects : MonoBehaviour
{
    [SerializeField] private Transform _shockwaveTransform;

    [SerializeField] private Animator _animator;

    [SerializeField] private ParticleSystem _weaponTrail;

    [SerializeField] private ParticleSystem _shockWaveCircle;

    [SerializeField] private ParticleSystem _debree;

    [SerializeField] private ParticleSystem _crack;

    [SerializeField] private ParticleSystem _shockWaveFill;

    public void StartWeaponTrail()
    {
        _weaponTrail.Play();
    }

    public void StartShockWave()
    {
        // TODO:
        // Object pool with particle effects for better performance
        Vector3 shockwavePos = new Vector3(_shockwaveTransform.position.x, 0.25f, _shockwaveTransform.position.z);
        Quaternion shockwaveRot = _shockwaveTransform.rotation;

        Instantiate(_shockWaveCircle, shockwavePos, shockwaveRot);
        Instantiate(_shockWaveFill, shockwavePos, shockwaveRot);
        Instantiate(_debree, shockwavePos, shockwaveRot);
        Instantiate(_crack, new Vector3(shockwavePos.x, 0.25f, shockwavePos.z), shockwaveRot);

        // Destroy objects after finished playing, until object pool is implemented.

    }
}
