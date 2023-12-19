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
        // Used for auto writing button text
        //TMP.text = ($"{Challenge.ChallengeName}");

        //TMP.text = ($"{Challenge.ChallengeName} |  {Challenge.Description}");
        onClick.AddListener(delegate () { ChallengeManager.Instance.ActivateOrDeactivateChalleng(Challenge); });

    }

    // Update is called once per frame
    void Update()
    {
        //TMP.text = ($"{Challenge.ChallengeName}");
        
        
        //onClick.AddListener(delegate () { ChallengeManager.Instance.ActivateOrDeactivateChalleng(Challenge); });

    }

    public void UpdateButtonInfo()
    {
        TMP.text = ($"{Challenge.ChallengeName}");


        onClick.AddListener(delegate () { ChallengeManager.Instance.ActivateOrDeactivateChalleng(Challenge); });
    }

    
}
