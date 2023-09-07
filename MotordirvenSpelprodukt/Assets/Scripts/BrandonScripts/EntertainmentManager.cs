using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntertainmentManager : MonoBehaviour
{
    //For Testing
    public TextMeshProUGUI EntertainmentText;
    public TextMeshProUGUI CrowdText;


    [SerializeField] float _entertainmentPoints;


    [Header("UI Arrow")]
    [SerializeField] private Transform _indicatorArrow;
    private float _indicatorArrowrRotateAngle;


    //ETP = Entartainment Points

    [SerializeField] private float _maxETP = 100;
    private float _ETPThreashold;
    private float _startETP;
    private float _currentThreshold;


    public GameObject[] EnemyGameObjects;
    public GameObject PlayerCharacter;
    [SerializeField] [Range(0, 10)] float _scanEnemyArea;
    [SerializeField] float _timeOutOfCombatCounter = 0;
    [SerializeField] float _timeOutOfCombatThreshold = 10;




    [Header("Conditions")]

    [SerializeField] private bool _isOutOfCombat; //OOC


    void Start()
    {

        EnemyGameObjects = GameObject.FindGameObjectsWithTag("EnemyTesting");



        //ETP
        _startETP = _maxETP / 2;
        _ETPThreashold = _maxETP / 2;
        _entertainmentPoints = _startETP;
        _indicatorArrowrRotateAngle = 90;
        _indicatorArrow.eulerAngles = new Vector3(0, 0, _indicatorArrowrRotateAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _entertainmentPoints += 25;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            _isOutOfCombat = !_isOutOfCombat;
        }

        if (_isOutOfCombat)
        {
            OutOfCombatDecreaseOverTime();
        }

        if(_entertainmentPoints < _ETPThreashold)
        {
            CrowdText.text = "Booooooo!!";
        }
        else
        {
            CrowdText.text = "Let's GOOOOO!!";
            CrowdText.color = Color.black;
        }

        _indicatorArrowrRotateAngle = (180 / _maxETP) * _entertainmentPoints;

        _entertainmentPoints =  Mathf.Clamp(_entertainmentPoints, 0, _maxETP);
        EntertainmentText.text = "ETP: " + Mathf.Round(_entertainmentPoints).ToString();



        //Scan for enemies

        foreach (GameObject enemies in EnemyGameObjects)
        {
            float dist = Vector3.Distance(enemies.transform.position, PlayerCharacter.transform.position);
            if(dist > _scanEnemyArea)
            {

                _timeOutOfCombatCounter += Time.deltaTime;

                if (_timeOutOfCombatCounter >= _timeOutOfCombatThreshold)
                {
                    Debug.Log("Out of Combat");
                    _isOutOfCombat = true;

                }

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Q)) //""
                {
                    _isOutOfCombat = false;
                    //_timeOutOfCombatCounter = 0; //Lägg till att man måste attackera en enemy innan man sätter den till 0 i real game
                }
                //_isOutOfCombat = false;
                _timeOutOfCombatCounter = 0; //Lägg till att man måste attackera en enemy innan man sätter den till 0 i real game
            }
        }

       




    }

    void OutOfCombatDecreaseOverTime()
    {
        _indicatorArrowrRotateAngle = Mathf.Clamp(_indicatorArrowrRotateAngle, 0, 180);

        _indicatorArrow.eulerAngles = new Vector3(0, 0, _indicatorArrowrRotateAngle);

        _entertainmentPoints -= (Time.deltaTime); //Every second ETP -1

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PlayerCharacter.transform.position, _scanEnemyArea);
        
    }
}
