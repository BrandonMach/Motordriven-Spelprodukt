using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class GenerateWeapon : MonoBehaviour
{
    [SerializeField] private Image _weaponImage;
    [SerializeField] private TextMeshProUGUI _weaponNameText;
    [SerializeField] private TextMeshProUGUI _weaponLevelText;
    [SerializeField] private TextMeshProUGUI _weaponDamageText;
    [SerializeField] private TextMeshProUGUI _weaponSpeedText;
    [SerializeField] private TextMeshProUGUI _weaponRangeText;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private WeaponDictionary _weaponDictionary;
    private Weapon _weapon;
    private bool _purshased;
    private List<string> _naming;


    ImagePrefab prefab;

    private int _priceModifyier;
    private void Awake()
    {
        _weapon = new Weapon();
        _naming = new List<string>();
        _priceModifyier = 1;
    }
    public void GenerateWeaponPanel(Weapontype type, int level)
    {

      

        _purshased = false;
        _weapon.SetUpWeapon(type,level, 
            2f, 
            1f, 
            1f);


        prefab = _weaponDictionary.GetImagePrefab(_weapon.GetWeaponType());
        //_weapon.SetName(GenerateName(type));

        GenerateModel();
        UpdatePanel(prefab.GetBaseLevel());
       
    }

    private string GenerateName(Weapontype type)
    {
        _naming.Clear();
        TextAsset textAsset = Resources.Load<TextAsset>(type.GetNameList());
        if (textAsset != null)
        {
            string[] lines = textAsset.text.Split('\n');
            foreach(string line in lines)
            {
                string trimmedLine = line.Trim();
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                   _naming.Add(trimmedLine);
                }
            }
            int r = Random.Range(0, _naming.Count);
            return _naming[r] + " " + type.name;
        }
        return "";
    }
    private void GenerateModel()
    {
      //  ImagePrefab prefab = _weaponDictionary.GetImagePrefab(_weapon.GetWeaponType());
        if (prefab != null)
        {
            _weapon.SetImage(prefab.GetImage());
            _weapon.SetPrefabPath(prefab.GetPath());
            _weaponNameText.text = prefab.GetWeaponName();
        }
    }
    private void UpdatePanel( int level)
    {
        if(_weapon.GetImage()!=null)
        {
            _weaponImage.sprite = _weapon.GetImage(); 
        }
        //_weaponNameText.text = _weapon.GetName();
        _weaponLevelText.text = "Level " + level.ToString();      
        _weaponDamageText.text = _weapon.GetDamage().ToString();
        _weaponSpeedText.text = _weapon.GetSpeed().ToString();          // Tog bort + " dmg/spd/rng" pga att det syns på annat vis i UI
        _weaponRangeText.text = _weapon.GetRange().ToString();
        _buttonText.text = ((int) Price()).ToString();
    }
    public int Price(){ return (int) prefab.GetBasePrice() + (_priceModifyier * /*_weapon.GetLevel()*/ prefab.GetBaseLevel()); }    
    public Weapon Purshase()
    {
        _weaponImage.sprite = null;
        _weaponNameText.text = "";
        _weaponLevelText.text = "";
        _weaponDamageText.text = "";
        _weaponSpeedText.text = "";
        _weaponRangeText.text = "";
        _buttonText.text = "Sold";
        _purshased = true;
        return _weapon;
    }
    public bool Bought() { return _purshased; }
}
