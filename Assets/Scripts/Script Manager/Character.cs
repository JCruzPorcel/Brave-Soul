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
        GameManager.Instance.CharSelected = charData;

        charName.text = charData.CharName.ToString();
        charImage.sprite = charData.CharImage;
        weaponImage.sprite = charData.StartWeapon.WeaponImage;

        ShowCurrentChar();
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

        if (charData.VarName == GameManager.Instance.CharSelected.VarName)
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
        descriptionName.text = GameManager.Instance.CharSelected.CharName.ToString();
        descriptionImage.sprite = GameManager.Instance.CharSelected.CharImage;
        descriptionWeaponImage.sprite = GameManager.Instance.CharSelected.StartWeapon.WeaponImage;
        description.text = GameManager.Instance.CharSelected.Description.ToString();
        descriptionPrice.text = GameManager.Instance.CharSelected.CharPrice.ToString();

        if (GameManager.Instance.CharSelected.IsOwned)
        {
            buyButtonText.text = selectButton.ToString();
            pennyGoldText.color = new Color(255, 255, 255, 255); //White

            goldObject.SetActive(false);
        }
        else if (!GameManager.Instance.CharSelected.IsOwned && GameManager.Instance.CharSelected.ItsBuyable)
        {
            descriptionPrice.text = GameManager.Instance.CharSelected.CharPrice.ToString();
            buyButtonText.text = string.Empty;
            goldObject.SetActive(true);

            if (GameManager.Instance.PlayerGold >= GameManager.Instance.CharSelected.CharPrice)
            {
                pennyGoldText.color = new Color(255, 255, 255, 255); //White
            }
            else
            {
                pennyGoldText.color = new Color(255, 0, 0, 255); //Red
            }
        }
        else if (!GameManager.Instance.CharSelected.IsOwned && !GameManager.Instance.CharSelected.ItsBuyable)
        {
            goldObject.SetActive(false);
            buyButtonText.text = unlockButton.ToString();
            pennyGoldText.color = new Color(255, 0, 255, 255); //Purple

        }
    }

    public void SelectChar()
    {
        GameManager.Instance.CharSelected = charData;
        ShowSelectedChar();

        if (GameManager.Instance.playerInputs.currentControlScheme == "Gamepad")
        {
            m_eventSystem.SetSelectedGameObject(null); // Clean Button
            m_eventSystem.SetSelectedGameObject(buyButton); // New Button
                                                            // Button Nav = None
                                                            // back button pressed = back
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
        else if (GameManager.Instance.PlayerGold >= GameManager.Instance.CharSelected.CharPrice)
        {
            if (!GameManager.Instance.CharSelected.IsOwned && GameManager.Instance.CharSelected.ItsBuyable)
            {
                GameManager.Instance.PlayerGold -= GameManager.Instance.CharSelected.CharPrice;
                SaveManager.SavePlayerData(gameManager);
                GameManager.Instance.CharSelected.IsOwned = true;
                canShowIt = true;
            }
        }
        else if (buyButtonText.text == unlockButton.ToString())
        {
            LevelLoader.Instance.OpenURL("https://highest.itch.io");
        }

    }

    //ToDo: Selected Change Color Black
    //ToDo: Buy Characters
}
