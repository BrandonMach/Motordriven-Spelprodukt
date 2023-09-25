using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace LevelDesign
{
    public class Spawner : MonoBehaviour
    {
        [Header("Spawn data")]
        [SerializeField] private SpawnData placements;   // Data will be stored here by the pcg system
        [SerializeField] private GameObject[] treePrefabs;
        [SerializeField] private GameObject[] vegetationPrefabs;
        [SerializeField] private GameObject[] groundPrefabs;

        [Header("Debug")]
        [SerializeField] private bool promptPlacement;
        List<GameObject> spawnedObjs;

        private void OnValidate()
        {
            if (!promptPlacement) return;

            string errorMessage = string.Empty;
            //if (!SpawnObjectsByType(placements, LayerType.Tree, ref errorMessage)) Debug.LogError(errorMessage);
            if (!Run(ref errorMessage)) Debug.LogWarning(errorMessage);
            UpdateInspectorButtons();
        }

        private void UpdateInspectorButtons()
        {
            if (promptPlacement)
                promptPlacement = false;
        }

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

        private bool Run(ref string errorMessage)
        {
            spawnedObjs = new List<GameObject>();

            for (int i = 0; i < placements.data.Length; i++)
            {
                LayerType type = placements.data[i].type;
                Vector3[] spawnPos = placements.data[i].spawnPositions;
                Vector3[] spawnRots = placements.data[i].spawnRotations;
                Vector3[] spawnScales = placements.data[i].spawnScales;

                if (spawnPos.Length <= 0) return !string.IsNullOrEmpty(errorMessage = "Object type doesn't exist in data");
                if (spawnPos.Length != spawnRots.Length || spawnPos.Length != spawnScales.Length || spawnRots.Length != spawnScales.Length) return !string.IsNullOrEmpty(errorMessage = "Array mismatch in spawn data");

                for (int j = 0; j < spawnPos.Length; j++)
                {
                    if (!PrefabExistForSpawn(type, ref errorMessage)) return false;
                    GameObject obj = Instantiate(GetRandomPrefabByType(type));
                    obj.transform.position = spawnPos[j];
                    obj.transform.rotation = Quaternion.Euler(spawnRots[j]);
                    obj.transform.parent = transform;
                    spawnedObjs.Add(obj);
                }
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
                case LayerType.Ground:
                    if (groundPrefabs.Length < 0) errorMessage = "No ground prefabs found";
                    break;
                case LayerType.Vegetation:
                    if (vegetationPrefabs.Length < 0) errorMessage = "No vegetation prefabs found";
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
                case LayerType.Ground:
                    return groundPrefabs[Random.Range(0, groundPrefabs.Length)];
                case LayerType.Vegetation:
                    return vegetationPrefabs[Random.Range(0, vegetationPrefabs.Length)];
            }

            return null;
        }
    }
}
