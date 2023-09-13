using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ComboTestScript : MonoBehaviour
{
    

    [SerializeField] KeyCode[] _attackInputs; //Static attack inputs

    [SerializeField] TextMeshProUGUI _comboTreeInfoText;

    [Header("Combo Sequence")]
    public KeyCode[] _inputSequence;

    [SerializeField] private float _comboWindowTimer = 0;

    [SerializeField] private bool _startComboWindowTimer;


    private List<KeyCode[]> cbList = new List<KeyCode[]>();

    EntertainmentManager _etpManager;
    public List<KeyCode> _lastUsedInputs = new List<KeyCode>();

    string ComboTree;

    void Start()
    {

        _etpManager = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();

        cbList.Add(new KeyCode[] {_attackInputs[0], _attackInputs[0], _attackInputs[3]}); //Combo: V,V,M
        cbList.Add(new KeyCode[] {_attackInputs[0], _attackInputs[1], _attackInputs[2]}); //Combo: V,B,N
        cbList.Add(new KeyCode[] {_attackInputs[3], _attackInputs[0], _attackInputs[3]}); //Combo: M,V,M



        for (int i = 0; i < cbList.Count; i++)
        {
            for (int j = 0; j < cbList[i].Length; j++)
            {
                ComboTree += ", "+  cbList[i].GetValue(j).ToString();
            }

            
            _comboTreeInfoText.text += "Combo: "+(ComboTree.Remove(0, 1)) +"\n";
            ComboTree = "";
        }

    }

    // Update is called once per frame
    void Update()
    {   

        for (int i = 0; i < _attackInputs.Length; i++)
        {
            if (Input.GetKeyDown(_attackInputs[i])) //Checks if attack input has been pressed
            {
                _startComboWindowTimer = true;
                _comboWindowTimer = 0;


                if (_lastUsedInputs.Count == 3) //remove first inputed key, Only 3 input lenght Combos
                {
                    _lastUsedInputs.RemoveAt(0);
                }
                _lastUsedInputs.Add(_attackInputs[i]); //add latest key


                KeyCode[] comboAttempt = _lastUsedInputs.ToArray();

                foreach (var Combos in cbList)
                {
                    if (Enumerable.SequenceEqual(Combos, comboAttempt))
                    {
                        Debug.LogError("Combo matched");
                        _etpManager.increaseETP(15);
                    }
                    
                }
            }
        }

        if (_startComboWindowTimer)
        {
            StartComboWindowCheck();
        }
    }


    void StartComboWindowCheck()
    {
        float comboWindow = 1;
        _comboWindowTimer += Time.deltaTime;

        if (_comboWindowTimer >= comboWindow)
        {
            _comboWindowTimer = 0;
            _lastUsedInputs.Clear();
            //Debug.LogError("Combo Broken");
        }
    }
}
