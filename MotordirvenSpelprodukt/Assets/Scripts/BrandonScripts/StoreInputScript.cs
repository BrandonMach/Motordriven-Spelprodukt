using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreInputScript : MonoBehaviour
{

    //For testing

    [SerializeField] private bool _isSpamming;
    ArrayList comboList = new ArrayList();




    [SerializeField] KeyCode[] _attackInputs;
    public List<KeyCode> _lastUsedInputs = new List<KeyCode>();
    public int _spamThreshold; //Should be private in final build

    [SerializeField] private EntertainmentManager _etpManager;

    void Start()
    {
        _etpManager = GameObject.Find("Canvas").GetComponent<EntertainmentManager>(); //Canvas of now, name may need to change

    }

    
    void Update()
    {
        for (int i = 0; i < _attackInputs.Length; i++)
        {
            if (Input.GetKeyDown(_attackInputs[i])) //Checks if attack input has been pressed
            {
                if (_lastUsedInputs.Count == _spamThreshold) //remove first inputed key
                {
                    _lastUsedInputs.RemoveAt(0);
                }          
                _lastUsedInputs.Add(_attackInputs[i]); //add latest key

                CheckSpamInput(_lastUsedInputs);

                if (_isSpamming)
                {
                    //Decrease ETP
                    _etpManager.DecreseETP(10);
                }

            }        
        }

        

        
           
    }

    void CheckSpamInput(List<KeyCode> inputList)
    {
        Dictionary<KeyCode,int> occurrences = inputList.GroupBy(x => x).ToDictionary(y => y.Key, z => z.Count()); //Groupes by all the same inputs and counts the occurrences

        foreach (var item in occurrences)
        {
            if(item.Value == _spamThreshold) //If the occurences of one input is same as Spamthreshold 
            {
                Debug.Log("Spammed attack button: " + item.Key + item.Value + " times.");
                _isSpamming = true;
            }
            else
            {
                _isSpamming = false;
            }
        }
    }  
}
