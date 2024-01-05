using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string content;
    public string header;

    public RectTransform reT;
    [SerializeField] private VirtualMouseInput _virtualMouseInput;

    private void Start()
    {
        reT = GetComponent<RectTransform>();
        _virtualMouseInput = FindAnyObjectByType<VirtualMouseInput>();
    }

    private void Update()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(header,content);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

    
    
}
