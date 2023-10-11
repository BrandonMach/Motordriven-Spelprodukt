using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAutoDestory : MonoBehaviour
{
    private ParticleSystem _ps;
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, _ps.main.duration);
    }
}
