using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public static class PoissonDiscSampling
    {
        public static List<Vector3> Run(Rect surface, float altitude, float radius, float numOfCandidates = 25, float maxSamplingCount = 30)
        {
            List<Vector3> candidates = new List<Vector3>();
            Vector2 gridSize = Vector2.zero;
            float cellSize = 0;

            foreach (Vector2 c in Generate(surface, radius, numOfCandidates, maxSamplingCount, ref gridSize, ref cellSize))
            {
                Vector2 candidate = ConvertToWorldCoordinates(surface, gridSize, cellSize, c);
                Vector3 point = new Vector3(candidate.x, altitude, candidate.y);
                candidates.Add(point);
            }
            return candidates;
        }


        private static List<Vector2> Generate(Rect surface, float radius, float numOfCandidates, float maxSamplingCount, ref Vector2 gridSize, ref float cellSize)
        {
            // Create a grid with cells based on the radius - cells will be used to measure if a candidate is outside the reference
            cellSize = radius / Mathf.Sqrt(2);

            // Number of columns and rows based on the cell size
            gridSize = new Vector2(Mathf.CeilToInt(surface.width / cellSize), Mathf.CeilToInt(surface.height / cellSize));

            int[,] grid = new int[(int)gridSize.x, (int)gridSize.y];

            List<Vector2> candidates = new List<Vector2>();
            List<Vector2> potentials = new List<Vector2>();

            float x = Random.Range(grid.GetLength(0) / 4, 3 * grid.GetLength(0) / 4);
            float y = Random.Range(grid.GetLength(1) / 4, 3 * grid.GetLength(1) / 4);
            Vector2 seed = new Vector2(x, y);
            potentials.Add(seed);

            while (potentials.Count > 0)
            {
                // If enough candidates have been spawned end the generation
                if (candidates.Count == numOfCandidates) return candidates;

                // Select one of the existing candidate as reference
                int refIndex = Random.Range(0, potentials.Count);
                Vector2 reference = potentials[refIndex];

                bool candidateAccepted = false;

                for (int i = 0; i < maxSamplingCount; i++)
                {
                    // Create a new candidate outside the circumference of the reference
                    float angle = Random.value * Mathf.PI * 2;
                    // Comment: Unity's unit circle has an 90 degree counter-clockwise offset
                    Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                    float distance = Random.Range(radius, 2*radius);

                    Vector2 newCandidate = reference + (direction * distance);

                    // Check if the new candidate is not within the circumference of other existing candidates
                    if (IsValid(grid, cellSize, radius, candidates, newCandidate))
                    {
                        candidates.Add(newCandidate);
                        potentials.Add(newCandidate);
                        grid[(int)(newCandidate.x / cellSize), (int)(newCandidate.y / cellSize)] = candidates.Count;

                        candidateAccepted = true;
                        break;
                    }
                }

                if (!candidateAccepted)
                    potentials.RemoveAt(refIndex);
            }
            return candidates;
        }

        private static Vector2 ConvertToWorldCoordinates(Rect surface, Vector2 gridSize, float cellSize, Vector2 sample)
        {
            float unitPerGridCellWidth = surface.width / (gridSize.x * cellSize);
            float unitPerGridCellHeight = surface.height / (gridSize.y * cellSize);
            return new Vector2(surface.xMin + (sample.x * unitPerGridCellWidth), surface.yMax - (sample.y * unitPerGridCellHeight));
        }

        private static bool IsValid(int[,] grid, float cellSize, float radius, List<Vector2> points, Vector2 candidate)
        {
            // Get cell index for the candidate
            int cellX = (int)(candidate.x/cellSize);
            int cellY = (int)(candidate.y/cellSize);

            // Check if it's inside the grid
            if (!IsInsideGrid(new Vector2(grid.GetLength(0), grid.GetLength(1)), new Vector2(cellX, cellY))) return false;

            // Check two nearest cells of the candidate for existing candidates (in all directions)
            int startX = Mathf.Max(0, cellX - 2);
            int endX = Mathf.Min(cellX + 2, grid.GetLength(0));
            int startY = Mathf.Max(0, cellY - 2);
            int endY = Mathf.Min(cellY + 2, grid.GetLength(1));

            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    // Get the index of the point residing in cell (x,y)
                    int index = grid[x, y] - 1;

                    // If there exist a point check distance between candidate and the point
                    if (index != -1)
                    {
                        float distance = Mathf.Sqrt((candidate - points[index]).sqrMagnitude);
                        if (distance < radius) return false;
                    }
                }
            }
            return true;
        }

        private static bool IsInsideGrid(Vector2 gridSize, Vector2 candidateCell)
        {
            return candidateCell.x > 0 && candidateCell.x < gridSize.x && candidateCell.y > 0 && candidateCell.y < gridSize.y;
        } 
    }

}