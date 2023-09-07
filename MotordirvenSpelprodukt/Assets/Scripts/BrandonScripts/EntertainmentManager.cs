using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntertainmentManager : MonoBehaviour
{
    //For Testing
    public TextMeshProUGUI EntertainmentText;
    public TextMeshProUGUI CrowdText;
    public GameObject OOCPopUp;


    [SerializeField] float _entertainmentPoints;


    [Header("UI Arrow")]
    [SerializeField] private Transform _indicatorArrow;
    private float _indicatorArrowrRotateAngle;


    //ETP = Entartainment Points

    [SerializeField] private float _maxETP = 100;
    private float _ETPThreashold;
    private float _startETP;
    private float _currentThreshold;

    [Header("OOC- Out Of Combat")]
    public GameObject[] EnemyGameObjects;
    public GameObject PlayerCharacter;
    [SerializeField] [Range(0, 10)] float _scanEnemyArea;
    [SerializeField] float _timeOutOfCombatCounter = 0;
    [SerializeField] float _timeOutOfCombatThreshold;

    [Header("Combo Sequence")]
    public KeyCode[] _inputSequence;
    int _indexOfINputSequence = 0;
    


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

        CheckIfOutOfCombat();


        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    _entertainmentPoints += 25;
        //}

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


        //Combo sequence

        if(_indexOfINputSequence < _inputSequence.Length)
        {
            if (Input.GetKeyDown(_inputSequence[_indexOfINputSequence]))  
            {
                Debug.Log(_inputSequence[_indexOfINputSequence]);
                _indexOfINputSequence++;
               
                //Timer, if no correct input i made start over



                if (_indexOfINputSequence == _inputSequence.Length)
                {
                    _indexOfINputSequence = 0;
                    Debug.Log("Combo sequence achived");
                    _entertainmentPoints += 25;
                }

            }
        }

        





        //For testing
        OOCPopUp.SetActive(_isOutOfCombat);
       




    }

    void CheckIfOutOfCombat()
    {
        //Scan for enemies

        foreach (GameObject enemies in EnemyGameObjects)
        {
            float dist = Vector3.Distance(enemies.transform.position, PlayerCharacter.transform.position);
            if (dist > _scanEnemyArea)
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
                if (Input.GetKeyDown(KeyCode.Q)) //"Switch _isOutOfCombat to false when attacking is detected, Placeholder for now"
                {
                    _isOutOfCombat = false;

                }
                _timeOutOfCombatCounter = 0;
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
