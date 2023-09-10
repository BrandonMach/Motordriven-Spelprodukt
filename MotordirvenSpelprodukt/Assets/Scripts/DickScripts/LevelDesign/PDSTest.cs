using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LevelDesign;

public class PDSTest : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private float displayRadius = 1f;

    [Header("Poisson Disc Sampling")]
    [SerializeField] private float radius = 1f;
    [SerializeField] private Vector2 regionSize = Vector2.one;
    [SerializeField] private int maxSamplingCount = 30;


    List<Vector2> points;

    private void OnValidate()
    {
       points = PoissonDiscSampling.GeneratePoints(radius, regionSize, maxSamplingCount); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(regionSize / 2, regionSize);


        if (points != null)
        {
            foreach (Vector2 point in points)
            {
                Gizmos.DrawSphere(point, displayRadius);
            }
        }
    }
}
