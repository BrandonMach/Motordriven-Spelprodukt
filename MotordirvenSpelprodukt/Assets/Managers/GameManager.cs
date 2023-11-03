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

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }
        Instance = this;

    }

    /// <summary>
    /// Set your _gameManager variable to this instance in order to achieve the Singleton pattern.
    /// Example: _gameManager = GameManager.Instance
    /// </summary>
    public static GameManager Instance { get => _instance; set => _instance = value; }

    #endregion

    #region ChallengeVariables

    private ChallengeManager _challengeManager;

    float _gameStartTimer;
    float _challengeTimer;
    bool _isTimerActive;

    int _killCount;
    float _challengeTimerMinion;
    float _challengeTimerChampion;
    bool _isChampionDead;
    bool _isChallengeRequirementsMet;

    #endregion

    [SerializeField] UnityEngine.Object _champion;
    [SerializeField] SwitchCamera CamManager;
    private bool _kingCam;

    [SerializeField] EntertainmentManager _etp;
    [SerializeField] GameObject _player;

    public static int PlayerCoins; //Static så att anadra scener kan få access
    
    public int KillCount { get => _killCount; set => _killCount = value; }



    //Event the King subscribes to
    public event EventHandler OnChampionKilled;




    void Start()
    {
        _challengeManager = ChallengeManager.Instance;
        _gameStartTimer = 0;

        _champion = GameObject.FindObjectOfType<CMPScript>();
        _player = GameObject.FindGameObjectWithTag("Player");
        CamManager = GameObject.FindWithTag("CamManager").GetComponent<SwitchCamera>();
        _etp = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
        Debug.Log(_champion.name);


        //För testing
        PlayerCoins = 50;
        Debug.Log("Coins" + PlayerCoins);

        //Not used at the moment
        //_challengeManager.OnChallengeCompleted += HandleChallengeCompleted;
        
        
    }

    void Update()
    {
        if(_champion == null && !_kingCam)
        {
            _etp.MatchFinished = true;
            _kingCam = true;
            CamManager.GoToKingCam();
            OnChampionKilled?.Invoke(this, EventArgs.Empty); // Calls event 
        }

        //If player dies ... Simon jobbar med att flytta Healthmanager och i Damage
        if(_player == null)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        

        // Testing challenges
        CheckChallengesCompletion();
        ChallengeTimersUpdate();
    }


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
                if (timeChallenge.TimeForCompletion >= _gameStartTimer && _killCount >= timeChallenge.Requirement)
                {
                    HandleChallengeCompleted(timeChallenge);
                }
            }
        }
    }

    public void ChallengeTimersUpdate()
    {
        _gameStartTimer += Time.deltaTime;
    }

    #endregion



}
