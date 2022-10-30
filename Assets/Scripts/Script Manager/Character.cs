using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterData charData;

    [SerializeField] private TMP_Text charName;
    [SerializeField] private Image charImage;
    [SerializeField] private Image weaponImage;

    private void Start()
    {
        charName.text = charData.CharName.ToString();
        charImage.sprite = charData.CharImage;
        weaponImage.sprite = charData.StartWeapon.WeaponImage;
    }
}
