using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public static class SpawnerAuxiliaries
    { 
        public static Vector3[] SpawnPositionsByType(ObjectData[] data, LayerType type)
        {
            foreach (var obj in data)
                if (obj.type == type)
                    return obj.spawnPositions;

            return null;
        }

        public static Vector3[] SpawnRotationsByType(ObjectData[] data, LayerType type)
        {
            foreach (var obj in data)
                if (obj.type == type)
                    return obj.spawnRotations;

            return null;
        }

        public static Vector3[] SpawnScalesByType(ObjectData[] data, LayerType type)
        {
            foreach (var obj in data)
                if (type == obj.type)
                    return obj.spawnScales;

            return null;
        }

        public static Rect SurfaceToRect(SpawnSurface s)
        {
            float centerX = s.centerPoint.x;
            float centerZ = s.centerPoint.y;
            float halfWidth = s.width / 2;
            float halfHeight = s.height / 2;
            Vector2 min = new Vector2(centerX - halfWidth, centerZ - halfHeight);
            return new Rect(min.x, min.y, s.width, s.height);
        }

       
    }
}

