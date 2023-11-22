using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverCharacterSpacing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private float hoverCharacterSpacing = 20.0f;
    private float originalCharacterSpacing;

    private void Start()
    {
        if (textMeshPro != null)
        {
            originalCharacterSpacing = textMeshPro.characterSpacing;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetCharacterSpacing(hoverCharacterSpacing);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetCharacterSpacing(originalCharacterSpacing);
    }

    private void SetCharacterSpacing(float spacing)
    {
        if (textMeshPro != null)
        {
            textMeshPro.characterSpacing = spacing;
        }
    }
}
