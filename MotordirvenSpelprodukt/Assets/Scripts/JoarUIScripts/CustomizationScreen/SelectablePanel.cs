using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectablePanel : MonoBehaviour
{
    [Header("Selectable Panel Items")]
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _abilitiesPanel;
    [SerializeField] private GameObject _challengesPanel;
    [SerializeField] private GameObject _shopPanel;

    private GameObject[] _panels;
    private int _currentPanelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _panels = new GameObject[]
        {
            _inventoryPanel,
            _abilitiesPanel,
            _challengesPanel,
            _shopPanel
        };
    }

    private void TogglePanel(int panelIndex)
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetActive(i == panelIndex);
        }
    }

    private void SetCurrentPanel(int panelIndex)
    {
        if (panelIndex >= 0 && panelIndex < _panels.Length)
        {
            _currentPanelIndex = panelIndex;
            TogglePanel(_currentPanelIndex);
        }
        else
        {
            Debug.LogError("Invalid panel index");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InventoryPanelClicked() 
    {
        SetCurrentPanel(0);
    }
    public void AbilitiesPanelClicked()
    {
        SetCurrentPanel(1);
    }

    public void ChallengesPanelClicked()
    {
        SetCurrentPanel(2);
    }

    public void ShopPanelClicked()
    {
        SetCurrentPanel(3);
    }
}
