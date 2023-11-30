using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameLoopManager : MonoBehaviour
{
    // Is only to be instantiated once, but can be reachable throughout the project with GameManager.Instance.
    #region Singleton

    private static GameLoopManager _instance;
    public static GameLoopManager Instance { get => _instance; set => _instance = value; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of GameLoopManager found");
            return;
        }
        Instance = this;

       

        Vector3 championSpawnPos = new Vector3(_championStartPos.position.x, 1.5f, _championStartPos.position.z);
        Quaternion championRotation = Quaternion.Euler(_championList[KilledChampions].transform.rotation.x, _championList[KilledChampions].transform.rotation.y + 180, _championList[KilledChampions].transform.rotation.z);

        _champion = Instantiate(_championList[KilledChampions], championSpawnPos, championRotation);

        //_champion = GameObject.FindObjectOfType<CMP1Script>();


    }

    /// <summary>
    /// Set your _gameManager variable to this instance in order to achieve the Singleton pattern.
    /// Example: _gameManager = GameManager.Instance
    /// </summary>

    #endregion

    #region ChallengeVariables

    private ChallengeManager _challengeManager;


    float _gameStartTimer;
    float _berserkerTimer;
    float _challengeTimer;
    bool _isTimerActive;

    int _killCount;
    int _knockedUpCount;
    int _knockedOutOfArena;
    int _killstreakKillCount;
    int _totalDeaths;

    int _totalKillcount;
    int _totalKnockUps;
    int _totalKnockedOutOfArena;
    int _highestKillStreakKillCount;

    float _challengeTimerMinion;
    float _challengeTimerChampion;
    bool _championIsDead;
    bool _challengeRequirementsMet;

    bool beenOutOfCombat;


    #endregion


    #region GameLoop


    [SerializeField] EntertainmentManager _etp;

    [SerializeField] Player _player;


    public bool MatchIsFinished;
    public System.EventHandler OnMatchFinished;

    public GameObject[] Canvases;

    #region Champions

    //Champion Path 
    [Header("Champion Path")]

    [SerializeField] public GameObject _champion;
    public int AmountOfChampionsToKill; //CHampion road-map
    public static int KilledChampions;
    [SerializeField] private List<GameObject> _championList;
    [SerializeField] private Transform _championStartPos;
    
    //HP bar

    [SerializeField] private  TextMeshProUGUI _championHPText;
    [SerializeField] private TextMeshProUGUI _championNameText;
    #endregion


    #region Camera

    [SerializeField] SwitchCamera CamManager;
    private bool _kingCam;
    #endregion


    #region Minions

    //Enemy List
    public  GameObject[] EnemyGameObjects;
    private SpawnEnemy _spawnEnemy;

    #endregion

    #endregion

    //public int KillCount { get => _killCount; set => _killCount = value; }
    public int KillCount
    {
        get { return _killCount; }

        set
        {
            _player.HasTakenDamage = false;
            _killstreakKillCount++;

            // Stores highest killstreak achieved, used for stats
            if (_killstreakKillCount > HighestKillStreakKillCount)
            {
                HighestKillStreakKillCount = _killstreakKillCount;
            }

            _killCount = value;

            //Debug.Log($"KillstreakCount: {_killstreakKillCount}");
        }
    }

    public int KnockedUpCount { get => _knockedUpCount; set => _knockedUpCount = value; }
    public int KnockedOutOfArena { get => _knockedOutOfArena; set => _knockedOutOfArena = value; }
    public int TotalKillcount { get => _totalKillcount; set => _totalKillcount = value; }
    public int TotalKnockUps { get => _totalKnockUps; set => _totalKnockUps = value; }
    public int TotalKnockedOutOfArena { get => _totalKnockedOutOfArena; set => _totalKnockedOutOfArena = value; }
    public int HighestKillStreakKillCount { get => _highestKillStreakKillCount; set => _highestKillStreakKillCount = value; }
    public int TotalDeaths { get => _totalDeaths; set => _totalDeaths = value; }
    public bool BeenOutOfCombat { get => beenOutOfCombat; set => beenOutOfCombat = value; }

    public event EventHandler OnChampionKilled;

    public void UpdateEnemyList()
    {
        EnemyGameObjects =   GameObject.FindGameObjectsWithTag("EnemyTesting");
    }

    void Start()
    {
        _challengeManager = ChallengeManager.Instance;
        _gameStartTimer = 0;
        _berserkerTimer = 0;

      
        _player = Player.Instance;
       
        CamManager = GameObject.FindWithTag("CamManager").GetComponent<SwitchCamera>();
        _etp = EntertainmentManager.Instance;


        

        _spawnEnemy = SpawnEnemy.Instance;

        AmountOfChampionsToKill = 2;

        OnMatchFinished += MatchFinished;
        UpdateEnemyList();
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var canvas in Canvases)
        {
            canvas.SetActive(!PauseMenu.GameIsPaused);
        }

       


        if (_champion != null)
        {

            _championNameText.text = _champion.GetComponent<CMP1Script>().ChampionName;
            var championHealthManager = _champion.GetComponent<HealthManager>();
            _championHPText.text = (championHealthManager.CurrentHealthPoints / championHealthManager.MaxHP * 100).ToString() + "%";
        }

        
        if (_champion == null && !_kingCam)
        {
            OnMatchFinished?.Invoke(this, EventArgs.Empty);
        }

        //If player dies ... Simon jobbar med att flytta Healthmanager och i Damage

        if(_player == null)
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }

        // Testing challenges
        CheckChallengesCompletion();
        ChallengeTimersUpdate();
    }


    #region MatchFinished

    private void MatchFinished(object sender, EventArgs e)
    {
        SettingStatistics();

        MatchIsFinished = true; //Stop the match
        

        foreach (var canvas in Canvases)
        {
            canvas.SetActive(MatchIsFinished);
        }


        foreach (var enemies in EnemyGameObjects)
        {
            Destroy(enemies);
        }

        if (_etp.GetETP() > (_etp.GetMaxETP() / 2))
        {
            GameManager.ChampionsKilled++;
            Debug.LogError("Champions Killed" + GameManager.ChampionsKilled);
            GameManager.Instance.RewardCoins(GameManager.ChampionsKilled * 33);
            if (GameManager.ChampionsKilled == AmountOfChampionsToKill)
            {
                Debug.Log("You killed all champions");
            }
        }
        else
        {
     
           _player.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

        _kingCam = true;
        CamManager.GoToKingCam();
        OnChampionKilled?.Invoke(this, EventArgs.Empty);

       // _championIsDead = true;
        GameManager.Instance._championIsDeadX = true;
        _championIsDead = true;
        

       
    }

    /// <summary>
    /// Updates variables for statistics and resets challengecounters (except killstreak)
    /// </summary>
    private void SettingStatistics()
    {
        GameManager.Instance.TotalKillcount = GameManager.Instance.KillCount;
        GameManager.Instance.TotalKnockUps = GameManager.Instance.KnockedUpCount;
        GameManager.Instance.TotalKnockedOutOfArena = GameManager.Instance.KnockedOutOfArena;

        GameManager.Instance.KillCount = 0;
        GameManager.Instance.KnockedUpCount = 0;
        GameManager.Instance.KnockedOutOfArena = 0;
    }
    #endregion


    // Kommer föras över till ChallengeManager ifall det finns tid
    // Problem: CheckChallengesCompletion() kollas först efter en fight är avgjord
    #region ChallengeMethods

    private void HandleChallengeCompleted(Challenge completedChallenge)
    {
        GameManager.Instance.RewardCoins( completedChallenge.Reward);
        completedChallenge.IsCompleted = true;
        _challengeManager.DeActivateChallenge(completedChallenge);
        _challengeManager.RemoveChallenge(completedChallenge);
        Debug.Log("Challenge completed " + completedChallenge.ChallengeName);
        Debug.Log("PlayerCoins = " + GameManager.PlayerCoins);
        //completedChallenge.ChallengeButton.SetActive(false);
    }

    /// <summary>
    /// Checks completion condition for each active challenge
    /// </summary>
    public void CheckChallengesCompletion()
    {
        if (_challengeManager == null || _challengeManager.ActiveChallenges.Count == 0)
        {
            return;
        }

        //Used for safe modifying of _challengeManager.ActiveChallenges during run-time
        List<Challenge> copyOfActiveChallenges = new List<Challenge>(_challengeManager.ActiveChallenges);

        foreach (Challenge challenge in copyOfActiveChallenges)
        {

            if (challenge is TimeChallenge timeChallenge)
            {
                if (TimeChallengeCheck(timeChallenge))
                {
                    HandleChallengeCompleted(timeChallenge);
                }
            }
            else if (challenge is Challenge combatChallenge)
            {
                if (CombatChallengeCheck(combatChallenge))
                {
                    HandleChallengeCompleted(combatChallenge);
                }
            }
        }
    }

    /// <summary>
    /// Contains all checks for TimeChallenges and returns true for the completed ones
    /// </summary>
    /// <param name="challenge"></param>
    /// <returns></returns>
    private bool TimeChallengeCheck(TimeChallenge challenge)
    {
        return BerserkerCheck(challenge) || ChampionslayerCheck(challenge);
    }

    /// <summary>
    /// Contains all checks for CombatChallenges and returns true for the completed ones
    /// </summary>
    /// <param name="challenge"></param>
    /// <returns></returns>
    private bool CombatChallengeCheck(Challenge challenge)
    {
        return KillstreakCheck(challenge) || MuggleCheck(challenge) ||
               AthleticCheck(challenge) || KnockEmUpCheck(challenge) ||
               ThisIsSpartaCheck(challenge) || StraightEdgeCheck(challenge) ||
               FearlessCheck(challenge);
    }

    // Tested and works
    private bool BerserkerCheck(TimeChallenge timeChallenge)
    {

        if (_player.HasTakenDamage)
        {
            _berserkerTimer = 0f;
            _player.HasTakenDamage = false;
            Debug.Log("_berserkerTimer has been reset");
        }

        if (timeChallenge.ChallengeName == "Berserker" && timeChallenge.TimeForCompletion >= _berserkerTimer && _killCount >= timeChallenge.Requirement)
        {
            return true;
        }
        return false;
    }

    // Tested and works
    private bool ChampionslayerCheck(TimeChallenge timeChallenge)
    {
        if (timeChallenge.ChallengeName == "Championslayer" && timeChallenge.TimeForCompletion >= _gameStartTimer && _championIsDead)
        {
            return true;
        }
        return false;
    }

    // Tested and works
    private bool KillstreakCheck(Challenge challenge)
    {
        if (_player.HasTakenDamage)
        {
            _killstreakKillCount = 0;
            Debug.Log("_killStreakKillCount has been reset");
        }

        if (challenge.ChallengeName == "Killstreak" && !_player.HasTakenDamage && _killstreakKillCount >= challenge.Requirement)
        {
            return true;
        }
        return false;
    }

    private bool MuggleCheck(Challenge challenge)
    {
        if (challenge.ChallengeName == "Muggle" /* && !bool abilitiesUsed */)
        {
            return true;
        }
        return false;
    }

    private bool AthleticCheck(Challenge challenge)
    {
        if (challenge.ChallengeName == "Athletic" /* && !bool damageFromKamikaze */)
        {
            return true;
        }
        return false;
    }

    //Tested, works
    private bool KnockEmUpCheck(Challenge challenge)
    {
        if (challenge.ChallengeName == "Knock 'em Up" && _knockedUpCount >= challenge.Requirement )
        {
            return true;
        }
        return false;
    }

    private bool ThisIsSpartaCheck(Challenge challenge)
    {
        if (challenge.ChallengeName == "This is Sparta!" && _knockedOutOfArena >= challenge.Requirement )
        {
            return true;
        }
        return false;
    }

    private bool StraightEdgeCheck(Challenge challenge)
    {
        if (challenge.ChallengeName == "Straight Edge" /* && !bool potionsUsed */)
        {
            return true;
        }
        return false;
    }

    private bool FearlessCheck(Challenge challenge)
    {
        if (challenge.ChallengeName == "Fearless" && !BeenOutOfCombat)
        {
            return true;
        }
        return false;
    }

    public void ChallengeTimersUpdate()
    {
        _gameStartTimer += Time.deltaTime;
        _berserkerTimer += Time.deltaTime;
    }


    


    #endregion



}
