using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentationTransition : MonoBehaviour
{
    public GameObject Orc;
    public GameObject swat;
    void Start()
    {
        Orc.SetActive(false);
        //
        //= GetComponent<Canvas>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //AI
        {
            Orc.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)) //AI
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.Y)) //AI
        {
            Instantiate(swat);
        }


    }
}
