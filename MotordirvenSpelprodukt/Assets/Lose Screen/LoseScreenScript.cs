using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoseScreenScript : MonoBehaviour
{

    [Header("Death Cause")]
    public TextMeshProUGUI CauseOfDeathText;
    public static bool KingExecution;


    [Header ("Textboxes")]
    [SerializeField] private TextMeshProUGUI _championSlain;
    [SerializeField] private TextMeshProUGUI _minionsKilled;
    [SerializeField] private TextMeshProUGUI _moneyEarned;
    
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
        _championSlain.text = "Champions Slain: " +  GameManager.ChampionsKilled.ToString();
        _moneyEarned.text = "Total money earned:" + GameManager.Instance.TotalMoneyEarned.ToString();
        _minionsKilled.text = "Minions Killed: " + GameManager.Instance.GameManagerKillCount.ToString();

    }

    public void RestartGame()
    {
        GameManager.Instance.Reset();
        SceneManager.LoadScene(1, LoadSceneMode.Single);

    }
}
