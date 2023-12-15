using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior instance;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }
    public void sceneToMoveTo()
    {
        SceneManager.LoadScene("CustomizationMenu");
    }
    public void OnCollisionEnter(Collision collision)
    {
        GameBehavior.instance.sceneToMoveTo();
        Debug.Log("here");
    }
}
