using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;
    public int characterWrapLimit;
    public RectTransform rectTransform;
    public Vector2 position;

    [SerializeField] private VirtualMouseInput _virtualMouseInput;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        _virtualMouseInput = FindAnyObjectByType<VirtualMouseInput>();
    }
    public void SetText(string header, string content="")
    {
        if(string.IsNullOrEmpty(header)){ headerField.gameObject.SetActive(false); }
        else 
        { 
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }
        contentField.text = content;
        int headerLength = headerField.text.Length;
        int contentLength = headerField.text.Length;
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        Image image = gameObject.GetComponent<Image>();
        image.SetNativeSize();
        
    }
    private void Update()
    {
        //position = new Vector2(Input.mousePosition.x , Input.mousePosition.y+40);

        //float pivotX = position.x / Screen.width;
        //float pivotY = position.y / Screen.height;
        //rectTransform.pivot = new Vector2(pivotX, pivotY);


        if (_virtualMouseInput != null)
        {
            position = new Vector2(_virtualMouseInput.cursorTransform.position.x + 830, _virtualMouseInput.cursorTransform.position.y );

            float pivotX = position.x / Screen.width;
            float pivotY = position.y / Screen.height;
            rectTransform.pivot = new Vector2(pivotX, pivotY);


            transform.position = position;  //new Vector2(_virtualMouseInput.cu rsorTransform.position.x, _virtualMouseInput.cursorTransform.position.x + 40);
        }
        else
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 40);

            float pivotX = position.x / Screen.width;
            float pivotY = position.y / Screen.height;
            rectTransform.pivot = new Vector2(pivotX, pivotY);

            transform.position = position;
        }

        //position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 40);

        //float pivotX = position.x / Screen.width;
        //float pivotY = position.y / Screen.height;
        //rectTransform.pivot = new Vector2(pivotX, pivotY);

        //transform.position = position;


    }
}
