using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [Header("Textboxes")]
    [SerializeField] private TextMeshProUGUI _championSlain;
    [SerializeField] private TextMeshProUGUI _minionsKilled;
    [SerializeField] private TextMeshProUGUI _moneyEarned;
    [SerializeField] private TextMeshProUGUI _timePlayed;

    // Update is called once per frame
    void Update()
    {
        //_championSlain.text = "Champions Slain: " + GameManager.ChampionsKilled.ToString();
        _moneyEarned.text = "Total money earned:" + (GameManager.Instance.TotalMoneyEarned).ToString();
        _minionsKilled.text = "Minions Killed: " + GameLoopManager.Instance.KillCount.ToString();
        //_timePlayed.text = "Time Fighting: " + GameManager.Instance.GameStartTimerX.ToString();

    }

    public void ContinueGame()
    {
        GameManager.ArenaLayoutIndex++;
        GameManager.BattleIndex++;
        SceneManager.LoadScene(6, LoadSceneMode.Single);
    }
}
