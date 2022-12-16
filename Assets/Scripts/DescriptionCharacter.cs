using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DescriptionCharacter : Singleton<DescriptionCharacter>
{

    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image characterImage;
    [SerializeField] private Image weaponImage;

    [SerializeField] private TMP_Text buyButton;

    [SerializeField] private GameObject goldIcon;
    [SerializeField] private RectTransform buyButtonTransform;

    [SerializeField] private CharacterData currentChar;
    [SerializeField] private CharacterData selectedChar;

    public CharacterData CurrentChar { get => currentChar; set => currentChar = value; }
    public CharacterData SelectedChar { get => selectedChar; set => selectedChar = value; }

    public string select;
    public string play;
    public string unlock;

    bool isSelected;

    public bool IsSelected { get => isSelected; set => isSelected = value; }


    void Update()
    {
        if (currentChar == selectedChar)
        {
            characterName.text = selectedChar.CharName;
            description.text = selectedChar.Description;
            characterImage.sprite = selectedChar.CharImage;
            weaponImage.sprite = selectedChar.StartWeapon.WeaponImage;


            if (selectedChar.IsOwned)
            {
                buyButtonTransform.anchoredPosition = new Vector2(0, 0);
                goldIcon.SetActive(false);

                ButtonState();
            }
            else if (!selectedChar.IsOwned && selectedChar.ItsBuyable)
            {
                buyButtonTransform.anchoredPosition = new Vector2(24.4f, 0);
                goldIcon.SetActive(true);

                BuyCharacter();
            }
            else if (!selectedChar.IsOwned && !selectedChar.ItsBuyable)
            {
                buyButtonTransform.anchoredPosition = new Vector2(0, 0);
                goldIcon.SetActive(false);
                buyButton.text = unlock;
                buyButton.color = Color.magenta;
            }
        }
        else if (currentChar != null)
        {
            characterName.text = currentChar.CharName;
            description.text = currentChar.Description;
            characterImage.sprite = currentChar.CharImage;
            weaponImage.sprite = currentChar.StartWeapon.WeaponImage;


            if (currentChar.IsOwned)
            {
                buyButtonTransform.anchoredPosition = new Vector2(0, 0);
                goldIcon.SetActive(false);

                ButtonState();

            }
            else if (!currentChar.IsOwned && currentChar.ItsBuyable)
            {
                buyButtonTransform.anchoredPosition = new Vector2(24.4f, 0);
                goldIcon.SetActive(true);

                BuyCharacter();

            }
            else if (!currentChar.IsOwned && !currentChar.ItsBuyable)
            {
                buyButtonTransform.anchoredPosition = new Vector2(0, 0);
                goldIcon.SetActive(false);
                buyButton.text = unlock;
                buyButton.color = Color.magenta;
            }
        }
        else
        {
            characterName.text = selectedChar.CharName;
            description.text = selectedChar.Description;
            characterImage.sprite = selectedChar.CharImage;
            weaponImage.sprite = selectedChar.StartWeapon.WeaponImage;


            if (selectedChar.IsOwned)
            {
                buyButtonTransform.anchoredPosition = new Vector2(0, 0);
                goldIcon.SetActive(false);

                ButtonState();

            }
            else if (!selectedChar.IsOwned && selectedChar.ItsBuyable)
            {
                buyButtonTransform.anchoredPosition = new Vector2(24.4f, 0);
                goldIcon.SetActive(true);

                BuyCharacter();

            }
            else if (!selectedChar.IsOwned && !selectedChar.ItsBuyable)
            {
                buyButtonTransform.anchoredPosition = new Vector2(0, 0);
                goldIcon.SetActive(false);
                buyButton.text = unlock;
                buyButton.color = Color.magenta;
            }
        }
    }

    public void ButtonState()
    {
        buyButton.color = Color.white;

        if (CurrentChar == selectedChar)
        {
            if (!isSelected)
            {
                buyButton.text = select;
            }
            else
            {
                buyButton.text = play;
            }
        }
        else if (CurrentChar == null)
        {
            if (!isSelected)
            {
                buyButton.text = select;
            }
            else
            {
                buyButton.text = play;
            }
        }
        else
        {
            buyButton.text = select;
        }
    }

    public void State()
    {
        if (buyButton.text == play)
        {
            MenuManager.Instance.StartGame();
        }

        if (selectedChar.IsOwned)
        {
            isSelected = true;
            GameManager.Instance.CharSelected = selectedChar;
        }
        else if (!selectedChar.IsOwned && selectedChar.ItsBuyable)
        {
            if (GameManager.Instance.PlayerGold >= selectedChar.CharPrice)
            {
                selectedChar.IsOwned = true;
                GameManager.Instance.PlayerGold -= selectedChar.CharPrice;
                GameManager.Instance.Save();
            }
        }
        else if (!selectedChar.IsOwned && !selectedChar.ItsBuyable)
        {
            Application.OpenURL("https://highest.itch.io");
        }
    }


    public void BuyCharacter()
    {
        if (CurrentChar == selectedChar)
        {
            selectedChar.CharPrice.ToString();

            if (GameManager.Instance.PlayerGold >= currentChar.CharPrice)
            {
                buyButton.color = Color.white;
            }
            else
            {
                buyButton.color = Color.red;
            }
        }
        else if (CurrentChar == null)
        {
            buyButton.text = selectedChar.CharPrice.ToString();

            if (GameManager.Instance.PlayerGold >= selectedChar.CharPrice)
            {
                buyButton.color = Color.white;
            }
            else
            {
                buyButton.color = Color.red;
            }
        }
        else
        {
            buyButton.text = currentChar.CharPrice.ToString();

            if (GameManager.Instance.PlayerGold >= currentChar.CharPrice)
            {
                buyButton.color = Color.white;
            }
            else
            {
                buyButton.color = Color.red;
            }
        }        
    }
}
