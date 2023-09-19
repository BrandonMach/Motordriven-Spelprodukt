using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackMoveSequence")]
public class AttackMovesSO : ScriptableObject
{

    [SerializeField] public List<KeyCode> _buttonSequence = new List<KeyCode>();
    [SerializeField] int _comboPriority = 0;




    public bool IsMoveAvilable(List<KeyCode> playerKeyCodes)
    {
        int comboIndex = 0;

        for (int i = 0; i < playerKeyCodes.Count; i++)
        {
            if (playerKeyCodes[i] == _buttonSequence[i])
            {
                comboIndex++;
                if(comboIndex == _buttonSequence.Count) //End of the move
                {
                    return true;
                }
            }
            else
            {
                comboIndex = 0;
            }
        }

        return false;
    }
}
