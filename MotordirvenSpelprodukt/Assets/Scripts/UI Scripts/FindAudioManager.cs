using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FindAudioManager : MonoBehaviour
{
    public Button button;
    public string targetObjectName = "FMODAudioManager";
    GameObject targetObject;

    void Start()
    {
        //// Find the GameObject in the current scene
        //targetObject = GameObject.Find(targetObjectName);
        //if (targetObject != null)
        //{
        //    // Do something with the found object
        //    Debug.Log("Found the object: " + targetObject.name);
        //}
        //else
        //{
        //    Debug.LogWarning("Object not found in the current scene.");
        //}

        button = gameObject.GetComponent<Button>();

        button.onClick.AddListener(delegate () { ChangeEvent(); });
    }

    public void ChangeEvent()
    {
        //FMODController.Instance.ChangeEvent("event:/Music");
    }



}
