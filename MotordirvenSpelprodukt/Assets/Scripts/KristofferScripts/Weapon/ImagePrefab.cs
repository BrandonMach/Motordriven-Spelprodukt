using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "WeaponType/ImagePrefab/ImagePrefab")]
public class ImagePrefab : ScriptableObject
{
    [SerializeField] private Texture2D _image;
    [SerializeField] private string prefabPath;
    public Texture2D GetImage(){ return _image; }
    public string GetPath(){ return prefabPath; }
}
