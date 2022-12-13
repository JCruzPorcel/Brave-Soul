using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterManager : MonoBehaviour
{
    private string _characterName;
    public Image character_image;
    public Image weapon_character_image;

    public TMP_Text nameText;


    public virtual void SelectCharacter() { }

    public TMP_Text GetCharacterName()
    {
        nameText.text = _characterName.ToString();

        return nameText;
    }

    public Image GetCharacterImage()
    {

        return character_image;
    }
}
