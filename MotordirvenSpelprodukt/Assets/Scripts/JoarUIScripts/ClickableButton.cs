using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private bool isMouseOver;

    // TODO: add AudioClip _compressClip, _uncompressClip; add AudioSource _source : When click audio will be added as a feature



    private void Update()
    {
        Debug.Log("This method is running");
    }

    private void OnMouseEnter()
    {
        _textMeshPro.characterSpacing = 20;
        Debug.Log("Entered!");

    }

    private void OnMouseExit()
    {
        _textMeshPro.characterSpacing = 0;
        Debug.Log("Exited!");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
    }

    public void IWasClicked()
    {
        Debug.Log("Clicked!");
    }


}
