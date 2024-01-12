using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle3rdPersonCam : MonoBehaviour
{
    Toggle _toggle;
    bool assigned;
    void Start()
    {

        if (GameManager.Instance != null && !assigned)
        {
            AssignToggle();
            assigned = false;
        }

        if (GameManager.Instance != null)
        {
            _toggle.isOn = GameManager.Instance.OverTheSholderCamActive;
            
        }


    }
    private void Update()
    {
        if(GameManager.Instance != null && !assigned)
        {
            AssignToggle();
            assigned = false;
        }

        if(GameManager.Instance != null)
        {
            _toggle.isOn = GameManager.Instance.OverTheSholderCamActive;
            Debug.LogError(_toggle.isOn);
        }

       
    }

   void AssignToggle()
    {
        
        //Fetch the Toggle GameObject
        _toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        _toggle.onValueChanged.AddListener(delegate {
            GameManager.Instance.Toggle3rdPersonCam();
        });
    }

}
