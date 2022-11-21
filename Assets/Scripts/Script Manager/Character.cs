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
    [SerializeField] private TMP_Text buyButtonText;
    [SerializeField] private TMP_Text pennyGoldText;

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


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {

        if (GameManager.Instance.currentDevice == DeviceType.keyboard)
        {
            ShowCurrentChar();
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        ShowSelectedChar();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameManager.Instance.CharSelected = charData;
        ShowSelectedChar();
    }

    void ShowCurrentChar()
    {
        descriptionName.text = charData.CharName.ToString();
        descriptionImage.sprite = charData.CharImage;
        descriptionWeaponImage.sprite = charData.StartWeapon.WeaponImage;
        description.text = charData.Description.ToString();

        if (charData.IsOwned)
        {
            buyButtonText.text = "Seleccionar";
            pennyGoldText.color = new Color(255, 255, 255, 255); //White

            goldObject.SetActive(false);
        }
        else if (!charData.IsOwned && charData.ItsBuyable)
        {
            descriptionPrice.text = charData.CharPrice.ToString();
            buyButtonText.text = string.Empty;
            goldObject.SetActive(true);

            if (PlayerData.Instance.CurrentGold >= charData.CharPrice)
            {
                pennyGoldText.color = new Color(255, 255, 255, 255); //White
            }
            else
            {
                pennyGoldText.color = new Color(255, 0, 0, 255); //Red
            }
        }
    }

    void ShowSelectedChar()
    {
        descriptionName.text = GameManager.Instance.CharSelected.CharName.ToString();
        descriptionImage.sprite = GameManager.Instance.CharSelected.CharImage;
        descriptionWeaponImage.sprite = GameManager.Instance.CharSelected.StartWeapon.WeaponImage;
        description.text = GameManager.Instance.CharSelected.Description.ToString();
        descriptionPrice.text = GameManager.Instance.CharSelected.CharPrice.ToString();

        if (GameManager.Instance.CharSelected.IsOwned)
        {
            buyButtonText.text = "Seleccionar";
            pennyGoldText.color = new Color(255, 255, 255, 255); //White

            goldObject.SetActive(false);
        }
        else if (!GameManager.Instance.CharSelected.IsOwned && GameManager.Instance.CharSelected.ItsBuyable)
        {
            descriptionPrice.text = GameManager.Instance.CharSelected.CharPrice.ToString();
            buyButtonText.text = string.Empty;
            goldObject.SetActive(true);

            if (PlayerData.Instance.CurrentGold >= GameManager.Instance.CharSelected.CharPrice)
            {
                pennyGoldText.color = new Color(255, 255, 255, 255); //White
            }
            else
            {
                pennyGoldText.color = new Color(255, 0, 0, 255); //Red
            }
        }
    }

    //ToDo: Selected Change Color Black
    //ToDo: Buy Characters
}
