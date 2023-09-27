using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    // DO NOT CHANGE THE LAYOUT OF THIS SCRIPT - Level data will be erased! 

    [CreateAssetMenu(menuName = "LevelDesign/SpawnSurfaceData", fileName = "spawnSurfaceData")]
    public class SpawnSurfaceData : ScriptableObject
    {
        public LayerType type;
        public SpawnSurface[] data;
    }

    [Serializable]
    public struct SpawnSurface
    {
        public float spawnRadius;
        public float spawnAltitude;
        public float maxSpawnCount;
        public float xWidth;
        public float yWidth;
        public Vector2 centerPoint;
    }
}