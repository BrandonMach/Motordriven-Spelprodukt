using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveEffect
{
    public ParticleSystem ShockWaveCircle { get; set; }
    public ParticleSystem Debree { get; set; }
    public ParticleSystem Crack { get; set; }
    public ParticleSystem ShockWaveFill { get; set; }


    public void SetTransform(Vector3 particlePos)
    {
        ShockWaveCircle.transform.SetPositionAndRotation(particlePos, Quaternion.identity);
        Debree.transform.SetPositionAndRotation(particlePos, Quaternion.identity);
        Crack.transform.SetPositionAndRotation(particlePos, Quaternion.identity);
        ShockWaveFill.transform.SetPositionAndRotation(particlePos, Quaternion.identity);
    }

    public void Play()
    {
        ShockWaveCircle.Play();
        Debree.Play();
        Crack.Play();
        ShockWaveFill.Play();
    }
}
