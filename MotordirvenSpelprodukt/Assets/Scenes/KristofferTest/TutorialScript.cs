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
        if (_currentObjective.GetObjective()=="Light")
        {
            _currentObjective.IncrementObjective();
            CheckCompleted();           
        }
    }
    private void HeavyAttackChecker(object sender, EventArgs e)
    {
        if (_currentObjective.GetObjective() == "Heavy")
        {
            _currentObjective.IncrementObjective();
            CheckCompleted();
        }
    }
    private void CheckCompleted()
    {
        Debug.Log(_objStack.Count);
        UpdateUI();
        if (_currentObjective.CheckCompleted())
        {           
            if(_objStack.Count>0)
            {
                _currentObjective = (TutorialObjective)_objStack.Pop();
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
        Debug.Log("Here");
        attackText.text = "Tutorial Completed";
        descTextField.text = "You may step on the arrow to leave";
        _arrow.SetActive(true);
    }
}
