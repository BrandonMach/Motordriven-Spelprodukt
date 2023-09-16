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
    private Weapon _weapon;
    private bool _purshased;
    private List<string> _naming;
    private void Awake()
    {
        _weapon = new Weapon();
        _naming = new List<string>();
    }
    public void GenerateWeaponPanel(Weapontype type, int level)
    {
        _purshased = false;
        _weapon.SetUpWeapon(type,level, 
            1f, 
            1f, 
            1f);
        _weapon.SetName(GenerateName(type));
        UpdatePanel(level);
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
            Debug.Log(r);
            return _naming[r] + " " + type.name;
        }
        return "";
    }
    private void UpdatePanel(int level)
    {
        if(_weapon.GetWeaponType().GetImage()!=null){_weaponImage = _weapon.GetWeaponType().GetImage(); }
        _weaponNameText.text = _weapon.GetName();
        _weaponLevelText.text = "Level " + level.ToString();      
        _weaponDamageText.text = _weapon.GetDamage().ToString() + " Dmg";
        _weaponSpeedText.text = _weapon.GetSpeed().ToString() + " Spd";
        _weaponRangeText.text = _weapon.GetRange().ToString() + " Range";
        _buttonText.text = (_weapon.GetWeaponType().GetBaseCost()*level).ToString();
    }
    public int Price(){ return (int)_weapon.GetWeaponType().GetBaseCost() * _weapon.GetLevel(); }    
    public Weapon Purshase()
    {
        _weaponImage = null;
        _weaponNameText.text = "";
        _weaponLevelText.text = "";
        _weaponDamageText.text = "";
        _weaponSpeedText.text = "";
        _weaponRangeText.text = "";
        _buttonText.text = "Sold";
        _purshased = true;
        Debug.Log(_weapon.GetName());
        return _weapon;
    }
    public bool Bought() { return _purshased; }
}
