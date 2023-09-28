using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectablePanelButton : Button
{
    Color _normalColor = new Color(96, 96, 96);
    Color _pressedColor = new Color(200, 200, 200);
    bool _isSelected = false;
    ColorBlock _colorsBlock;

    // Start is called before the first frame update
    void Start()
    {
        _colorsBlock = colors;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (!_isSelected)
        {
            ButtonIsSelected();
        }
        else
        {
            ButtonIsUnSelected();
        }

    }

    private void ButtonIsSelected()
    {
        _isSelected = true;
        _colorsBlock.normalColor = _pressedColor;
    }

    private void ButtonIsUnSelected()
    {
        _isSelected = false;
        _colorsBlock.normalColor = _normalColor;
    }
}
