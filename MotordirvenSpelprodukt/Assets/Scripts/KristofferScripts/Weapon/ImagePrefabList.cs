using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponType/ImagePrefab/ImagePrefabList")]
public class ImagePrefabList : ScriptableObject
{
    [SerializeField] private List<ImagePrefab> _imagePrefabList = new List<ImagePrefab>();
    [SerializeField] private Weapontype _weaponType;

    public ImagePrefab GetRandomItem()
    {
        int r = Random.Range(0, _imagePrefabList.Count);
        return _imagePrefabList[r];
    }
    public Weapontype GetWeaponType()
    {
        return _weaponType;
    }
    public List<ImagePrefab> GetImageList()
    {
        return _imagePrefabList;
    }
}
