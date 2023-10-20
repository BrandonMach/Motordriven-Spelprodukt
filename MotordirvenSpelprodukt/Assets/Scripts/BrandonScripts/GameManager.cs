using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Object _champion;
    [SerializeField] SwitchCamera CamManager;
    private bool _kingCam;
    [SerializeField] Animator _kingAnim;
    [SerializeField] EntertainmentManager _etp; 

    int _playerCoins;

    // Variables for Challenges
    ChallengeManager _challengeManager;

    int _killCount;
    float _challengeTimerMinion;
    float _challengeTimerChampion;
    bool _isChampionDead;
    bool _isChallengeRequirementsMet;

    void Start()
    {
        
        _champion = GameObject.FindObjectOfType<CMPScript>();
        CamManager = GameObject.FindWithTag("CamManager").GetComponent<SwitchCamera>();
        _kingAnim = GameObject.FindWithTag("King").GetComponent<Animator>();
        _etp = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
        Debug.Log(_champion.name);

        _kingAnim.SetBool("Approved", false);

        _challengeManager.OnChallengeCompleted += HandleChallengeCompleted;
    }

    // Update is called once per frame
    void Update()
    {

        

        if(_champion == null && !_kingCam)
        {
            _kingCam = true;
            CamManager.GoToKingCam();
            Debug.Log("Champion Is dead");
            _kingAnim.SetBool("Approved", true);
            _kingAnim.SetFloat("ETP",(_etp.GetETP()/100));
        }
    }

    private void HandleChallengeCompleted(Challenge completedChallenge)
    {
        _playerCoins += completedChallenge.Reward;
    }
}
