using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableObject : MonoBehaviour
{
    public string PopUptext;

    [SerializeField] int scenIndex;
    [SerializeField] GameObject interachHUDPopup;

    bool isInteracting;
    public enum InteractionType
    {
        ScenSwitch,
        interact,
      
    }

    public  InteractionType _interactionType;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInteracting &&_interactionType == InteractionType.interact && Input.GetKeyDown(KeyCode.Q))
        {
            Player.Instance.GetComponent<PlayerMovement>()._canMove = true;
            interachHUDPopup.SetActive(false);
            isInteracting = false;
        }


       
    }


    public void Interacting()
    {
        isInteracting = true;


        if(_interactionType == InteractionType.ScenSwitch)
        {
            GoToScen();
        }
        else if(_interactionType == InteractionType.interact)
        {
            OpenInteractionHUDPopup();
        }
    
    }

    public void GoToScen()
    {
        SceneManager.LoadScene(scenIndex, LoadSceneMode.Single);
    }

    public void OpenInteractionHUDPopup()
    {
       
        interachHUDPopup.SetActive(true);
    }




    
}
