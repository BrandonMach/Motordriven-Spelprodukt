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
      
    }

    private void Update()
    {
        

    }


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
        _textMeshPro.characterSpacing = hoverCharacterSpacing;
        //Debug.Log("Entered!");

        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        _textMeshPro.characterSpacing = originalCharacterSpacing;
        //Debug.Log("Exit!");

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
