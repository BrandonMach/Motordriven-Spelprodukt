using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenScript : MonoBehaviour
{
    [Header("Win Cause")]
    public TextMeshProUGUI CauseOfDeathText;
    //public static bool KingExecution;
    private Image _image;
    [SerializeField] Sprite[] _winImages;

    [Header("Textboxes")]
    [SerializeField] private TextMeshProUGUI _championSlain;
    [SerializeField] private TextMeshProUGUI _minionsKilled;
    [SerializeField] private TextMeshProUGUI _moneyEarned;

    [SerializeField] private TextMeshProUGUI _buttonText;

    void Start()
    {
        _image = gameObject.GetComponent<Image>();
        if (GameManager.FreedomWin)
        {
            _image.sprite = _winImages[0];
            CauseOfDeathText.text = "Bought your freedom";
            _buttonText.text = "Start over";
        }
        if(GameManager.KilledAllChampions)
        {
            _image.sprite = _winImages[1];
            CauseOfDeathText.text = "Slayed all champions";
            _buttonText.text = "Townhall";

        }
    }

    // Update is called once per frame
    void Update()
    {
        _championSlain.text = "Champions Slain: " + GameManager.ChampionsKilled.ToString();
        _moneyEarned.text = "Total money earned:" + (GameManager.Instance.TotalMoneyEarned-50).ToString(); //50 is start money
        _minionsKilled.text = "Minions Killed: " + GameManager.Instance.GameManagerKillCount.ToString();

    }

    public void GoToHUb()
    {
        if (GameManager.KilledAllChampions)
        {
            SceneManager.LoadScene(6, LoadSceneMode.Single);         
        }

        if(GameManager.FreedomWin)
        {
            GameManager.Instance.Reset();
            SceneManager.LoadScene(1, LoadSceneMode.Single);      
        }
       

    }
}
