using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    #region Singleton


    private static GameManager _instance;
    public static GameManager Instance { get => _instance; set => _instance = value; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }
        Instance = this;



      

       


    }



    #endregion


    public  enum CurrentScen
    {
        MainMenuScene,
        CustomizationScene,
        AreaScen,
        ShopScen
    }

    public CurrentScen _currentScen;


    Scene currentScene;
    public static float PlayerCoins; //Static så att anadra scener kan få access
    public static int ChampionsKilled;


    void Start()
    {

        //För testing
        PlayerCoins = 50;
        //Debug.Log("Coins" +     PlayerCoins);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Coins" + PlayerCoins);
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex == 0)
        {
            _currentScen = CurrentScen.MainMenuScene;
            Debug.Log("In MainMenuScene");
        }
        if (currentScene.buildIndex == 1)
        {
            _currentScen = CurrentScen.CustomizationScene;
            //Customization Scene
            Debug.Log("In Customization Scene");
        }
        if (currentScene.buildIndex == 2)
        {
            //Brandon new testing Scene
            Debug.Log("In Brandon new testing Scene");
            _currentScen = CurrentScen.AreaScen;
        }


        //switch (currentScene.buildIndex)
        //{
            
        //}
    }

    public void RewardCoins(float amount)
    {
        PlayerCoins += amount;
    }

    public void RemoveCoins(float amount)
    {
        PlayerCoins -= amount;
    }
}
