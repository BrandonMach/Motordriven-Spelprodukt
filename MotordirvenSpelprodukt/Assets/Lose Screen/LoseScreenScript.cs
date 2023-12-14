using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreenScript : MonoBehaviour
{

    [Header("Death Cause")]
    public TextMeshProUGUI CauseOfDeathText;
    public static bool KingExecution;
    private Image _image;
    [SerializeField] Sprite[] _deathImages;

    [Header ("Textboxes")]
    [SerializeField] private TextMeshProUGUI _championSlain;
    [SerializeField] private TextMeshProUGUI _minionsKilled;
    [SerializeField] private TextMeshProUGUI _moneyEarned;
    
    void Start()
    {
        _image.GetComponent<Image>();
        if (KingExecution)
        {
            _image.sprite = _deathImages[0];
            CauseOfDeathText.text = "King Execution";
        }
        else
        {
            _image.sprite = _deathImages[1];
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
