using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LevelDesign
{
    public enum LayerType
    {
        Tree,
        Pebble,
        Boulder,
        Grass
    }

    [CreateAssetMenu(menuName = "LevelDesign/SpawnData", fileName = "SpawnData")]
    public class SpawnData : ScriptableObject
    {
        public ObjectData[] data;
    }

    [Serializable]
    public struct ObjectData
    {
        public LayerType type;
        public Vector3[] spawnPositions;
        public Vector3[] spawnRotations;
        public Vector3[] spawnScales;
    }
}
