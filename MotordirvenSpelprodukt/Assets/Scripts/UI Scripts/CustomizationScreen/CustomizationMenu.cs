using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationMenu : MonoBehaviour
{
    [SerializeField] ChallengeManager challengeManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        challengeManager = ChallengeManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickEnterArena()
    {

        SceneManager.LoadScene(2,LoadSceneMode.Single);
    }
}
