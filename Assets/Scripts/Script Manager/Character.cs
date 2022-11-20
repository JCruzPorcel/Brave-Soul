using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField] private TMP_Text charName;
    [SerializeField] private Image charImage;
    [SerializeField] private Image weaponImage;

    [SerializeField] private TMP_Text descriptionName;
    [SerializeField] private Image descriptionImage;
    [SerializeField] private Image descriptionWeaponImage;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text descriptionPrice;


    [Space(15)]
    [Header("Character Prefab")]
    [Tooltip("Change to new character scriptableObject")]
    [SerializeField] private CharacterData charData;


    private GameObject charSelected;

    private void Start()
    {
        charName.text = charData.CharName.ToString();
        charImage.sprite = charData.CharImage;
        weaponImage.sprite = charData.StartWeapon.WeaponImage;

        descriptionName.text = charData.CharName.ToString();
        descriptionImage.sprite = charData.CharImage;
        descriptionWeaponImage.sprite = charData.StartWeapon.WeaponImage;
        description.text = charData.Description.ToString();
        descriptionPrice.text = charData.CharPrice.ToString();
        charSelected = charData.CharPrefab;
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.currentDevice == DeviceType.keyboard)
        {
            descriptionName.text = charData.CharName.ToString();
            descriptionImage.sprite = charData.CharImage;
            descriptionWeaponImage.sprite = charData.StartWeapon.WeaponImage;
            description.text = charData.Description.ToString();
            descriptionPrice.text = charData.CharPrice.ToString();
            Debug.Log("Highlight");

        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        descriptionName.text = charData.CharName.ToString();
        descriptionImage.sprite = charData.CharImage;
        descriptionWeaponImage.sprite = charData.StartWeapon.WeaponImage;
        description.text = charData.Description.ToString();
        descriptionPrice.text = charData.CharPrice.ToString();
        charSelected = charData.CharPrefab;
        Debug.Log("Selected");
    }
}
