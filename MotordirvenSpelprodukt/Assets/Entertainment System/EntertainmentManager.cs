using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;
using UnityEngine.UI;

public class EntertainmentManager : MonoBehaviour
{
    private static EntertainmentManager _instance;
    public static EntertainmentManager Instance { get => _instance; set => _instance = value; }

    //For Testing
    public TextMeshProUGUI EntertainmentText;
    public TextMeshProUGUI CrowdText;
    


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
        return (_maxETP * 0.25f);
    }
    public float GetExcitedThreshold()
    {
        return (_maxETP * 0.75f);
    }

    //ETP events
    public event System.EventHandler OnETPExited;
    public event System.EventHandler OnETPAngry;
    public event System.EventHandler OnETPNormal;

  

    #endregion

    #region OUT OF COMBAT Variables

    [Header("OOC- Out Of Combat")]

    public Image OOCPopUp;
    //public GameObject[] EnemyGameObjects;
    //public GameObject PlayerCharacter;
    [SerializeField] [Range(0, 10)] float _scanEnemyArea;
    [SerializeField] float _timeOutOfCombatCounter = 0;
    [SerializeField] float _timeOutOfCombatThreshold;

    [Header("Conditions")]

    [SerializeField] private bool _isOutOfCombat; //OOC
    public bool PlayerNearEnemies;


    public event System.EventHandler OutOfCombat;
    public event System.EventHandler InCombat;

    public bool CanGoOTC;
    // public bool WavesAreSpawning;

    #endregion

    // [SerializeField] private bool _startComboWindowTimer;



    public Slider _sliderDist;
    private float _etpChangevalue;
    bool increaseEtp;
    [SerializeField] float _arrowMoveSpeed;

    [SerializeField] RectTransform _etpBarObject;
    [SerializeField] Vector3 _overTheShoulderCamPos;

    private Vector3 _originalETPTextPos;
    private Vector3 _originalETPBarPos;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("More than one instance of ChallengeManager found");
            return;
        }
        _instance = this;

       // DontDestroyOnLoad(gameObject);     
    }

    void Start()
    {    
        //ETP
        _startETP = _maxETP / 2;
        _ETPThreshold = _maxETP / 2;
        _entertainmentPoints = _startETP;

        Player.Instance.GetComponent<AttackManager>().EnemyHit += EnemyHitPlayerInCombat;
        // SpawnEnemy.Instance.SpawningDone += ResetFirstTimeInCombat;
        _originalETPBarPos = _etpBarObject.anchoredPosition;
        _originalETPTextPos = EntertainmentText.gameObject.GetComponentInParent<RectTransform>().localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.OverTheSholderCamActive)
        {
            _etpBarObject.anchoredPosition = new Vector3(_etpBarObject.localPosition.x,  93 ,_etpBarObject.localPosition.z);
            EntertainmentText.gameObject.GetComponentInParent<RectTransform>().localPosition = new Vector3(248, 410, -464);
        }
        else
        {
            _etpBarObject.anchoredPosition = _originalETPBarPos;
            EntertainmentText.gameObject.GetComponentInParent<RectTransform>().localPosition = _originalETPTextPos;
        }

        _entertainmentPoints = Mathf.Clamp(_entertainmentPoints, 0, _maxETP);

        CheckETPChanges();

        UpdateETPArrow();

        //For testing
        EntertainmentText.text = "ETP: " + Mathf.Round(_entertainmentPoints).ToString();

       

        if (!GameLoopManager.Instance.MatchIsFinished && CanGoOTC)
        {

            //Debug.Log("sdkasd");


            CheckIfOutOfCombat();
            if (_isOutOfCombat)
            {
                //OTC pop up
                OOCPopUp.color = new Color(OOCPopUp.color.r, OOCPopUp.color.g, OOCPopUp.color.b, 252);
                OutOfCombatDecreaseOverTime();
            }
            else
            {
                //OTC pop up
                OOCPopUp.color = new Color(OOCPopUp.color.r, OOCPopUp.color.g, OOCPopUp.color.b, 0);
            }
        } 
    }


    private void ResetFirstTimeInCombat(object sender, System.EventArgs e)
    {
        //_timeOutOfCombatCounter = 0;
       


    }

    #region ETP Change Events
    void CheckETPChanges()
    {
        if(GetETP() > GetExcitedThreshold())
        {
            OnETPExited?.Invoke(this, EventArgs.Empty);
          //  FMODSFXController.Instance.PlayCrowdCheer();    // VArFoR iNtE fOnKa??


        }
        else if (GetETP() < GetAngryThreshold())
        {
            OnETPAngry?.Invoke(this, EventArgs.Empty);
           // FMODSFXController.Instance.PlayCrowdBoo();      // VArFoR iNtE fOnKa??
        }
        else
        {
            OnETPNormal?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion



    void UpdateETPArrow()
    {
        if(_entertainmentPoints != _sliderDist.value)
        {
            if (increaseEtp &&  _sliderDist.value <= _entertainmentPoints)
            {
                _sliderDist.value += _arrowMoveSpeed * Time.deltaTime;
            }
            else if ( !increaseEtp && _sliderDist.value >= _entertainmentPoints)
            {
                _sliderDist.value -= _arrowMoveSpeed * Time.deltaTime;
            }
        }
      
    }

    #region OOC
    void CheckIfOutOfCombat()
    {
        //Scan for enemies
        if(GameLoopManager.Instance.EnemyGameObjects.Length > 0)
        {
            foreach (GameObject enemies in GameLoopManager.Instance.EnemyGameObjects)
            {
                float dist = Vector3.Distance(enemies.transform.position, Player.Instance.transform.position);
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
            }
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
        increaseEtp = false;
        _entertainmentPoints -= (Time.deltaTime); //Every second ETP -1
      


        //ChangeEtp(-1);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Player.Instance.transform.position, _scanEnemyArea);
    }

    private void OnOutOfCombat()
    {
        OutOfCombat?.Invoke(this, EventArgs.Empty);
        GameLoopManager.Instance.BeenOutOfCombat = true;
    }

    private void OnInCombat()
    {
        InCombat.Invoke(this, EventArgs.Empty);
    }
 

    #endregion
    //public void DecreseETP(float amoutToDecrese)
    //{
    //    //_entertainmentPoints -= amoutToDecrese;
    //    StopAllCoroutines();
        
    //    StartCoroutine(corutine);
    //}
    //public void increaseETP(int amoutToIncrease)
    //{
    //     // _entertainmentPoints += amoutToIncrease;
    //    StopAllCoroutines();
    //    //StopCoroutine(corutine);
     
    //    StartCoroutine(corutine);
    //}

    public void ChangeEtp(float amoutToChange)
    {
        // StopCoroutine(corutine);
        if (_entertainmentPoints + amoutToChange > _sliderDist.value)
        {
            increaseEtp = true;
        }
        else
        {
            increaseEtp = false;
        }

        _entertainmentPoints += amoutToChange;
     
        //corutine = ChangeEtpCorutine(amoutToChange);
        //StartCoroutine(corutine);
    }

  

}
