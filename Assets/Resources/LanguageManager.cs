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
    public TMP_Text languageDropdownText;
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

    private string systemLanguage;
    private int languageIndex;

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

        languageDropdown.options.Clear();

        for (int i = 0; i < optionTexts[systemLanguage].Length; i++)
        {
            languageDropdown.options.Add(new TMP_Dropdown.OptionData(optionTexts[systemLanguage][i]));
            if (optionTexts[systemLanguage][i] == systemLanguage)
            {
                languageIndex = i;
                languageDropdown.value = languageIndex;
                SetLanguage(languageIndex);
            }
        }

        if (languageIndex == -1)
        {
            languageIndex = 0;
            SetLanguage(languageIndex);
        }
        languageDropdown.RefreshShownValue();

        languageDropdown.onValueChanged.AddListener(SetLanguage);
    }


    public void SetLanguage(int value)
    {
        int index = value;

        string language = languageDropdown.options[index].text;

        language = languageNames.ContainsKey(language) ? languageNames[language] : language;

        string[] texts = languageDict[language];

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].button != null) buttons[i].button.text = texts[i];
        }

        if (optionTexts.ContainsKey(language))
        {
            var _texts = optionTexts[language];
            for (int i = 0; i < languageDropdown.options.Count; i++)
            {
                languageDropdown.options[i].text = _texts[i];
            }
        }
    }
}

[System.Serializable]
public class LanguageData
{
    public string[] English;
    public string[] Spanish;
    public string[] French;
}