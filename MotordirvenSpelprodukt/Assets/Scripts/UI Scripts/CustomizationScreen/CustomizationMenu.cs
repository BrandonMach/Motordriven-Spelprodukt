using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationMenu : MonoBehaviour
{
    [SerializeField] ChallengeManager challengeManager;

    float Angle = 45;

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
        //Angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, TargetRotation, ref r, 0.1f);

        //PlayerObject.transform.rotation = Quaternion.Euler(0, Angle, 0);
       // PlayerObject = Player.Instance.gameObject;
    }

    public void ClickEnterArena()
    {

        SceneManager.LoadScene(2,LoadSceneMode.Single);
    }

    public void RotateLeftButton()
    {

        StartCoroutine(Rotate(Angle));
    } 
    public void RotateRightButton()
    {
        StartCoroutine(Rotate(-Angle));
    }


    IEnumerator Rotate(float angle, float duration = 1.0f)
    {
        Quaternion from = GameManager.Instance._player.transform.rotation;
        Quaternion to = GameManager.Instance._player.transform.rotation;

        to *= Quaternion.Euler(new Vector3(0, 1, 0) * angle);

        float elapse = 0.0f;

        while(elapse < duration)
        {
            GameManager.Instance._player.transform.rotation = Quaternion.Slerp(from, to, elapse / duration);
            elapse += Time.deltaTime;
            yield return null;
        }

        transform.rotation = to;
    }
}
