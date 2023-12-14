using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


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

        Debug.developerConsoleVisible = true;

    }



    #endregion

    #region Scens

    public enum CurrentScen
    {
        MainMenuScene,
        CustomizationScene,
        ArenaScen,
        ShopScen,
        HUBWorld
    }

    public CurrentScen _currentScen;


    Scene currentScene;

    #endregion

    public static float PlayerCoins; //Static så att anadra scener kan få access

    #region UsedForFmod

    public delegate void EnterMainMenuEvent();
    public static event EnterMainMenuEvent OnMainMenuEnter;

    public delegate void EnterArenaEvent();
    public static event EnterArenaEvent OnArenaEnter;

    public delegate void AfterArenaEvent();
    public static event AfterArenaEvent OnAfterArenaEnter;

    public delegate void EnterOpenWorldEvent();
    public static event EnterOpenWorldEvent OnOpenWorldEnter;

    bool _beenIntoArena = false;
    private bool _mainMenuEventInvoked = false;
    private bool _afterArenaEventInvoked = false;
    private bool _arenaEventInvoked = false;
    private bool _openWorldEventInvoked = false;

    #endregion

    #region ChallengeVariables

    private ChallengeManager _challengeManager;


    float _gameStartTimerX;
    float _berserkerTimerX;
    float _challengeTimerX;
    bool _isTimerActive;

    int _killCountX;
    int _knockedUpCountX;
    int _knockedOutOfArenaX;
    int _killstreakKillCountX;
    int _totalDeathsX;

    int _totalKillcountX;
    int _totalKnockUpsX;
    int _totalKnockedOutOfArenaX;
    int _highestKillStreakKillCountX;

    float _challengeTimerMinionX;
    float _challengeTimerChampionX;
    public bool _championIsDeadX;
    bool _challengeRequirementsMetX;

    bool beenOutOfCombatX;


    public int KillCount
    {
        get { return _killCountX; }

        set
        {
            _player.HasTakenDamage = false;
            _killstreakKillCountX++;

            // Stores highest killstreak achieved, used for stats
            if (_killstreakKillCountX > HighestKillStreakKillCount)
            {
                HighestKillStreakKillCount = _killstreakKillCountX;
            }

            _killCountX = value;

            //Debug.Log($"KillstreakCount: {_killstreakKillCount}");
        }
    }


    public int KnockedUpCount { get => _knockedUpCountX; set => _knockedUpCountX = value; }
    public int KnockedOutOfArena { get => _knockedOutOfArenaX; set => _knockedOutOfArenaX = value; }
    public int TotalKillcount { get => _totalKillcountX; set => _totalKillcountX = value; }
    public int TotalKnockUps { get => _totalKnockUpsX; set => _totalKnockUpsX = value; }
    public int TotalKnockedOutOfArena { get => _totalKnockedOutOfArenaX; set => _totalKnockedOutOfArenaX = value; }
    public int HighestKillStreakKillCount { get => _highestKillStreakKillCountX; set => _highestKillStreakKillCountX = value; }
    public int TotalDeaths { get => _totalDeathsX; set => _totalDeathsX = value; }
    public bool BeenOutOfCombat { get => beenOutOfCombatX; set => beenOutOfCombatX = value; }


    #endregion



   [SerializeField] public Player _player;
    


    public static int ChampionsKilled;
    public float TotalMoneyEarned;
    public float GameManagerKillCount;
    public static int ArenaLayoutIndex;

    public System.EventHandler OnRestartGame;



    void Start()
    {

        //ArenaLayoutIndex = 0;
        //För testing
        PlayerCoins = 50;
        //Debug.Log("Coins" +     PlayerCoins);
        DontDestroyOnLoad(gameObject);
        TotalMoneyEarned = PlayerCoins;

        _challengeManager = ChallengeManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        if(Player.Instance != null)
        {
            _player = Player.Instance;
        }
       
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex == 1)
        {
            #region FMOD

            if (!_mainMenuEventInvoked)
            {
                OnMainMenuEnter?.Invoke();
                _mainMenuEventInvoked = true;
            }

            #endregion

            _currentScen = CurrentScen.MainMenuScene;

        }
        else
        {
            // Used for FMOD
            _mainMenuEventInvoked = false;
        }

        if (currentScene.buildIndex == 2)
        {

            #region FMOD

            if (!_afterArenaEventInvoked && _beenIntoArena)
            {
                OnAfterArenaEnter?.Invoke();
                _afterArenaEventInvoked = true;
            }

            #endregion


            foreach (var oldActiveChallenges in ChallengeManager.Instance.ActiveChallenges)
            {
                ChallengeManager.Instance.RemoveChallenge(oldActiveChallenges);
            }
            
            _currentScen = CurrentScen.CustomizationScene;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        else
        {
            // Used for FMOD
            _afterArenaEventInvoked = false;
        }

        if (currentScene.buildIndex == 3)
        {
            #region FMOD

            if (!_arenaEventInvoked)
            {
                OnArenaEnter?.Invoke();
                _arenaEventInvoked = true;
                _beenIntoArena = true;
            }

            #endregion

            CheckChallengesCompletion();
            ChallengeTimersUpdate();
            _currentScen = CurrentScen.ArenaScen;


            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        else
        {
            // Used for FMOD
            _arenaEventInvoked = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }




        if (currentScene.buildIndex == 6 || _currentScen == GameManager.CurrentScen.HUBWorld)
        {


            #region FMOD

            if (!_openWorldEventInvoked)
            {
                OnOpenWorldEnter?.Invoke();
                _openWorldEventInvoked = true;
            }

            #endregion


            _currentScen = CurrentScen.HUBWorld;
            GetComponent<SlowMo>()._returnSlowMo = true;


        }






    }


  



    #region Money
    public void RewardCoins(float amount)
    {
        PlayerCoins += amount;
        TotalMoneyEarned += amount;
    }

    public void RemoveCoins(float amount)
    {
        PlayerCoins -= amount;
    }
    #endregion


    #region ChallengeMethods

    private void HandleChallengeCompleted(Challenge completedChallenge)
    {
        GameManager.Instance.RewardCoins(completedChallenge.Reward);
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
            _berserkerTimerX = 0f;
            _player.HasTakenDamage = false;
            Debug.Log("_berserkerTimer has been reset");
        }

        if (timeChallenge.ChallengeName == "Berserker" && timeChallenge.TimeForCompletion >= _berserkerTimerX && _killCountX >= timeChallenge.Requirement)
        {
            return true;
        }
        return false;
    }

    // Tested and works
    private bool ChampionslayerCheck(TimeChallenge timeChallenge)
    {
        if (timeChallenge.ChallengeName == "Championslayer" && timeChallenge.TimeForCompletion >= _gameStartTimerX && _championIsDeadX)
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
            _killstreakKillCountX = 0;
            Debug.Log("_killStreakKillCount has been reset");
        }

        if (challenge.ChallengeName == "Killstreak" && !_player.HasTakenDamage && _killstreakKillCountX >= challenge.Requirement)
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
        if (challenge.ChallengeName == "Knock 'em Up" && _knockedUpCountX >= challenge.Requirement)
        {
            return true;
        }
        return false;
    }

    //Tested works
    private bool ThisIsSpartaCheck(Challenge challenge)
    {
        if (challenge.ChallengeName == "This is Sparta!" && _knockedOutOfArenaX >= challenge.Requirement)
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
        _gameStartTimerX += Time.deltaTime;
        _berserkerTimerX += Time.deltaTime;
    }





    #endregion


    public void Reset()
    {
        _gameStartTimerX = 0;
         _berserkerTimerX = 0;
         _challengeTimerX = 0;
         _isTimerActive = false;

         _killCountX = 0;
         _knockedUpCountX = 0;
         _knockedOutOfArenaX = 0;
         _killstreakKillCountX = 0;
         _totalDeathsX = 0;

         _totalKillcountX = 0;
         _totalKnockUpsX = 0;
         _totalKnockedOutOfArenaX = 0;
         _highestKillStreakKillCountX = 0;

         _challengeTimerMinionX = 0;
         _challengeTimerChampionX = 0;
         _championIsDeadX =false;
         _challengeRequirementsMetX = false;

         beenOutOfCombatX= false;



         ChampionsKilled = 0;
         TotalMoneyEarned = 0;
         GameManagerKillCount = 0;

         PlayerCoins = 50;

        TransferableScript.Instance.ResetInventorySlots();

    }
}
