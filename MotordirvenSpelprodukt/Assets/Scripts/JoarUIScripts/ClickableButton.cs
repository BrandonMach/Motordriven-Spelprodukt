using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    //[SerializeField] private BoxCollider boxCollider;

    private bool isMouseOver;
    private float hoverCharacterSpacing = 20.0f;
    private float originalCharacterSpacing = 0.0f;

    // TODO: add AudioClip _compressClip, _uncompressClip; add AudioSource _source : When click audio will be added as a feature

    private void Start()
    {
        //boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        

    }

    //private bool IsMouseOverBoxCollider()
    //{
    //    //if (boxCollider == null)
    //    //{
    //    //    Debug.LogWarning("BoxCollider not found.");
    //    //    return false;
    //    //}

    //    // Cast a ray from the mouse pointer
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    // Perform a raycast and check if it hits the BoxCollider
    //    if (Physics.Raycast(ray, out hit) && hit.collider == boxCollider)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    private void SpaceCharacters()
    {
        if (isMouseOver)
        {
            _textMeshPro.characterSpacing = hoverCharacterSpacing;
            Debug.Log("Spacing!");
        }
        else if (!isMouseOver)
        {
            _textMeshPro.characterSpacing = originalCharacterSpacing;
        }
    }

    private void OnMouseEnter()
    {
        _textMeshPro.characterSpacing = 20;
        Debug.Log("Entered!");

        isMouseOver = true;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //_img.sprite = _pressed;
        Debug.Log("Clicked!");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //_img.sprite = _default;
    }

    public void IWasClicked()
    {
        Debug.Log("Clicked!");
    }


}
