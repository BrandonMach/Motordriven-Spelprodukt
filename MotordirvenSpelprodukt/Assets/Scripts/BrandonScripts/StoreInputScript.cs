using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInputScript : MonoBehaviour
{

    [SerializeField] KeyCode[] _attackInputs;
    public List<KeyCode> _lastUsedInputs = new List<KeyCode>();
    string _keySpammed;

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
                
            }
               
        }
        

        if(CheckSpamInput(_lastUsedInputs, _lastUsedInputs.Count, 5))
        {
            Debug.LogError("Spamming Attack " + _keySpammed);
        }

        
        
           
    }

    bool CheckLast3Input(List<KeyCode> inputList, int listLenght)
    {
        

        return false;
    }

    bool CheckSpamInput(List<KeyCode> inputList, int listLenght,int range)
    {

        for (int i = 0; i < listLenght; i++)
        {
            int j = i + 1;
            int k = j + 1;
            int l = k + 1;
            int o = l + 1;
            int searchRange = range;

            while(searchRange > 0 && j < listLenght)
            {
                if (inputList[i] == inputList[j])
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
