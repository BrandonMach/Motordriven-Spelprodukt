using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KingDecision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GoToLoseScreen()
    {
        LoseScreenScript.KingExecution = true;
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void GoToShop()
    {
        //testar gå till concept save money
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
