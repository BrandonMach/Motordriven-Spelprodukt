using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveInfoHolder : ScriptableObject
{

    public EnemyScript[] WaveMinions;
    public float timeToNextEnemy;
    public float timeToNextWave;
    [HideInInspector] public int EnemiesLeft;



}
