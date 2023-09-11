using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public static class SpawnerAuxiliaries
    { 
        public static int RandomPrefabIndex(int prefabCount) => Random.Range(0, prefabCount);
        public static bool NumOfPrefabs(SpawnData data, GameObjectType type, ref int prefabCount, ref string errorMessage)
        {
            prefabCount = PrefabCountByType(data.data, type);
            if (prefabCount == -1) errorMessage = "No prefabs found in spawn data";
            return string.IsNullOrEmpty(errorMessage);
        }

        public static Vector3[] SpawnPositionsByType(ObjectData[] data, GameObjectType type)
        {
            foreach (var obj in data)
                if (obj.type == type)
                    return obj.spawnPositions;

            return null;
        }

        public static Vector3[] SpawnRotationsByType(ObjectData[] data, GameObjectType type)
        {
            foreach (var obj in data)
                if (obj.type == type)
                    return obj.spawnRotations;

            return null;
        }

        public static Vector3[] SpawnScalesByType(ObjectData[] data, GameObjectType type)
        {
            foreach (var obj in data)
                if (type == obj.type)
                    return obj.spawnScales;

            return null;
        }

        public static GameObject PrefabByTypeAndIndex(ObjectData[] data, GameObjectType type, int index)
        {
            foreach (var obj in data)
                if (type == obj.type)
                    return obj.prefabs[index];

            return null;
        }

        private static int PrefabCountByType(ObjectData[] data, GameObjectType type)
        {
            foreach (var obj in data)
                if (obj.type == type)
                    return obj.prefabs.Length;

            return -1;
        }
    }
}

