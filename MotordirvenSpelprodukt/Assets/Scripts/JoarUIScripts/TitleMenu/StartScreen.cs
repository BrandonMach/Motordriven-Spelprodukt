using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _SelfCanvasGroup;
    [SerializeField] private CanvasGroup _canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickEnter();
    }

    void ClickEnter()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _SelfCanvasGroup.alpha = 0.0f;
            _canvasGroup.alpha = 1.0f;
        }
    }
}
