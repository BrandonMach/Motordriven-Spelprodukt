using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Is only to be instantiated once, but can be reachable throughout the project with GameManager.Instance.
    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance { get => _instance; set => _instance = value; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }
        Instance = this;

        Vector3 championSpawnPos = new Vector3(_championStartPos.position.x, 1.5f, _championStartPos.position.z);
        Quaternion championRotation = Quaternion.Euler(_championList[KilledChampions].transform.rotation.x, _championList[KilledChampions].transform.rotation.y + 180, _championList[KilledChampions].transform.rotation.z);

        Instantiate(_championList[KilledChampions], championSpawnPos, championRotation);

    }

    /// <summary>
    /// Set your _gameManager variable to this instance in order to achieve the Singleton pattern.
    /// Example: _gameManager = GameManager.Instance
    /// </summary>

    #endregion

    #region ChallengeVariables

    private ChallengeManager _challengeManager;


    float _gameStartTimer;
    float _challengeTimer;
    bool _isTimerActive;

    int _killCount;
    float _challengeTimerMinion;
    float _challengeTimerChampion;
    bool _championIsDead;
    bool _challengeRequirementsMet;

    int _killstreakKillCount;



    #endregion


    #region GameLoop


    [SerializeField] EntertainmentManager _etp;
    [SerializeField] GameObject _playerGO;
    [SerializeField] Player _player;

    public static int PlayerCoins; //Static s� att anadra scener kan f� access

    public bool MatchIsFinished;
    public System.EventHandler OnMatchFinished;

    #region Champions

    //Champion Path 
    [Header("Champion Path")]
    [SerializeField] UnityEngine.Object _champion;
    public int AmountOfChampionsToKill; //CHampion road-map
    public static int KilledChampions;
    [SerializeField] private List<GameObject> _championList;
    [SerializeField] private Transform _championStartPos;
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
            _killCount = value;

            Debug.Log($"KillstreakCount: {_killstreakKillCount}");
        }
    }

    public event EventHandler OnChampionKilled;

    public void UpdateEnemyList()
    {
        EnemyGameObjects =   GameObject.FindGameObjectsWithTag("EnemyTesting");
    }

    void Start()
    {
        _challengeManager = ChallengeManager.Instance;
        _gameStartTimer = 0;

        _champion = GameObject.FindObjectOfType<CMP1Script>();
        _playerGO = GameObject.FindGameObjectWithTag("Player");
        CamManager = GameObject.FindWithTag("CamManager").GetComponent<SwitchCamera>();
        _etp = EntertainmentManager.Instance;


        //F�r testing
        PlayerCoins = 50;
        Debug.Log("Coins" + PlayerCoins);

        //Not used
        //_challengeManager.OnChallengeCompleted += HandleChallengeCompleted;
        _spawnEnemy = SpawnEnemy.Instance;

        AmountOfChampionsToKill = 2;

        OnMatchFinished += MatchFinished;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_champion == null && !_kingCam)
        {
            OnMatchFinished?.Invoke(this, EventArgs.Empty);
        }

        //If player dies ... Simon jobbar med att flytta Healthmanager och i Damage

        if(_playerGO == null)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }

        // Testing challenges
        CheckChallengesCompletion();
        ChallengeTimersUpdate();
    }


    #region MatchFinished

    private void MatchFinished(object sender, EventArgs e)
    {
        MatchIsFinished = true; //Stop the match

        foreach (var enemies in EnemyGameObjects)
        {
            Destroy(enemies);
        }

        if (_etp.GetETP() > (_etp.GetMaxETP() / 2))
        {
            KilledChampions++;
            Debug.LogError("Champions Killed" + KilledChampions);
            if (KilledChampions == AmountOfChampionsToKill)
            {
                Debug.Log("You killed all champions");
            }
        }
        else
        {
            _playerGO.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

        _kingCam = true;
        CamManager.GoToKingCam();
        OnChampionKilled?.Invoke(this, EventArgs.Empty);

        _championIsDead = true;
    }
    #endregion



    #region ChallengeMethods

    private void HandleChallengeCompleted(Challenge completedChallenge)
    {
        PlayerCoins += completedChallenge.Reward;
        completedChallenge.IsCompleted = true;
        _challengeManager.DeActivateChallenge(completedChallenge);
        _challengeManager.RemoveChallenge(completedChallenge);
        Debug.Log("Challenge completed " + completedChallenge.ChallengeName);
        Debug.Log("PlayerCoins = " + PlayerCoins);
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

        if (timeChallenge.ChallengeName == "Berserker" && timeChallenge.TimeForCompletion >= _gameStartTimer && _killCount >= timeChallenge.Requirement)
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

    private bool KnockEmUpCheck(Challenge challenge)
    {
        if (challenge.ChallengeName == "Knock 'em Up" /* && int knockUps >= challenge.Requirement */)
        {
            return true;
        }
        return false;
    }

    private bool ThisIsSpartaCheck(Challenge challenge)
    {
        if (challenge.ChallengeName == "This is Sparta!" /* && int outOfArena >= challenge.Requirement */)
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
        if (challenge.ChallengeName == "Fearless" /* && bool outOfCombat */)
        {
            return true;
        }
        return false;
    }

    public void ChallengeTimersUpdate()
    {
        _gameStartTimer += Time.deltaTime;
    }


    


    #endregion



}
