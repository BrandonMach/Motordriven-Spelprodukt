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
       // lines = File.ReadAllLines(Path);
    }

    // Update is called once per frame
    void Update()
    {
        lines = File.ReadAllLines(Path);
    }

    public void SwitchVideoClip(int comboIndex)
    {


        //switch (comboIndex)
        //{

        //    case 0:
        //        _description.text = lines[comboIndex];
        //        break;
        //    case 1:
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //    case 4:
        //        break;
        //    case 5:
        //        break;
        //    case 6:
        //        break;
        //    case 7:
        //        break;
        //    case 8:
        //        break;
        //    case 9:
        //        break;
        //    default:
        //        break;
        //}

        _description.text = lines[comboIndex];
        _videoClip.clip = AttackClips[comboIndex];



    }
}
