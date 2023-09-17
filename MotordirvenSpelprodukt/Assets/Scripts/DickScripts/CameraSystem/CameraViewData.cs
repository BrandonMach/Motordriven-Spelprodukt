using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CameraSystem/ViewData", fileName = "CameraViewData")]
public class CameraViewData : ScriptableObject
{
    [SerializeField, TextArea(1, 3)]
    string description;

    [Header("Camera Translation")]
    public Vector3 posOffset;
    public Vector3 angleOffset;
    public float offsetMargin = 0.5f;

    [Header("Camera settings")]
    public float movementSpeed = 0.75f;
}
