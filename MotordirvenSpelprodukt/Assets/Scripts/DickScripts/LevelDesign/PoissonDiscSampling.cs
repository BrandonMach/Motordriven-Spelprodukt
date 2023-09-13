using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public static class PoissonDiscSampling
    {
        private static Vector2 RndPosition(Vector2 lowerBound, Vector2 upperBound) => new Vector2(Random.Range(lowerBound.x, upperBound.x + 1), Random.Range(lowerBound.y, upperBound.y + 1));

        private static bool WithinBounds(Vector2 candidate, Rect bounds) => candidate.x >= bounds.xMin && candidate.x < bounds.xMax && candidate.y >= bounds.yMin && candidate.y < bounds.yMax;

        // Create a method in spawn aux. that: 1) Adds random spin, 2) Adds altitude (vector2->vector3), 3) Adds random resize (with bool trigger to enable disable this feature)
        public static List<Vector2> GenerateSpawnPoints(float avoidanceRadius, Rect bounds, int maxSamplingCount = 30)
        {
            // cells used to track if a candidate is spawned within or outside the circumference of the reference 
            float cellSize = avoidanceRadius / Mathf.Sqrt(2);

            int numOfColumn = Mathf.CeilToInt(bounds.width / cellSize);
            int numOfRow = Mathf.CeilToInt(bounds.height / cellSize);

            // Values in a particular array position indicates 
            int[,] grid = new int[numOfColumn, numOfRow];

            List<Vector2> points = new List<Vector2>();             // List of valid candidates
            List<Vector2> spawnPoints = new List<Vector2>();        // List to store all spawned points + with start seed

            // Add a starting seed within spawning bounds
            spawnPoints.Add(RndPosition(new Vector2(bounds.xMin, bounds.yMin), new Vector2(bounds.xMax, bounds.yMax)));
            Debug.Log($"Cellsize: {cellSize}");
            Debug.Log($"Gridsize: ({grid.GetLength(0)}, {grid.GetLength(1)})");


            while (spawnPoints.Count > 0)
            {
                // Get a reference point to spawn the candidate
                int refIndex = Random.Range(0, spawnPoints.Count);
                Vector2 refPos = spawnPoints[refIndex];

                bool isValidCandidate = false;

                for (int i = 0; i < maxSamplingCount; i++)
                {
                    // Generate a spawn position for the candidate (should be outside the circumference of the ref point!)
                    float angle = Random.value * Mathf.PI * 2;
                    // Unity's unit circle has an 90 degree counter-clockwise offset
                    Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                    // Let the distance from ref point be a random range from radius to two times the radius
                    float distance = Random.Range(avoidanceRadius, 2*avoidanceRadius);

                    Vector2 candidate = refPos + (direction * distance);
                    Debug.Log($"Candidate: {candidate}");
                   
                    if (IsValidPoint(candidate, points, bounds, grid, cellSize, avoidanceRadius))
                    {
                        // Add the point to the list
                        points.Add(candidate);
                        spawnPoints.Add(refPos);
                        grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
                        isValidCandidate = true;
                        break;
                    }
                }

                if (!isValidCandidate) spawnPoints.RemoveAt(refIndex);
            }

            return points;
        }

        
        private static bool IsValidPoint(Vector2 candidate, List<Vector2> points, Rect bounds, int[,] grid, float cellSize, float radius)
        {
            // Check if the spawn point is within bounds
            if (WithinBounds(candidate, bounds))
            {
                // Identify the cell the candidate resides in
                int cellX = (int)(candidate.x / cellSize);
                int cellY = (int)(candidate.y / cellSize);

                // Do a search on the two adjacent cells next to the candidate cell to check if there's any existing spawned points
                int startCellX = Mathf.Max(0, cellX - 2);
                int endCellX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
                int startCellY = Mathf.Max(0, cellY - 2);
                int endCellY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

                for (int i = startCellX; i < endCellX; i++)
                {
                    for (int j = startCellY; j < endCellY; j++)
                    {
                        // Get the index of the candidates that resides in the grid[i,j] cell position (-1 because points is one less of the size of the list "spawnPoints") 
                        int pointIndex = grid[i, j] - 1;

                        // Continue to next cell if there's point in the cell(i,j) (= -1)
                        if (pointIndex != -1)
                        {
                            // Do a proper check to see if the candidate is within vicinity of the ref. point's circumference 
                            float sqrtDistance = (candidate - points[pointIndex]).sqrMagnitude;
                            if (sqrtDistance < radius * radius) return false;
                        }
                    }
                }
                return true;
            }

            return false;
        }

        public static List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int samplingThresholdRejection = 30)
        {
            // rf. Pythagoras
            float cellSize = radius / Mathf.Sqrt(2);

            // (Num of column, num of rows)
            int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize)];

            List<Vector2> points = new List<Vector2>();
            List<Vector2> spawnPoints = new List<Vector2>();

            // Set center point
            spawnPoints.Add(sampleRegionSize / 2);      // points.Count = 0

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