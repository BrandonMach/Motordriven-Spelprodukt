using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private List<TutorialObjective> _objList;
    private Stack _objStack;
    private TutorialObjective _currentObjective;
    [SerializeField] private GameObject _arrow;
    [Header("Canvas Items")]
    [SerializeField] Canvas _canvas;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI descTextField;

    private void Start()
    {
        _objStack = new Stack();
        foreach (TutorialObjective obj in _objList)
        {
            obj.ResetTarget();
            _objStack.Push(obj);
        }
        _currentObjective = (TutorialObjective)_objStack.Pop();
        GameInput.Instance.OnLightAttackButtonPressed += LightAttackChecker;
        GameInput.Instance.OnHeavyAttackButtonPressed += HeavyAttackChecker;
        UpdateUI();
    }
    private void LightAttackChecker(object sender, EventArgs e)
    {
        string light = "Light";
        if (_currentObjective as ComboObjective)
        {
            _currentObjective.CheckQueue(light);
            UpdateUI();
            CheckCompleted();
        }
        else if (_currentObjective.GetObjective()== light && _currentObjective as EasyObjective)
        {
            _currentObjective.IncrementObjective();               
            CheckCompleted();
        }
    }
    private void HeavyAttackChecker(object sender, EventArgs e)
    {
        string heavy = "Heavy";
        if (_currentObjective as ComboObjective)
        {
            _currentObjective.CheckQueue(heavy);
            UpdateUI();
            CheckCompleted();
        }
        else if (_currentObjective.GetObjective() == heavy && _currentObjective as EasyObjective)
        {
            _currentObjective.IncrementObjective();                         
            CheckCompleted();
        }
    }
    private void CheckCompleted()
    {
        UpdateUI();
        if (_currentObjective.CheckCompleted())
        {           
            if(_objStack.Count>0)
            {
                _currentObjective = (TutorialObjective)_objStack.Pop();
                if(_currentObjective as ComboObjective)
                { _currentObjective.Setup(); }
                UpdateUI();
            }
            else
            {              
                SetCompleted();
            }
        }     
        
    }
    private void UpdateUI()
    {    
        int current=0, max=0;
        _currentObjective.GetNumber(ref current, ref max);
        attackText.text = _currentObjective.GetObjective() + ": " + current + "/" + max;
        descTextField.text = _currentObjective.GetDescription();
    }
    private void SetCompleted()
    {
        GameInput.Instance.OnLightAttackButtonPressed -= LightAttackChecker;
        GameInput.Instance.OnHeavyAttackButtonPressed -= HeavyAttackChecker;
        attackText.text = "Tutorial Completed";
        descTextField.text = "There are more Combos in the game. Experiment to find them for yourself."+"\n"+ "You may step on the arrow to leave";
        _arrow.SetActive(true);
    }
}
