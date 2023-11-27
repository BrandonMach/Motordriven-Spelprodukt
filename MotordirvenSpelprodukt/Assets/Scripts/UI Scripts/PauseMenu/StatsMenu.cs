using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsMenu : MenuAbstract, IMenu
{

    [SerializeField] TextMeshProUGUI _totalKills;
    [SerializeField] TextMeshProUGUI _totalKnockUps;
    [SerializeField] TextMeshProUGUI _totalOutOfArenas;
    [SerializeField] TextMeshProUGUI _highestKillstreak;
    [SerializeField] TextMeshProUGUI _totalDeaths;

    GameLoopManager _gameLoopManager;



    // Start is called before the first frame update
    void Start()
    {
        if (GameLoopManager.Instance != null)
        {
            _gameLoopManager = GameLoopManager.Instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatisticsTMPs();
        ClickESC();
    }

    public void ClickESC()
    {
        base.ClickESC();
    }

    public override void ClickBack()
    {
        base.ClickBack();
    }

    private void UpdateStatisticsTMPs()
    {
        if (GameLoopManager.Instance != null)
        {

            _totalKills.text = $"Total kills: {GameManager.Instance.KillCount}";
            _totalKnockUps.text = $"Total knock ups: {GameManager.Instance.TotalKnockUps}";
            _totalOutOfArenas.text = $"Total out of arenas: {GameManager.Instance.TotalKnockedOutOfArena}";
            _highestKillstreak.text = $"Highest killstreak: {GameManager.Instance.HighestKillStreakKillCount}";
            _totalDeaths.text = $"Total deaths: {GameManager.Instance.TotalDeaths}";
        }

    }
}
