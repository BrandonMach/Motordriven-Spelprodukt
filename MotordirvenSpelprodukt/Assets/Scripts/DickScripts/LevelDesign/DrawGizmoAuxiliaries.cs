using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{
    public static class DrawGizmoAuxiliaries
    {
        public static void DrawShapeByType(Vector3[] pos, GameObjectType type)
        {
            switch (type)
            {
                case GameObjectType.Tree:
                    DrawTree(pos);
                    break;
            }
        }

        static void DrawTree(Vector3[] pos)
        {
            Gizmos.color = Color.green;
            foreach (var obj in pos) Gizmos.DrawCube(obj, new Vector3(1, 2, 1));
        }
    }
}

