using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponType/ImagePrefab/ImageDirectory")]
public class WeaponDictionary : ScriptableObject
{
    [SerializeField] List<ImagePrefabList> _imagePrefabList;
    private Dictionary<Weapontype, ImagePrefabList> wepImageDictionary;
    private void SetUp()
    {
        wepImageDictionary = new Dictionary<Weapontype, ImagePrefabList>();
        for(int i = 0; i < _imagePrefabList.Count; i++)
        {
            wepImageDictionary.Add(_imagePrefabList[i].GetWeaponType(), _imagePrefabList[i]);            
        }
        
    }
    public ImagePrefab GetImagePrefab(Weapontype wep)
    {
        SetUp();
        wepImageDictionary.TryGetValue(wep, out ImagePrefabList value);
        return value.GetRandomItem();
    }

}
