using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LevelDesign;

public class PDSTest : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private float displayRadius = 1f;
    [SerializeField] private bool promptGeneration;

    [Header("Spawn data")]
    [SerializeField] private SpawnSurfaceData spawnSurfaces;
    [SerializeField] private int numOfSpawns;

    [Header("Poisson Disc Sampling")]
    [SerializeField] private float radius = 1f;
    [SerializeField] private Vector2 regionSize = Vector2.one;
    [SerializeField] private int maxSamplingCount = 30;

    List<Vector2> points;

    private void OnValidate()
    {
        if (promptGeneration)
            GeneratePointsOnSurface();
    }

    private void GeneratePointsOnSurface()
    {
        points = new List<Vector2>();

        Rect[] rects = new Rect[spawnSurfaces.data.Length];
        for (int i = 0; i < spawnSurfaces.data.Length; i++)
        {
            rects[i] = SpawnerAuxiliaries.SpawnSurfaceInRect(spawnSurfaces.data[i]);
            points = PoissonDiscSampling.GenerateSpawnPoints(radius, rects[i], maxSamplingCount);
        }
    }


    private void OnDrawGizmos()
    {
        if (spawnSurfaces != null)
            DrawGizmoAuxiliaries.DrawSpawnSurfaces(spawnSurfaces.data);


        if (points != null)
        {
            foreach (Vector2 point in points)
            {
                Gizmos.DrawSphere(point, displayRadius);
            }
        }
    }
}
