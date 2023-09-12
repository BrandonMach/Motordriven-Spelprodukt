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

    private bool isMouseOver;
    private float hoverCharacterSpacing = 0.20f;
    private float originalCharacterSpacing = 0.0f;
    [SerializeField]  private BoxCollider2D collider;

    // TODO: add AudioClip _compressClip, _uncompressClip; add AudioSource _source : When click audio will be added as a feature

    private void Start()
    {
        
    }

    private void Update()
    {
        SpaceCharacters();

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

    private void MouseHover()
    {
        bool hovered = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        foreach (RaycastHit hit in Physics.RaycastAll(ray))
        {
            if (hit.collider == collider)
            {
                hovered = true;
                break;
            }
        }
    }

    private void OnMouseEnter()
    {
        //_textMeshPro.characterSpacing = 20;
        Debug.Log("Entered!");

        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        _textMeshPro.characterSpacing = 0;
        Debug.Log("Exited!");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //_img.sprite = _pressed;
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
