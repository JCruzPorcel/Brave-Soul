using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CharacterData charData;

    [SerializeField] private TMP_Text charName;
    [SerializeField] private Image charImage;
    [SerializeField] private Image weaponImage;

    [SerializeField] private TMP_Text descriptionName;
    [SerializeField] private Image descriptionImage;
    [SerializeField] private Image descriptionWeaponImage;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text descriptionPrice;

    bool _over;

    private void Start()
    {
        charName.text = charData.CharName.ToString();
        charImage.sprite = charData.CharImage;
        weaponImage.sprite = charData.StartWeapon.WeaponImage;
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        _over = true;
        descriptionName.text = charData.CharName.ToString();
        descriptionImage.sprite = charData.CharImage;
        descriptionWeaponImage.sprite = charData.StartWeapon.WeaponImage;
        description.text = charData.Description.ToString();
        descriptionPrice.text = charData.CharPrice.ToString();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        _over = false;
    }
}
