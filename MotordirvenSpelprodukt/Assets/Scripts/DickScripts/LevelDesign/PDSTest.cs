using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LevelDesign
{
    public enum PCGType
    {
        PoissonDiscSampling,        // Done
        BinarySpacePartitioning,    // TODO
        Noise,                      // TODO
    }

    public class PDSTest : MonoBehaviour
    {
        [Header("Debug settings")]
        [SerializeField] private float displayRadius = 1f;
        [SerializeField] private bool promptGeneration;
        [SerializeField] private bool saveSpawnPoints;

        [Header("Poisson Disc Sampling")]
        [SerializeField] private float radius = 1f;
        [SerializeField] private int maxSpawnPoints;
        [SerializeField] private int maxSamplingCount = 30;

        [Header("Spawn data")]
        [SerializeField] private SpawnData spawnData;
        [SerializeField] private SpawnSurfaceData[] surfaces;

        Dictionary<LayerType, List<Vector3>> spawnPoints;

        private void OnValidate()
        {
            PromptGeneration();
        }

        private void PromptGeneration()
        {
            // Currently only one procedural generation method is implemented (run PSD by default)
            if (promptGeneration && surfaces?.Length > 0) RunPSD();
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
                    points.AddRange(PoissonDiscSampling.Run(rects[j], surfaces[i].data[j].altitude, radius, maxSpawnPoints, maxSamplingCount));
                }


                // Add the points to the list with correct layer type
                if (spawnPoints.ContainsKey(surfaces[i].type))
                    spawnPoints[surfaces[i].type].AddRange(points);
                else
                    spawnPoints.Add(surfaces[i].type, points);


            }

            if (saveSpawnPoints)
                SaveData();
        }

        private void SaveData()
        {
            int i = 0;
            spawnData.data = new ObjectData[spawnPoints.Count];


            foreach (KeyValuePair<LayerType, List<Vector3>> pair in spawnPoints)
            {
                Vector3[] pos = GetPosFromPoints(pair.Value);


                ObjectData obj = new ObjectData
                {
                    type = pair.Key,
                    spawnPositions = pos,
                    spawnRotations = RandomRotations(pos.Length),
                    spawnScales = FixedScale(pos.Length),
                };

                spawnData.data[i] = obj;
                i++;

            }
        }

        private Vector3[] GetPosFromPoints(List<Vector3> points)
        {
            Vector3[] pos = new Vector3[points.Count];
            for (int i = 0; i < points.Count; i++)
                pos[i] = points[i];

            return pos;
        }

        private Vector3[] RandomRotations(int length)
        {
            Vector3[] rot = new Vector3[length];
            for (int i = 0; i < length; i++)
                rot[i] = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);

            return rot;
        }

        private Vector3[] FixedScale(int length, int fixedScale = 20)
        {
            Vector3[] scale = new Vector3[length];
            for (int i = 0; i < length; i++)
                scale[i] = new Vector3(fixedScale, fixedScale, fixedScale);

            return scale;
        }

        private void OnDrawGizmos()
        {
            if (surfaces?.Length > 0)
                foreach (var surface in surfaces)
                    DrawGizmoAuxiliaries.DrawSpawnSurfaces(surface.data);

            if (spawnPoints != null)
            {
                foreach (var spawnPoint in spawnPoints.Values)
                {
                    foreach (Vector3 point in spawnPoint)
                    {
                        Gizmos.DrawSphere(point, displayRadius);
                    }
                }
            }
        }
    }
}

