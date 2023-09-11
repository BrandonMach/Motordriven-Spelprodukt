using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public class Spawner : MonoBehaviour
    {
        [Header("Spawn data")]
        [SerializeField] private SpawnData placementData;   // Data will be stored here by the pcg system
        
        [Header("Debug")]
        [SerializeField] private bool promptPlacement;

        List<GameObject> spawnedObjs;

        private void OnValidate()
        {
            if (!promptPlacement) return;

            string errorMessage = string.Empty;
            if (!SpawnObjectsByType(placementData, GameObjectType.Tree, ref errorMessage)) Debug.LogError(errorMessage);
        }

        private void OnDrawGizmos()
        {
            if (placementData == null) return;
            DrawGizmoOnType(GameObjectType.Tree);
        }

        private void DrawGizmoOnType(GameObjectType type)
        {
            Vector3[] spawnPos = SpawnerAuxiliaries.SpawnPositionsByType(placementData.data, type);
            DrawGizmoAuxiliaries.DrawShapeByType(spawnPos, type);
        }

        private bool SpawnObjectsByType(SpawnData placements, GameObjectType type, ref string errorMessage)
        {
            spawnedObjs = new List<GameObject>();

            Vector3[] spawnPos = SpawnerAuxiliaries.SpawnPositionsByType(placements.data, type);
            Vector3[] spawnRots = SpawnerAuxiliaries.SpawnRotationsByType(placements.data, type);
            Vector3[] spawnScales = SpawnerAuxiliaries.SpawnScalesByType(placements.data, type);

            if (spawnPos.Length <= 0) return !string.IsNullOrEmpty(errorMessage = "Object type doesn't exist in data");
            if (spawnPos.Length != spawnRots.Length || spawnPos.Length != spawnScales.Length || spawnRots.Length != spawnScales.Length) return !string.IsNullOrEmpty(errorMessage = "Array mismatch in spawn data");

            for (int i = 0; i < spawnPos.Length; i++)
            {
                int prefabCount = 0;
                if (!SpawnerAuxiliaries.NumOfPrefabs(placements, type, ref prefabCount, ref errorMessage)) return false;

                GameObject obj = Instantiate(SpawnerAuxiliaries.PrefabByTypeAndIndex(placements.data, type, SpawnerAuxiliaries.RandomPrefabIndex(prefabCount)));
                obj.transform.position = spawnPos[i];
                obj.transform.rotation = Quaternion.Euler(spawnRots[i]);
                obj.transform.localScale = spawnScales[i];
                obj.transform.parent = transform;
                spawnedObjs.Add(obj);
            }

            return true;
        }
    }
}
