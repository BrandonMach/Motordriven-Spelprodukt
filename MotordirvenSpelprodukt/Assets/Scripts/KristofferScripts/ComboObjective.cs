using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial Objective/ComboObjective")]
public class ComboObjective : TutorialObjective
{
    
    [SerializeField] private List<string> _attackList;
    private Queue attackQueue;

    public override void Setup()
    {
        attackQueue = new Queue();
        SetQueue();
        //_currentAttack = (string)attackQueue.Dequeue();
    }
    

    private void SetQueue()
    {
        attackQueue.Clear();
        for(int i=0;i<_attackList.Count;i++)
        {
            attackQueue.Enqueue(_attackList[i]);
        }
        _attack = (string)attackQueue.Dequeue();
    }
    public override void CheckQueue(string attackvalue)
    {
        Debug.Log(_attack + ":"+ attackvalue);
        if (_attack == attackvalue)
        {
            if(attackQueue.Count > 0)
            {
                _attack = (string)attackQueue.Dequeue();
            }
            else
            {
                IncrementObjective();
            }
        }
        else if(_attack != attackvalue)
        {
            Debug.Log("fail");
            SetQueue();
        }
    }
    public override void IncrementObjective()
    {
        _currentTarget++;
        if(_currentTarget< _numOfTarget)
        {
            SetQueue();
        }
    }
    public override string GetObjective()
    {
        return _attack;
    }
    public override void GetNumber(ref int cur, ref int max)
    {
        cur = _currentTarget;
        max = _numOfTarget;
    }
    public override void ResetTarget()
    {
        _currentTarget = 0;
        Setup();
    }
    public override bool CheckCompleted()
    {
        return _currentTarget >= _numOfTarget;
    }
}
