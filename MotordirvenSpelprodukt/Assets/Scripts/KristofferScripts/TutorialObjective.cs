using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjective : ScriptableObject
{

    [SerializeField] private int _numOfTarget;
    private int _currentTarget = 0;
    [SerializeField] private string _attack;
    [SerializeField][TextArea(15,20)] 
    private string _descText;
    

    public virtual void IncrementObjective()
    {
        _currentTarget++;
    }
    public virtual void GetNumber(ref int cur, ref int max)
    {
        cur = _currentTarget;
        max = _numOfTarget;
    }
    public virtual bool CheckCompleted()
    {
        return _currentTarget >= _numOfTarget;
    }
    public virtual string GetObjective()
    {
        return _attack;
    }
    public virtual void ResetTarget()
    {
        _currentTarget = 0;
    }
    public string GetDescription()
    {
        return _descText;
    }
    public virtual void CheckQueue(string attackvalue)
    { }
    public virtual void Setup()
    {}
}

