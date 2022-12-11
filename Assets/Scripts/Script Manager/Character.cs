using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Character : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text charName;
    [SerializeField] private Image charImage;
    [SerializeField] private Image weaponImage;

    [SerializeField] private TMP_Text descriptionName;
    [SerializeField] private Image descriptionImage;
    [SerializeField] private Image descriptionWeaponImage;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text descriptionPrice;

    [SerializeField] private GameObject goldObject;
    [SerializeField] TMP_Text buyButtonText;
    [SerializeField] private TMP_Text pennyGoldText;

    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject backButton;

    [SerializeField] EventSystem m_eventSystem;

    [SerializeField] string playButton = "Play";
    [SerializeField] string selectButton = "Select";
    [SerializeField] string unlockButton = "Unlock";
    bool canShowIt = false;

    [SerializeField] GameObject backPanel;

    [SerializeField] GameManager gameManager;

    [Space(15)]
    [Header("Character Data")]
    [Tooltip("Change to new character scriptableObject")]
    [SerializeField] private CharacterData charData;


    private void Start()
    {
        charName.text = charData.CharName.ToString();
        charImage.sprite = charData.CharImage;
        weaponImage.sprite = charData.StartWeapon.WeaponImage;

        ShowSelectedChar();
    }

    private void Update()
    {
        if (canShowIt)
        {
            if (EventSystem.current.currentSelectedGameObject == buyButton || EventSystem.current.currentSelectedGameObject == backButton)
            {
                ShowSelectedChar();
            }
            canShowIt = false;
        }

        if (PersistentManager.Instance.CharSelected.VarName == charData.VarName)
        {
            backPanel.SetActive(true);
        }
        else
        {
            backPanel.SetActive(false);
        }

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ShowCurrentChar();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        ShowSelectedChar();
    }

    public void OnSelect(BaseEventData eventData)
    {
        ShowCurrentChar();
    }

    public void ShowCurrentChar()
    {
        descriptionName.text = charData.CharName.ToString();
        descriptionImage.sprite = charData.CharImage;
        descriptionWeaponImage.sprite = charData.StartWeapon.WeaponImage;
        description.text = charData.Description.ToString();

        if (charData.IsOwned)
        {
            buyButtonText.text = selectButton.ToString();
            pennyGoldText.color = new Color(255, 255, 255, 255); //White

            goldObject.SetActive(false);
        }
        else if (!charData.IsOwned && charData.ItsBuyable)
        {
            descriptionPrice.text = charData.CharPrice.ToString();
            buyButtonText.text = string.Empty;
            goldObject.SetActive(true);

            if (GameManager.Instance.PlayerGold >= charData.CharPrice)
            {
                pennyGoldText.color = new Color(255, 255, 255, 255); //White
            }
            else
            {
                pennyGoldText.color = new Color(255, 0, 0, 255); //Red
            }
        }
        else if (!charData.IsOwned && !charData.ItsBuyable)
        {
            goldObject.SetActive(false);
            buyButtonText.text = string.Empty;
            buyButtonText.text = unlockButton.ToString();
        }
    }

    public void ShowSelectedChar()
    {
        descriptionName.text = PersistentManager.Instance.CharSelected.CharName.ToString();
        descriptionImage.sprite = PersistentManager.Instance.CharSelected.CharImage;
        descriptionWeaponImage.sprite = PersistentManager.Instance.CharSelected.StartWeapon.WeaponImage;
        description.text = PersistentManager.Instance.CharSelected.Description.ToString();
        descriptionPrice.text = PersistentManager.Instance.CharSelected.CharPrice.ToString();

        if (PersistentManager.Instance.CharSelected.IsOwned)
        {
            buyButtonText.text = selectButton.ToString();
            pennyGoldText.color = new Color(255, 255, 255, 255); //White

            goldObject.SetActive(false);
        }
        else if (!PersistentManager.Instance.CharSelected.IsOwned && PersistentManager.Instance.CharSelected.ItsBuyable)
        {
            descriptionPrice.text = PersistentManager.Instance.CharSelected.CharPrice.ToString();
            buyButtonText.text = string.Empty;
            goldObject.SetActive(true);

            if (GameManager.Instance.PlayerGold >= PersistentManager.Instance.CharSelected.CharPrice)
            {
                pennyGoldText.color = new Color(255, 255, 255, 255); //White
            }
            else
            {
                pennyGoldText.color = new Color(255, 0, 0, 255); //Red
            }
        }
        else if (!PersistentManager.Instance.CharSelected.IsOwned && !PersistentManager.Instance.CharSelected.ItsBuyable)
        {
            goldObject.SetActive(false);
            buyButtonText.text = unlockButton.ToString();
            pennyGoldText.color = new Color(255, 0, 255, 255); //Purple

        }
    }

    public void SelectChar()
    {
        PersistentManager.Instance.CharSelected = charData;
        ShowSelectedChar();

        if (GameManager.Instance.playerInputs.currentControlScheme == "Gamepad")
        {
            m_eventSystem.SetSelectedGameObject(null); // Clean Button
            m_eventSystem.SetSelectedGameObject(buyButton); // New Button
                                                            // Button Nav = None
                                                            // Back button pressed = back
        }
    }

    public void StartGame()
    {
        if (buyButtonText.text == selectButton.ToString())
        {
            buyButtonText.text = playButton.ToString();
        }
        else if (buyButtonText.text == playButton.ToString())
        {
            LevelLoader.Instance.LoadNextLevel("InGame");
            GameManager.Instance.playerInputs.SwitchCurrentActionMap("InGame");
        }
        else if (GameManager.Instance.PlayerGold >= PersistentManager.Instance.CharSelected.CharPrice)
        {
            if (!PersistentManager.Instance.CharSelected.IsOwned && PersistentManager.Instance.CharSelected.ItsBuyable)
            {
                GameManager.Instance.PlayerGold -= PersistentManager.Instance.CharSelected.CharPrice;
                SaveManager.SavePlayerData(gameManager);
                PersistentManager.Instance.CharSelected.IsOwned = true;
                canShowIt = true;
            }
        }
        else if (buyButtonText.text == unlockButton.ToString())
        {
            LevelLoader.Instance.OpenURL("https://highest.itch.io");
        }

    }
}
