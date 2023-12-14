using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "WeaponType/ImagePrefab/ImagePrefab")]
public class ImagePrefab : ScriptableObject
{
    [SerializeField] private Texture2D _image;
    [SerializeField] private string prefabPath;
    //New stuff
    [SerializeField] private GameObject _prefabObject;
    [SerializeField] private string _weaponName;
    [SerializeField] private int _weaponBaseLevel;
    [SerializeField] private int _weaponBasePrice;
    public Sprite GetImage(){
        Sprite spr = Sprite.Create(_image, new Rect(0,0,_image.width,_image.height), Vector2.zero);
        return spr; 
    }
    public string GetPath(){ return prefabPath; }


    //New stuff

    public GameObject GetModelPrefab() { return _prefabObject; }
    public string GetWeaponName() { return _weaponName; }
    public int GetBaseLevel() { return _weaponBaseLevel; }
    public int GetBasePrice() { return _weaponBasePrice; }
}
