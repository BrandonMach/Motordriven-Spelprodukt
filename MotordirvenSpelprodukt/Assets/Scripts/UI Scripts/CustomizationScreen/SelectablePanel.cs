using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectablePanel : MonoBehaviour
{
    [Header("Selectable Panel Items")]
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _abilitiesPanel;
    [SerializeField] private GameObject _challengesPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _inventoryVariantPanel;
    [SerializeField] private GameObject _panelInfo;
    [SerializeField] private GameObject _shopPanelInfo;
    [SerializeField] private TextMeshProUGUI _panelInfoText;

    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _abilitiesButton;
    [SerializeField] private Button _challengesButton;
    [SerializeField] private Button _shopButton;

    private GameObject[] _panels;
    private Button[] _buttons;
    private int _currentButtonIndex = 0;
    private int _currentPanelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _panels = new GameObject[]
        {
            _inventoryPanel,
            _abilitiesPanel,
            _challengesPanel
        };

        _buttons = new Button[]
        {
            _inventoryButton,
            _abilitiesButton,
            _challengesButton,
            _shopButton
        };

        InventoryPanelClicked();
    }

    private void ToggleInteractable(Button aButton)
    {
        foreach (Button button in _buttons)
        {
            if (aButton == button)
            {
                aButton.interactable = false;
                button.interactable = true;
            }
        }
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
        DeactivateShop();
        SetPanelInfoText("Inventory");

        _inventoryButton.interactable = false;
        _abilitiesButton.interactable = true;
        _challengesButton.interactable = true;
        _shopButton.interactable = true;
        ChallengeManager.Instance.ChallengesActive = false;
    }
    public void AbilitiesPanelClicked()
    {
        SetCurrentPanel(1);
        DeactivateShop();
        SetPanelInfoText("Abilities");

        _abilitiesButton.interactable = false;
        _inventoryButton.interactable = true;
        _challengesButton.interactable = true;
        _shopButton.interactable = true;
        ChallengeManager.Instance.ChallengesActive = false;
    }

    public void ChallengesPanelClicked()
    {
        SetCurrentPanel(2);
        DeactivateShop();
        SetPanelInfoText("Challenges");

        _challengesButton.interactable = false;
        _abilitiesButton.interactable = true;
        _inventoryButton.interactable = true;
        _shopButton.interactable = true;
        ChallengeManager.Instance.ChallengesActive = true;
        ChallengeManager.Instance.ChallengePanelOpen();

        foreach (var challengeButton in ChallengeManager.Instance.ChallengeButtonArray)
        {
            challengeButton.UpdateButtonInfo();
        }
    }

    public void ShopPanelClicked()
    {
        SetCurrentPanel(0);
        ActivateShop();
        SetPanelInfoText("Inventory");

        _shopButton.interactable = false;
        _challengesButton.interactable = true;
        _abilitiesButton.interactable = true;
        _inventoryButton.interactable = true;
        ChallengeManager.Instance.ChallengesActive = false;
       
    }

    private void ActivateShop()
    {
        if (!_shopPanel.activeSelf)
        {
            _shopPanel.SetActive(true);
            _shopPanelInfo.SetActive(true);
            //_inventoryVariantPanel.SetActive(true);
        }
        else if (!_inventoryVariantPanel.activeSelf)
        {
            
        }
        else if (_inventoryPanel.activeSelf)
        {
            _inventoryPanel.SetActive(true);
        }
    }

    private void DeactivateShop()
    {
        _shopPanel.SetActive(false);
        _shopPanelInfo.SetActive(false);
        _inventoryVariantPanel.SetActive(false);
    }

    private void SetPanelInfoText(string text)
    {
        _panelInfoText.text = text;
    }
}
