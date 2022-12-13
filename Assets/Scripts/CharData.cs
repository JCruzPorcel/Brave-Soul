using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    [SerializeField] TMP_Text characterName;

    [SerializeField] Image characterImage;

    [SerializeField] Image weaponImage;

    [SerializeField] DescriptionCharacter descriptionChar;

    [SerializeField] GameObject panel;

    [Space(10)]
    [SerializeField] CharacterData characterData;

    private void Start()
    {
        characterName.text = characterData.CharName.ToString();
        characterImage.sprite = characterData.CharImage;
        weaponImage.sprite = characterData.StartWeapon.WeaponImage;
    }

    private void Update()
    {
        if (DescriptionCharacter.Instance.SelectedChar == characterData)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        descriptionChar.CurrentChar = characterData;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        descriptionChar.CurrentChar = null;
    }

    public void OnSelect(BaseEventData eventData)
    {

        descriptionChar.IsSelected = false;

        descriptionChar.SelectedChar = characterData;
    }


    public void ButtonSelect(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(go);
    }
}
