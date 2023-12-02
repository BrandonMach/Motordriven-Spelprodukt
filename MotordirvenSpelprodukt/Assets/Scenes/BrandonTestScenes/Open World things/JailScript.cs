using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class JailScript : MonoBehaviour
{
    public GameObject GoToPopup;
    TextMeshProUGUI text;
    [SerializeField] string textToDisplay;
    [SerializeField] int ScenIndex;
    [SerializeField] bool isdlc;
    public bool _activatePopup;
    void Start()
    {
        text = GoToPopup.GetComponentInChildren<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {

       

        if (_activatePopup)
        {
            text.text = textToDisplay;
        }

        if(_activatePopup && Input.GetKeyDown(KeyCode.E) && !isdlc)
        {
            SceneManager.LoadScene(ScenIndex, LoadSceneMode.Single);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player.Instance.gameObject)
        {
            _activatePopup = true;
            GoToPopup.SetActive(_activatePopup);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player.Instance.gameObject)
        {
            _activatePopup = false;
            GoToPopup.SetActive(_activatePopup);
        }
    }
}
