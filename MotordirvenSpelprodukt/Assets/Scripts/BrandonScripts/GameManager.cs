using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


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

    [SerializeField] Object _champion;
    [SerializeField] SwitchCamera CamManager;
    private bool _kingCam;
    [SerializeField] Animator _kingAnim;
    [SerializeField] EntertainmentManager _etp;

    public static int PlayerCoins; //Static så att anadra scener kan få access



    public static GameManager Instance { get => _instance; set => _instance = value; }
    public int KillCount { get => _killCount; set => _killCount = value; }




    void Start()
    {
        _challengeManager = ChallengeManager.Instance; // Singleton
        _gameStartTimer = 0;

        _champion = GameObject.FindObjectOfType<CMPScript>();
        CamManager = GameObject.FindWithTag("CamManager").GetComponent<SwitchCamera>();
        _kingAnim = GameObject.FindWithTag("King").GetComponent<Animator>();
        _etp = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
        Debug.Log(_champion.name);

        _kingAnim.SetBool("Approved", false);

        //För testing
        PlayerCoins = 89;
        Debug.Log("Coins" + PlayerCoins);

        //Not used at the moment
        _challengeManager.OnChallengeCompleted += HandleChallengeCompleted;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_champion == null && !_kingCam)
        {
            _etp.MatchFinished = true;
            _kingCam = true;
            CamManager.GoToKingCam();
            Debug.Log("Champion Is dead");
            _kingAnim.SetBool("Approved", true);
            _kingAnim.SetFloat("ETP", (_etp.GetETP() / 100)); //Selects what animation to play based on ETP
        }

        //If player dies ... Simon jobbar med att flytta Healthmanager och i Damage


        // Testing challenges
        CheckChallengesCompletion();
        ChallengeTimersUpdate();
    }


    #region ChallengeMethods
    private void HandleChallengeCompleted(Challenge completedChallenge)
    {
        PlayerCoins += completedChallenge.Reward;
        _challengeManager.DeActivateChallenge(completedChallenge);
        _challengeManager.RemoveChallenge(completedChallenge);
        Debug.Log("Challenge completed " + completedChallenge.ChallengeName);
        Debug.Log("PlayerCoins = " + PlayerCoins);
    }

    public void CheckChallengesCompletion()
    {
        foreach (Challenge challenge in _challengeManager.ActiveChallenges)
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
