using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class EntertainmentManager : MonoBehaviour
{
    private static EntertainmentManager _instance;
    public static EntertainmentManager Instance { get => _instance; set => _instance = value; }

    //For Testing
    public TextMeshProUGUI EntertainmentText;
    public TextMeshProUGUI CrowdText;
    public GameObject OOCPopUp;


    [Header("UI Arrow")]
    [SerializeField] private RectTransform _indicatorArrow;

    #region Entertainment Point
    
    [SerializeField] float _entertainmentPoints;
    public float GetETP()
    {
        return _entertainmentPoints;
    }

    [SerializeField] private float _maxETP = 100;
    private float _startETP;
    public float GetMaxETP()
    {
        return _maxETP;
    }

    private float _ETPThreshold;
    public float GetETPThreshold()
    {
        return _ETPThreshold;
    }

    public float GetAngryThreshold()
    {
        return (_maxETP * 0.33f);
    }
    public float GetExcitedThreshold()
    {
        return (_maxETP * 0.66f);
    }

    //ETP events
    public event System.EventHandler OnETPExited;
    public event System.EventHandler OnETPAngry;
    public event System.EventHandler OnETPNormal;

    #endregion

    #region OUT OF COMBAT Variables

    [Header("OOC- Out Of Combat")]

    //public GameObject[] EnemyGameObjects;
    public GameObject PlayerCharacter;
    [SerializeField] [Range(0, 10)] float _scanEnemyArea;
    [SerializeField] float _timeOutOfCombatCounter = 0;
    [SerializeField] float _timeOutOfCombatThreshold;

    [Header("Conditions")]

    [SerializeField] private bool _isOutOfCombat; //OOC
    public bool PlayerNearEnemies;


    public event System.EventHandler OutOfCombat;
    public event System.EventHandler InCombat;

    #endregion

    // [SerializeField] private bool _startComboWindowTimer;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("More than one instance of ChallengeManager found");
            return;
        }
        _instance = this;

        //DontDestroyOnLoad(gameObject);     
    }

    void Start()
    {    
        //ETP
        _startETP = _maxETP / 2;
        _ETPThreshold = _maxETP / 2;
        _entertainmentPoints = _startETP;

        Player.Instance.GetComponent<AttackManager>().EnemyHit += EnemyHitPlayerInCombat;
    }

    // Update is called once per frame
    void Update()
    {   
        _entertainmentPoints = Mathf.Clamp(_entertainmentPoints, 0, _maxETP);

        CheckETPChanges();

        UpdateETPArrow();

        //For testing
        EntertainmentText.text = "ETP: " + Mathf.Round(_entertainmentPoints).ToString();
        //OTC pop up
        OOCPopUp.SetActive(_isOutOfCombat);

        if (!GameManager.Instance.MatchIsFinished)
        {
            //Debug.Log("sdkasd");
            CheckIfOutOfCombat();
            if (_isOutOfCombat)
            {
                OutOfCombatDecreaseOverTime();
            }
        } 
    }


    #region ETP Change Events
    void CheckETPChanges()
    {
        if(GetETP() > GetExcitedThreshold())
        {
            OnETPExited?.Invoke(this, EventArgs.Empty);
        }
        else if (GetETP() < GetAngryThreshold())
        {
            OnETPAngry?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnETPNormal?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion
    void UpdateETPArrow()
    {
        _indicatorArrow.localPosition = new Vector3(-160 +(320/_maxETP)*_entertainmentPoints, _indicatorArrow.localPosition.y, _indicatorArrow.localPosition.z);
    }

    #region OOC
    void CheckIfOutOfCombat()
    {
        //Scan for enemies
        foreach (GameObject enemies in  GameManager.Instance.EnemyGameObjects)
        {
            float dist = Vector3.Distance(enemies.transform.position, PlayerCharacter.transform.position);
            if (dist > _scanEnemyArea && !_isOutOfCombat)
            {
                PlayerNearEnemies = false; 
                _timeOutOfCombatCounter += Time.deltaTime;

                if (_timeOutOfCombatCounter >= _timeOutOfCombatThreshold)
                {
                    Debug.Log("Out of Combat");
                    _isOutOfCombat = true;
                    OnOutOfCombat();
                }
            }
            else
            {      
                _timeOutOfCombatCounter = 0;
            }

            //if (dist < _scanEnemyArea  /* attack hits enemy*/)
            //{
            //    PlayerNearEnemies = true;
            //}
        }
    }

    private void EnemyHitPlayerInCombat(object sender, System.EventArgs e) // Player hit Enemy => Player in combat
    {  
        _isOutOfCombat = false;
        OnInCombat();
        Debug.Log("Hit Enemy & out of combat");
    }

    void OutOfCombatDecreaseOverTime()
    {
        _entertainmentPoints -= (Time.deltaTime); //Every second ETP -1
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PlayerCharacter.transform.position, _scanEnemyArea);
    }

    private void OnOutOfCombat()
    {
        OutOfCombat?.Invoke(this, EventArgs.Empty);
    }

    private void OnInCombat()
    {
        InCombat.Invoke(this, EventArgs.Empty);
    }
 

    #endregion
    public void DecreseETP(float amoutToDecrese)
    {
        _entertainmentPoints -= amoutToDecrese;
    }
    public void increaseETP(int amoutToIncrease)
    {
        _entertainmentPoints += amoutToIncrease;     
    }

    //private void OnDestroy()
    //{
    //    Debug.Log("sdad");
    //}
}
