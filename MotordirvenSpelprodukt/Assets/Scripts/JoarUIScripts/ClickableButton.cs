using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    // TODO: add AudioClip _compressClip, _uncompressClip; add AudioSource _source : When click audio will be added as a feature

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
