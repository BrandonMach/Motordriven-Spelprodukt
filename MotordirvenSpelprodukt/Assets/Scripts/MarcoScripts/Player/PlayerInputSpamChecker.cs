using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerInputSpamChecker : MonoBehaviour
{
    [SerializeField] private int _maxSize;
    [SerializeField] private int _inputSpamAmount; // How many times an input has to be entered to be considered spam
    [SerializeField] private int _amountToCheckForSpam; // How many inputs back we will check each time
    [SerializeField] private int _enertainmentDecrease;

    private EntertainmentManager _entertainmentManager;

    private List<string> _inputHistory = new List<string>();


    private void Start()
    {
        _entertainmentManager = EntertainmentManager.Instance;
    }
    public void AddInputSequence(string inputSequence)
    {
        _inputHistory.Add(inputSequence);
        CheckRepeatedCombos();
    }

    private void CheckRepeatedCombos()
    {
        if (_inputHistory.Count > _inputSpamAmount)
        {
            string lastCombo = _inputHistory.Last();
            int repeatCount = _inputHistory.TakeLast(_amountToCheckForSpam).Count(combo => combo == lastCombo); // Checks the last 5 combos and counts how many matches the last combo sequence    

            if (repeatCount >= _inputSpamAmount)
            {
                Debug.Log("Repeated combo detected: " + lastCombo);
                _entertainmentManager?.ChangeEtp(_enertainmentDecrease);
         
            }
        }

        if (_inputHistory.Count > _maxSize)
        {
            _inputHistory.RemoveAt(0); // Remove the oldest combo
        }
    }
}
