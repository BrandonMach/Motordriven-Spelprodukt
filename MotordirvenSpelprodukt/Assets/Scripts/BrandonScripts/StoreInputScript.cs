using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreInputScript : MonoBehaviour
{

    //For testing

    public bool IsSpamming;


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
        var occurrences = inputList.GroupBy(x => x).ToDictionary(y => y.Key, z => z.Count());

        foreach (var item in occurrences)
        {
            if(item.Value == _spamThreshold) //If spam threshold is all the same key player has spammed
            {
                Debug.Log("Spammed attack button: " + item.Key + item.Value + " times.");
                IsSpamming = true;
            }
        }
    }

   
}
