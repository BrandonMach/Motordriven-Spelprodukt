using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public static class DrawGizmoAuxiliaries
    {
        public static void DrawSpawnSurfaces(SpawnSurface[] surfaces)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < surfaces.Length; i++)
            {
                float halfwidth = surfaces[i].width / 2;
                float halfheight = surfaces[i].height / 2;
                float centerX = surfaces[i].centerPoint.x;
                float centerZ = surfaces[i].centerPoint.y;
                float y = surfaces[i].altitude;

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

        public static void DrawShapeByType(Vector3[] pos, LayerType type)
        {
            switch (type)
            {
                case LayerType.Tree:
                    DrawTree(pos);
                    break;
            }
        }

        static void DrawTree(Vector3[] pos)
        {
            Gizmos.color = Color.green;
            foreach (var obj in pos) Gizmos.DrawCube(obj, new Vector3(1, 2, 1));
        }

        static void DrawPebbles(Vector3[] pos)
        {
            // TODO
        }

        static void DrawGrass(Vector3[] pos)
        {
            // TODO
        }
    }
}

