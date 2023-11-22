using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public static class DrawGizmoAuxiliaries
    {
        private static Vector3 GizmoDefaultCube => new Vector3(0.5f, 1, 0.5f);
        public static void DrawSpawnSurfaces(LayerType type, SpawnSurface[] surfaces, float altitudeOffset)
        {
            SetGizmoColorByLayerType(type);
            for (int i = 0; i < surfaces.Length; i++)
            {
                float halfwidth = surfaces[i].xWidth / 2;
                float halfheight = surfaces[i].yWidth / 2;
                float centerX = surfaces[i].centerPoint.x;
                float centerZ = surfaces[i].centerPoint.y;
                float y = surfaces[i].spawnAltitude+altitudeOffset;

                Vector3 tl = new Vector3(centerX - halfwidth, y, centerZ + halfheight);
                Vector3 tr = new Vector3(centerX + halfwidth, y, centerZ + halfheight);
                Vector3 bl = new Vector3(centerX - halfwidth, y, centerZ - halfheight);
                Vector3 br = new Vector3(centerX + halfwidth, y, centerZ - halfheight);

                Gizmos.DrawLine(tl, tr);
                Gizmos.DrawLine(bl, br);
                Gizmos.DrawLine(tl, bl);
                Gizmos.DrawLine(tr, br);
            }
        }

        public static void DrawShapesBySpawnType(LayerType type, Vector3[] positions)
        {
            SetGizmoColorByLayerType(type);

            switch (type)
            {
                case LayerType.Tree:
                    DrawTree(positions);
                    break;
                case LayerType.Ground:
                    DrawGround(positions);
                    break;
                case LayerType.Vegetation:
                    DrawGrass(positions); 
                    break;
                case LayerType.ArenaProps:
                    DrawArenaProps(positions);
                    break;
            }
        }

        static void DrawTree(Vector3[] positions)
        {
            foreach (Vector3 pos in positions) Gizmos.DrawCube(pos, GizmoDefaultCube);
        }

        static void DrawGround(Vector3[] positions)
        {
            foreach (Vector3 pos in positions) Gizmos.DrawSphere(pos, 0.25f);
        }

        static void DrawGrass(Vector3[] positions)
        {
            foreach (Vector3 pos in positions) Gizmos.DrawWireCube(pos, GizmoDefaultCube);
        }

        static void DrawArenaProps(Vector3[] positions)
        {
            foreach (Vector3 pos in positions) Gizmos.DrawSphere(pos, 0.25f);
        }

        static void SetGizmoColorByLayerType(LayerType type)
        {
            switch (type)
            {
                case LayerType.Tree:
                    Gizmos.color = Color.green;
                    break;
                case LayerType.Ground:
                    Gizmos.color = Color.red;
                    break;
                case LayerType.Vegetation:
                    Gizmos.color = Color.yellow;
                    break;
                case LayerType.ArenaProps:
                    Gizmos.color = Color.magenta;
                    break;
            }
        }
    }
}

