using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text timeSurvived_Text;
    [SerializeField] TMP_Text gold_Text;
    [SerializeField] TMP_Text enemiesKilled_Text;
    [SerializeField] TMP_Text totalScore_Text;
    [SerializeField] TMP_Text go_To_MainMenu_Text;

    float mathGold = 0f;
    float factor_time = 1f;
    float factor_enemies = 1.5f;  // .1f

    float mathTotalScore = 0f;
    float score_Factor_time = 10f;
    float score_Factor_enemies = 1f;

    int minutes = 0;
    int seconds = 0;
    int enemies = 0;

    public LanguageManager languageManager;
    public string language;
    string[] texts;

    readonly int mainMenu_Language = 33;
    readonly int time_language = 34;
    readonly int enemies_language = 35;
    readonly int gold_language = 36;
    readonly int totalScore_language = 37;


    private void Update()
    {
        language = GameManager.Instance.Previous_Language;

        texts = languageManager.languageDict[language];

        enemies = TimerScript.Instance.enemiesKilled;

        minutes = TimerScript.Instance.minutes;
        seconds = TimerScript.Instance.seconds;

        mathGold = (int)Mathf.Round((minutes * factor_time) + (enemies * factor_enemies));

        mathTotalScore = Mathf.Round((minutes * score_Factor_time) + (enemies * score_Factor_enemies));


        timeSurvived_Text.text = string.Format("<b>{2}</b>:  <color=white><size=50>{0:00} : {1:00}</size></color>", minutes, seconds, texts[time_language]);
        gold_Text.text = string.Format("{0}:  <color=white><size=50>{1}</size></color>", texts[gold_language], mathGold);

        enemiesKilled_Text.text = string.Format("{0}:  <color=white><size=50>{1}</size></color>", texts[enemies_language], enemies);
        totalScore_Text.text = string.Format("{0}:  <color=white><size=80>{1}</size></color>", texts[totalScore_language], mathTotalScore);
        go_To_MainMenu_Text.text = string.Format("{0}", texts[mainMenu_Language]);

        GameManager.Instance.PlayerGold += (int)mathGold;
        GameManager.Instance.Save();
    }
}
