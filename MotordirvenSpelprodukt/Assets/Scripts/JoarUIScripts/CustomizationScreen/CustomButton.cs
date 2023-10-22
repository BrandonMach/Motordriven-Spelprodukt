using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : Button
{
    [Header("Used for the Button Text")]
    [SerializeField] public TextMeshProUGUI TMP;
    [SerializeField] public Challenge Challenge;

    // Start is called before the first frame update 
    void Start()
    {
        TMP.text = ($"{Challenge.ChallengeName} : {Challenge.Description}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
