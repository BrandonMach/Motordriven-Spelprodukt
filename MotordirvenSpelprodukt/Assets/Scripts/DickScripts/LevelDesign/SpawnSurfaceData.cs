using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    [CreateAssetMenu(menuName = "LevelDesign/SpawnSurfaceData", fileName = "spawnSurfaceData")]
    public class SpawnSurfaceData : ScriptableObject
    {
        public SpawnSurface[] data;
    }

    [Serializable]
    public struct SpawnSurface
    {
        public float altitude;
        public float width;
        public float height;
        public Vector2 centerPoint;
    }

}