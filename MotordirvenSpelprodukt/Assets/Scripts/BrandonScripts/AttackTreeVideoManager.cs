using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using System.IO;

public class AttackTreeVideoManager : MonoBehaviour
{

    [SerializeField] private VideoPlayer _videoClip;
    [SerializeField] private TextMeshProUGUI _description;

    public VideoClip[] AttackClips;
    //Lägg till path till vapen typ eller vapen
    public string Path;
    
    public int ComboIndex;

    string[] lines;

    void Start()
    {
        //Resources.Load(Path);

        var textFile = Resources.Load<TextAsset>(Path);

        var yo= textFile.ToString();
        Debug.LogWarning(yo);


        lines = yo.Split(",");
        

        for (int i = 0; i < lines.Length; i++)
        {
            Debug.Log( lines[i].Trim('\n') + " " + i);
        }

     
       // lines = File.ReadAllLines(Path);
       
    }

    // Update is called once per frame
    void Update()
    {
       // Path = GameObject.FindObjectOfType<TransferableScript>().GetWeapon().GetAttackTreeDescriptionPath();
        //lines = File.ReadAllLines(Path);
    }

    public void SwitchVideoClip(int comboIndex)
    {
        ComboIndex = comboIndex;
        _videoClip.clip = AttackClips[comboIndex];
        _description.text = lines[comboIndex];
       



    }
}
