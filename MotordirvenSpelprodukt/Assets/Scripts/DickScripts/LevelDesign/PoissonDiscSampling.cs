using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public static class PoissonDiscSampling
    {
        public static List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int samplingThresholdRejection = 30)
        {
            // rf. Pythagoras
            float cellSize = radius / Mathf.Sqrt(2);

            // (Num of column, num of rows)
            int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize)];

            List<Vector2> points = new List<Vector2>();
            List<Vector2> spawnPoints = new List<Vector2>();

            // Set center point
            spawnPoints.Add(sampleRegionSize / 2);

            while (spawnPoints.Count > 0)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Count);
                Vector2 spawnCenter = spawnPoints[spawnIndex];

                bool validCandidateGenerated = false;

                for (int i = 0; i < samplingThresholdRejection; i++)
                {
                    float angle = Random.value * Mathf.PI * 2;
                    Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));

                    // Spawn the candidate outside the spawn center
                    float magnitude = Random.Range(radius, 2 * radius);

                    Vector2 candidatePos = spawnCenter * direction * magnitude;

                    if (IsValid(candidatePos, sampleRegionSize, cellSize, radius, points, grid))
                    {
                        points.Add(candidatePos);
                        spawnPoints.Add(candidatePos);

                        // add 
                        grid[(int)(candidatePos.x / cellSize), (int)(candidatePos.y / cellSize)] = points.Count;

                        validCandidateGenerated = true;
                        break;
                    }
                }

                // If no candidate was accepted
                if (!validCandidateGenerated)
                    spawnPoints.RemoveAt(spawnIndex);
            }

            return points;
        }


        private static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid)
        {
            if (candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 && candidate.y < sampleRegionSize.y)
            {
                // Identify cell the candidate resides into
                int cellX = (int)(candidate.x / cellSize);
                int cellY = (int)(candidate.y / cellSize);

                int startCellX = Mathf.Max(0, cellX - 2);
                int endCellX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
                int startCellY = Mathf.Max(0, cellY - 2);
                int endCellY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);


                for (int i = startCellX; i < endCellX; i++)
                {
                    for (int j = startCellY; j < endCellY; j++)
                    {
                        int pointIndex = grid[i, j] - 1;

                        if (pointIndex != -1)
                        {
                            float sqrtDistance = (candidate - points[pointIndex]).sqrMagnitude;
                            if (sqrtDistance < radius*radius)
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }
            return false;
        }
    }

}