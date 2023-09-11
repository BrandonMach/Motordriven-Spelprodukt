using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreInputScript : MonoBehaviour
{

    //For testing

    [SerializeField] private bool _isSpamming;


    [SerializeField] KeyCode[] _attackInputs;
    public List<KeyCode> _lastUsedInputs = new List<KeyCode>();
    public int _spamThreshold; //Should be private in final build


    void Start()
    {
        
    }

    
    void Update()
    {
        for (int i = 0; i < _attackInputs.Length; i++)
        {
            if (Input.GetKeyDown(_attackInputs[i])) 
            {
                if(_lastUsedInputs.Count == _spamThreshold) //remove first inputed key
                {
                    _lastUsedInputs.RemoveAt(0);
                }          
                _lastUsedInputs.Add(_attackInputs[i]); //add latest key
                
            }        
        }

        CheckSpamInput(_lastUsedInputs);

        
           
    }

    void CheckSpamInput(List<KeyCode> inputList)
    {
        var occurrences = inputList.GroupBy(x => x).ToDictionary(y => y.Key, z => z.Count()); //Groupes by all the same inputs and counts the occurrences

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
