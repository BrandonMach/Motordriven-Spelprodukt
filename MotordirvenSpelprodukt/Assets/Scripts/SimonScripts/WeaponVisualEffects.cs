using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualEffects : MonoBehaviour
{
    [SerializeField] private Transform _shockwaveTransform;

    [SerializeField] private ParticleSystem _weaponTrail;

    [SerializeField] private ParticleSystem _shockWaveCircle;

    [SerializeField] private ParticleSystem _debree;

    [SerializeField] private ParticleSystem _crack;

    [SerializeField] private ParticleSystem _shockWaveFill;

    [SerializeField] private LayerMask _groundLayer;

    private ParticleSystem.MainModule _debreeParticleMainModule;
    private Color _debreeParticleStartColor;
    private Vector3 _shockwavePos;

    private void Start()
    {
        _debreeParticleMainModule = _debree.main;
        _debreeParticleStartColor = _debreeParticleMainModule.startColor.color;

    }
    public void StartWeaponTrail()
    {
        _weaponTrail.Play();
    }

    public void StartShockWave()
    {
        // TODO:
        // Object pool with particle effects for better performance
        //_shockwavePos = new Vector3(_shockwaveTransform.position.x, 0, _shockwaveTransform.position.z);
        //Quaternion shockwaveRot = _shockwaveTransform.rotation;

        //Instantiate(_shockWaveCircle, _shockwavePos, shockwaveRot);
        //Instantiate(_shockWaveFill, _shockwavePos, shockwaveRot);
        //GetDebreeColorFromRaycast(2, 0.5f);
        //Instantiate(_debree, _shockwavePos, shockwaveRot);
        //Instantiate(_crack, new Vector3(_shockwavePos.x, 0f, _shockwavePos.z), shockwaveRot);

        // Destroy objects after finished playing, until object pool is implemented.
    }

    void GetDebreeColorFromRaycast(float distance, float yOffset)
    {
        RaycastHit hit;
        //Check if we hit something on the ground layer with the raycast
        if (Physics.Raycast(_shockwaveTransform.position + Vector3.up * yOffset , -Vector3.up, out hit, distance, _groundLayer))
        {
            // If there is a renderer on the object change the color of the particle system accordingly
            MeshRenderer renderer = hit.collider.gameObject.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                
                _debreeParticleMainModule.startColor = new Color(renderer.material.color.r * 0.8f, renderer.material.color.g * 0.8f, renderer.material.color.b * 0.8f);
            }
        }
        else
        {
            _debreeParticleMainModule.startColor = _debreeParticleStartColor;
        }
    }
    public void SetNewTrail(Transform trail)
    {
        _weaponTrail = trail.GetComponent<ParticleSystem>();
    }
}
