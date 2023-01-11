using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

[System.Serializable]
public class LanguageButton
{
    public string name;
    public TextMeshProUGUI button;
}

public class LanguageManager : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;
    public LanguageButton[] buttons;

    public Dictionary<string, string[]> languageDict = new Dictionary<string, string[]>();
    public Dictionary<string, string[]> optionTexts = new Dictionary<string, string[]>();
    Dictionary<string, string> languageNames = new Dictionary<string, string>() {
    { "English", "English" },
    { "Spanish", "Spanish" },
    { "French", "French" },
    { "Inglés", "English" },
    { "Español", "Spanish" },
    { "Francés", "French" },
    { "Anglais", "English" },
    { "Espagnol", "Spanish" },
    { "Français", "French" }
    };

    public Dictionary<string, string> languageTranslations = new Dictionary<string, string>() {
        { "English", "English" },
        { "Spanish", "Español" },
        { "French", "Français" }
    };

    private string systemLanguage;
    private int languageIndex;
    public string previous_Language;

    GameManager gameManager;

    void Awake()
    {
        string jsonString = File.ReadAllText(Application.streamingAssetsPath + "/languages.json");
        LanguageData data = JsonUtility.FromJson<LanguageData>(jsonString);

        languageDict.Add("English", data.English);
        languageDict.Add("Spanish", data.Spanish);
        languageDict.Add("French", data.French);

        optionTexts.Add("English", new string[] { "English", "Spanish", "French" });
        optionTexts.Add("Spanish", new string[] { "Inglés", "Español", "Francés" });
        optionTexts.Add("French", new string[] { "Anglais", "Espagnol", "Français" });

        systemLanguage = Application.systemLanguage.ToString();
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        PlayerData playerData = SaveManager.LoadPlayerData();

        languageDropdown.options.Clear();

        previous_Language = playerData.Language;

        Debug.Log(previous_Language);

        if (string.IsNullOrEmpty(previous_Language))
        {
            Debug.Log("Si");

            for (int i = 0; i < optionTexts[systemLanguage].Length; i++)
            {
                languageDropdown.options.Add(new TMP_Dropdown.OptionData(optionTexts[systemLanguage][i]));

                if (optionTexts["English"][i] == systemLanguage)
                {
                    languageIndex = i;
                    languageDropdown.value = languageIndex;
                    SetLanguage(languageIndex);
                }
                else if (optionTexts["Spanish"][i] == systemLanguage)
                {
                    languageIndex = i;
                    languageDropdown.value = languageIndex;
                    SetLanguage(languageIndex);
                }
                else if (optionTexts["French"][i] == systemLanguage)
                {
                    languageIndex = i;
                    languageDropdown.value = languageIndex;
                    SetLanguage(languageIndex);
                }
            }
        }
        else
        {
            Debug.Log("No");
            for (int i = 0; i < optionTexts[previous_Language].Length; i++)
            {
                languageDropdown.options.Add(new TMP_Dropdown.OptionData(optionTexts[previous_Language][i]));

                if (optionTexts["English"][i] == previous_Language)
                {
                    languageIndex = i;
                    languageDropdown.value = languageIndex;
                    SetLanguage(languageIndex);
                }
                else if (optionTexts["Spanish"][i] == previous_Language)
                {
                    languageIndex = i;
                    languageDropdown.value = languageIndex;
                    SetLanguage(languageIndex);
                }
                else if (optionTexts["French"][i] == previous_Language)
                {
                    languageIndex = i;
                    languageDropdown.value = languageIndex;
                    SetLanguage(languageIndex);
                }
            }
        }

        Debug.Log(previous_Language);

        languageDropdown.RefreshShownValue();

        languageDropdown.onValueChanged.AddListener(SetLanguage);
    }


    public void SetLanguage(int value)
    {
        string language = languageDropdown.options[value].text;

        language = languageNames.ContainsKey(language) ? languageNames[language] : language;

        string languageName = languageNames[language];

        string[] texts = languageDict[language];

        if (optionTexts.ContainsKey(language))
            languageDropdown.captionText.text = languageTranslations[languageName];

        if (gameManager.currentGameState == GameState.mainMenu)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].button != null) buttons[i].button.text = texts[i];
            }
        }else
        {
            int p = 5;

            for (int i = 0; i < buttons.Length; i++,p++)
            {
                if (buttons[i].button != null) buttons[i].button.text = texts[p+1];
            }
        }



        if (optionTexts.ContainsKey(language))
        {
            var _texts = optionTexts[language];
            for (int i = 0; i < languageDropdown.options.Count; i++)
            {
                languageDropdown.options[i].text = _texts[i];
            }
        }

        previous_Language = language;

        GameManager.Instance.Previous_Language = previous_Language;
        GameManager.Instance.Save();
    }
}

[System.Serializable]
public class LanguageData
{
    public string[] English;
    public string[] Spanish;
    public string[] French;
}