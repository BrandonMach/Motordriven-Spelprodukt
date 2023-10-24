using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoseScreenScript : MonoBehaviour
{

    [Header("Death Cause")]
    public TextMeshProUGUI CauseOfDeathText;
    public static bool KingExecution;
    void Start()
    {
        if (KingExecution)
        {
            CauseOfDeathText.text = "King Execution";
        }
        else
        {
            CauseOfDeathText.text = "Slain in battle";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
