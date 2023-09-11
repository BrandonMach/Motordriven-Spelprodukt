using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ComboTestScript : MonoBehaviour
{
    

    [SerializeField] KeyCode[] _attackInputs; //Static attack inputs



    [Header("Combo Sequence")]
    public KeyCode[] _inputSequence;

    [SerializeField] private float _comboWindowTimer = 0;

    [SerializeField] private bool _startComboWindowTimer;

    private ArrayList _comboList = new ArrayList();

    private List<KeyCode[]> cbList = new List<KeyCode[]>();

    EntertainmentManager _etpManager;
    public List<KeyCode> _lastUsedInputs = new List<KeyCode>();

    void Start()
    {

        _etpManager = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
        //_comboList.Add((_attackInputs[0], _attackInputs[0], _attackInputs[1])); //Combo: V,V,B
        //_comboList.Add((_attackInputs[0], _attackInputs[1], _attackInputs[2])); //Combo: V,B,N
        //_comboList.Add((_attackInputs[3], _attackInputs[1], _attackInputs[3])); //Combo: M,V,M


        cbList.Add(new KeyCode[] {_attackInputs[0], _attackInputs[0], _attackInputs[3]});
        cbList.Add(new KeyCode[] {_attackInputs[0], _attackInputs[1], _attackInputs[2]});
        cbList.Add(new KeyCode[] {_attackInputs[3], _attackInputs[0], _attackInputs[3]});


        //foreach (var combos in _comboList)
        //{
        //    Debug.Log(combos);
        //}
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


                if (_lastUsedInputs.Count == 3) //remove first inputed key
                {
                    _lastUsedInputs.RemoveAt(0);
                }
                _lastUsedInputs.Add(_attackInputs[i]); //add latest key


                KeyCode[] sdsda = _lastUsedInputs.ToArray();

                Debug.Log(sdsda);
                foreach (var Combos in cbList)
                {
                    if (Enumerable.SequenceEqual(Combos, sdsda))
                    {
                        Debug.LogError("Combo matched");
                        _etpManager.increaseETP(15);
                    }
                    Debug.Log(Combos.Equals(sdsda));
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
