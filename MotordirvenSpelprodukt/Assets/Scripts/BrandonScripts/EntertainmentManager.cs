using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntertainmentManager : MonoBehaviour
{
    //For Testing
    public TextMeshProUGUI EntertainmentText; 


    [SerializeField] float _entertainmentPoints;


    [Header("UI Arrow")]
    [SerializeField] private Transform _indicatorArrow;
    private float _indicatorArrowrRotateAngle;


    //ETP = Entartainment Points

    [SerializeField] private float _maxETP = 100;
    private float _startETP;
    private float _currentThreshold;







    [Header("Conditions")]

    [SerializeField] private bool _outOfCombat;


    void Start()
    {

        _startETP = _maxETP / 2;

        //EntertainmentThreshold = _currentThreshold;
        _entertainmentPoints = _startETP;

        //Start from midde for now
        _indicatorArrowrRotateAngle = 90;
        _indicatorArrow.eulerAngles = new Vector3(0, 0, _indicatorArrowrRotateAngle);
        //EntertainmentText.text = Mathf.Round(_entertainmentPoints).ToString();
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
            _outOfCombat = !_outOfCombat;
        }

        if (_outOfCombat)
        {
            OutOfCombatDecreaseOverTime();
        }

        _indicatorArrowrRotateAngle = (180 / _maxETP) * _entertainmentPoints;

        _entertainmentPoints =  Mathf.Clamp(_entertainmentPoints, 0, _maxETP);
        EntertainmentText.text = "ETP: " + Mathf.Round(_entertainmentPoints).ToString();

    }

    void OutOfCombatDecreaseOverTime()
    {
        _indicatorArrowrRotateAngle = Mathf.Clamp(_indicatorArrowrRotateAngle, 0, 180);

        float pointsDecrease = 180 / _maxETP;

        //if(_indicatorArrowrRotateAngle > 0)
        //{
        //    _indicatorArrowrRotateAngle -= (Time.deltaTime * pointsDecrease);
        //    Debug.Log(_indicatorArrowrRotateAngle);

        //    _indicatorArrow.eulerAngles = new Vector3(0, 0, _indicatorArrowrRotateAngle);
        //}

        _indicatorArrow.eulerAngles = new Vector3(0, 0, _indicatorArrowrRotateAngle);

        _entertainmentPoints -= (Time.deltaTime); //Every second ETP -1

    }
}
