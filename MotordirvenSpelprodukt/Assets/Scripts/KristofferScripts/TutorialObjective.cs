using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Tutorial Objective")]
public class TutorialObjective : ScriptableObject
{
    [SerializeField] private int _numOfTarget;
    private int _currentTarget = 0;
    [SerializeField] private string _attack;
    [SerializeField][TextArea(15,20)] 
    private string _descText;

    public void ResetTarget()
    {
        _currentTarget = 0;
    }
    public void IncrementObjective()
    {
        _currentTarget++;
    }
    public bool CheckCompleted()
    {
        return _currentTarget >= _numOfTarget;
    }
    public string GetObjective()
    {
        return _attack;
    }
    public string GetDescription()
    {
        return _descText;
    }
    public void GetNumber(ref int cur, ref int max)
    {
        cur = _currentTarget;
        max = _numOfTarget;
    }     
}
