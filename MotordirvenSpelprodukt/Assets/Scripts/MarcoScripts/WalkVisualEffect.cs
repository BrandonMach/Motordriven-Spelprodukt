using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkVisualEffect : MonoBehaviour
{

    [SerializeField] private ParticleSystem _walkParticle;
    [SerializeField] private LayerMask _groundLayer;

    private ParticleSystem.MainModule _walkParticleMainModule;
    private Color _walkParticleStartColor;
    private Vector3 _position;


    private void Awake()
    {
        _walkParticleMainModule = _walkParticle.main;
        _walkParticleStartColor = _walkParticleMainModule.startColor.color;
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetColorFromMaterial(Vector3 position, float distance, float yOffset)
    {
        RaycastHit hit;
        //Check if we hit something on the ground layer with the raycast
        if (Physics.Raycast(position + Vector3.up * yOffset, -Vector3.up, out hit, distance, _groundLayer))
        {
            // If there is a renderer on the object change the color of the particle system accordingly
            MeshRenderer renderer = hit.collider.gameObject.GetComponent<MeshRenderer>();
            if (renderer != null)
            {

                _walkParticleMainModule.startColor = new Color(renderer.material.color.r * 0.8f, renderer.material.color.g * 0.8f, renderer.material.color.b * 0.8f);
            }
        }
        else
        {
            _walkParticleMainModule.startColor = _walkParticleStartColor;
        }
    }
}
