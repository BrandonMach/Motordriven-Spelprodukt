using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInputScript : MonoBehaviour
{

    [SerializeField] KeyCode[] _attackInputs;
    public List<KeyCode> _lastUsedInputs = new List<KeyCode>();
    string _keySpammed;
    [SerializeField] private int _spamThreshold;

    void Start()
    {
        
    }

    
    void Update()
    {
        for (int i = 0; i < _attackInputs.Length; i++)
        {
            if (Input.GetKeyDown(_attackInputs[i])) 
            {
                Debug.Log("Pressed");
                _lastUsedInputs.Add(_attackInputs[i]);

                if (CheckSpamInput(_lastUsedInputs, _lastUsedInputs.Count, _spamThreshold))
                {
                    Debug.LogError("Spamming Attack " + _keySpammed);
                }
            }
               
        }
        

       

        
        
           
    }

    bool CheckSpamInput(List<KeyCode> inputList, int listLenght,int spamRate)
    {
        for (int i = 0; i < listLenght; i++)
        {
            int j = i + 1;
            int searchRange = spamRate;

            while(searchRange > 0 && j < listLenght)
            {
                if(inputList[i] == inputList[j])
                {
                    _keySpammed = inputList[i].ToString();
                    return true;
                }

                j++;
                searchRange--;
            }

        }

        return false;


    }
}
