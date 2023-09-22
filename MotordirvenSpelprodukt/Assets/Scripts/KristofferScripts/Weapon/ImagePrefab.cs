using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "WeaponType/ImagePrefab/ImagePrefab")]
public class ImagePrefab : ScriptableObject
{
    [SerializeField] private Sprite _image;
    [SerializeField] private string prefabPath;
    public Sprite GetImage(){ return _image; }
    public string GetPath(){ return prefabPath; }
}
