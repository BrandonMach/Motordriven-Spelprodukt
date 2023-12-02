using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractHubWorld : MonoBehaviour
{

    public GameObject Popup;
    TextMeshProUGUI text;
   
    
    public bool _activatePopup;
    [SerializeField] InteractableObject _currentInteractingObject;
    void Start()
    {

        text = Popup.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {



        if (_activatePopup)
        {
        
        }

        if (_activatePopup && Input.GetKeyDown(KeyCode.E))
        {
            Player.Instance.GetComponent<PlayerMovement>()._canMove = false;
            Popup.SetActive(false);
            _currentInteractingObject.Interacting();

          
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("interactable"))
        {
            text.text = other.GetComponent<InteractableObject>().PopUptext;
            _activatePopup = true;
            Popup.SetActive(_activatePopup);

            _currentInteractingObject = other.GetComponent<InteractableObject>();


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("interactable"))
        {
            _activatePopup = false;
            Popup.SetActive(_activatePopup);
            _currentInteractingObject = null;
        }
    }
}
