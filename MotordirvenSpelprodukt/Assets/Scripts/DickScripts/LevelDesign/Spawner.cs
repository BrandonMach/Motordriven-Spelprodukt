using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace LevelDesign
{
    public class Spawner : MonoBehaviour
    {
        [Header("Spawn data")]
        [SerializeField] private SpawnData spawns;   // Data will be stored here by the pcg system
        [SerializeField] private GameObject[] treePrefabs;
        [SerializeField] private GameObject[] pebblePrefabs;


        [Header("Debug")]
        [SerializeField] private bool promptPlacement;

        List<GameObject> spawnedObjs;

        private void OnValidate()
        {
            if (!promptPlacement) return;

            string errorMessage = string.Empty;
            if (!SpawnObjectsByType(spawns, LayerType.Tree, ref errorMessage)) Debug.LogError(errorMessage);
        }

        private void OnDrawGizmos()
        {
            DrawGizmoOnType(LayerType.Tree);
            //DrawSpawnSurfaces();
        }

        #region Gizmo Debug
        private void DrawGizmoOnType(LayerType type)
        {
            if (spawns == null) return;
            Vector3[] spawnPos = SpawnerAuxiliaries.SpawnPositionsByType(spawns.data, type);
            DrawGizmoAuxiliaries.DrawShapeByType(spawnPos, type);
        }

        //private void DrawSpawnSurfaces()
        //{
        //    if (spawnSurfaces == null) return;
        //    DrawGizmoAuxiliaries.DrawSpawnSurfaces(spawnSurfaces.data);
        //}
        #endregion

        private bool SpawnObjectsByType(SpawnData placements, LayerType type, ref string errorMessage)
        {
            spawnedObjs = new List<GameObject>();

            Vector3[] spawnPos = SpawnerAuxiliaries.SpawnPositionsByType(placements.data, type);
            Vector3[] spawnRots = SpawnerAuxiliaries.SpawnRotationsByType(placements.data, type);
            Vector3[] spawnScales = SpawnerAuxiliaries.SpawnScalesByType(placements.data, type);

            if (spawnPos.Length <= 0) return !string.IsNullOrEmpty(errorMessage = "Object type doesn't exist in data");
            if (spawnPos.Length != spawnRots.Length || spawnPos.Length != spawnScales.Length || spawnRots.Length != spawnScales.Length) return !string.IsNullOrEmpty(errorMessage = "Array mismatch in spawn data");

            for (int i = 0; i < spawnPos.Length; i++)
            {
                if (!PrefabExistForSpawn(type, ref errorMessage)) return false;

                GameObject obj = Instantiate(GetRandomPrefabByType(type));
                obj.transform.position = spawnPos[i];
                obj.transform.rotation = Quaternion.Euler(spawnRots[i]);
                obj.transform.localScale = spawnScales[i];
                obj.transform.parent = transform;
                spawnedObjs.Add(obj);
            }

            return true;
        }


        private bool PrefabExistForSpawn(LayerType type, ref string errorMessage)
        {
            switch(type)
            {
                case LayerType.Tree:
                    if (treePrefabs.Length < 0) errorMessage = "No tree prefabs found";
                    break;
                case LayerType.Pebble:
                    if (pebblePrefabs.Length < 0) errorMessage = "No pebble prefabs found";
                    break;
            }

            return string.IsNullOrEmpty(errorMessage);
        }

        private GameObject GetRandomPrefabByType(LayerType type)
        {
            switch (type)
            {
                case LayerType.Tree:
                    return treePrefabs[Random.Range(0, treePrefabs.Length)];
                case LayerType.Pebble:
                    return pebblePrefabs[Random.Range(0, pebblePrefabs.Length)];
            }

            return null;
        }
    }
}
