using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualEffects : MonoBehaviour
{
    [SerializeField] private Transform _shockwaveTransform;

    [SerializeField] private Animator _animator;

    [SerializeField] private ParticleSystem _weaponTrail;

    [SerializeField] private ParticleSystem _shockWave;

    [SerializeField] private ParticleSystem _debree;

    [SerializeField] private ParticleSystem _crack;


    public void StartWeaponTrail()
    {
        _weaponTrail.Play();
    }

    public void StartShockWave()
    {
        Vector3 shockwavePos = new Vector3(_shockwaveTransform.position.x, 0.25f, _shockwaveTransform.position.z);
        Quaternion shockwaveRot = _shockwaveTransform.rotation;

        Instantiate(_shockWave, shockwavePos, shockwaveRot);
        Instantiate(_debree, shockwavePos, shockwaveRot);
        Instantiate(_crack, new Vector3(shockwavePos.x, 0f, shockwavePos.z), shockwaveRot);

        //_shockWave.Play();
        //_debree.Play();
        //_crack.Play();
    }
}
