using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelDesign/PlacementData", fileName = "PlacementData")]
public class PlacementData : ScriptableObject
{
    public Vector3[] treePositions;
    public Vector3[] treeRotations;
    public Vector3[] treeScales;
}
