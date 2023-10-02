using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "WeaponType/ImagePrefab/ImagePrefab")]
public class ImagePrefab : ScriptableObject
{
    [SerializeField] private Texture2D _image;
    [SerializeField] private string prefabPath;
    public Sprite GetImage(){
        Sprite spr = Sprite.Create(_image, new Rect(0,0,_image.width,_image.height), Vector2.zero);
        return spr; 
    }
    public string GetPath(){ return prefabPath; }
}
