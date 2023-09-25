using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public class SpawnDataGenerator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool saveSpawnPoints;

        [Header("Poisson Disc Sampling")]
        [SerializeField] private int maxSamplingCount = 30;
        [SerializeField] private bool promptGeneration;

        [Header("Debug")]
        [SerializeField] private float gizmoHeightOffset;
        [SerializeField] private bool showSpawnData;

        [Header("Spawn data")]
        [SerializeField] private SpawnData spawnData;
        [SerializeField] private SpawnSurfaceData[] surfaces;

        private bool drawSpawnData;
        Dictionary<LayerType, List<Vector3>> spawnPoints;

        private void OnValidate()
        {
            PromptGeneration();
        }

        private void PromptGeneration()
        {
            // Currently only one procedural generation method is implemented (run PSD by default)
            if (promptGeneration && surfaces?.Length > 0) RunPSD();
            if (saveSpawnPoints && spawnPoints?.Count > 0) SaveData();

           UpdateInspectorButtons();
        }

        private void UpdateInspectorButtons()
        {
            
            if (showSpawnData)
            {
                saveSpawnPoints = false;
                promptGeneration = false;
                showSpawnData = false;

                // Update Gizmo
                drawSpawnData = !drawSpawnData;
                if (!drawSpawnData && spawnPoints?.Count > 0) spawnPoints.Clear();
            }
            else if (saveSpawnPoints)
            {
                showSpawnData = false;
                promptGeneration = false;
            }
            else if (promptGeneration)
            {
                promptGeneration = false;
                
                // Update Gizmo
                drawSpawnData = false;
            }
        }

        private void RunPSD()
        {
            spawnPoints = new Dictionary<LayerType, List<Vector3>>();

            for (int i = 0; i < surfaces.Length; i++)
            {
                // Convert every surfaces to rect objects
                Rect[] rects = new Rect[surfaces[i].data.Length];
                List<Vector3> points = new List<Vector3>();

                for (int j = 0; j < surfaces[i].data.Length; j++)
                {
                    rects[j] = SpawnerAuxiliaries.SurfaceToRect(surfaces[i].data[j]);
                    points.AddRange(PoissonDiscSampling.Run(rects[j], surfaces[i].data[j].spawnAltitude, surfaces[i].data[j].spawnRadius, surfaces[i].data[j].maxSpawnCount, maxSamplingCount));
                }

                // Add the points to the list with correct layer type
                if (spawnPoints.ContainsKey(surfaces[i].type))
                    spawnPoints[surfaces[i].type].AddRange(points);
                else
                    spawnPoints.Add(surfaces[i].type, points);
            }
        }

        private void SaveData()
        {
            int i = 0;
            spawnData.data = new ObjectData[spawnPoints.Count];

            foreach (KeyValuePair<LayerType, List<Vector3>> pair in spawnPoints)
            {
                Vector3[] pos = ListToArray(pair.Value);
                ObjectData obj = new ObjectData
                {
                    type = pair.Key,
                    spawnPositions = pos,
                    spawnRotations = RandomRotations(pos.Length),
                    spawnScales = VectorOnes(pos.Length),          // Use prefab's default scale!
                };

                spawnData.data[i] = obj;
                i++;
            }
        }
        private Vector3[] RandomRotations(int length)
        {
            Vector3[] rot = new Vector3[length];
            for (int i = 0; i < length; i++)
                rot[i] = new Vector3(0, Random.Range(0, 360), 0);

            return rot;
        }

        private Vector3[] VectorOnes(int length)
        {
            Vector3[] scales = new Vector3[length];
            for (int i = 0; i < length; i++)
                scales[i] = Vector3.one;
            return scales;
        }

        private void OnDrawGizmos()
        {
            if (surfaces?.Length > 0)
            {
                for (int i = 0; i< surfaces.Length;i++) DrawGizmoAuxiliaries.DrawSpawnSurfaces(surfaces[i].type, surfaces[i].data, gizmoHeightOffset);
            }

            if (drawSpawnData)
            {
                for (int i = 0; i < spawnData.data.Length; i++) DrawGizmoAuxiliaries.DrawShapesBySpawnType(spawnData.data[i].type, spawnData.data[i].spawnPositions);
            }
            else if (spawnPoints != null)
            {
                foreach (KeyValuePair<LayerType, List<Vector3>> pair in spawnPoints) DrawGizmoAuxiliaries.DrawShapesBySpawnType(pair.Key, ListToArray(pair.Value));
            }
        }

        #region Auxiliary
        private Vector3[] ListToArray(List<Vector3> points)
        {
            Vector3[] pos = new Vector3[points.Count];
            for (int i = 0; i < points.Count; i++)
                pos[i] = points[i];

            return pos;
        }
        #endregion
    }
}

