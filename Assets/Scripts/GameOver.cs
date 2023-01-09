using System.ComponentModel;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text timeSurvived_Text;
    [SerializeField] TMP_Text gold_Text;
    [SerializeField] TMP_Text enemiesKilled_Text;
    [SerializeField] TMP_Text totalScore_Text;
    [SerializeField] TMP_Text go_To_MainMenu_Text;


    public string timeSurvive;
    public string gold;
    public string enemiesKilled;
    public string totalScore;
    public string go_To_MainMenu;

    float mathGold = 0;
    float factor_time = 1f;
    float factor_enemies = .8f;

    float mathTotalScore = 0;
    float score_Factor_time = 5;
    float score_Factor_enemies = .3f;

    float time = 0;
    int enemies = 0;


    private void Start()
    {
        enemies = PlayerScore.Instance.enemiesKilled;
        time = Mathf.Round((TimerScript.Instance.minutes * 1f) + (TimerScript.Instance.seconds * .35f));

        mathGold = Mathf.Round((time * factor_time) + (enemies * factor_enemies));

        mathTotalScore = Mathf.Round((time * score_Factor_time) + (enemies * score_Factor_enemies));



        timeSurvived_Text.text = string.Format("{2}: <color=white>{0:00} : {1:00}</color>", TimerScript.Instance.minutes, TimerScript.Instance.seconds, timeSurvive);
        gold_Text.text = string.Format("{0}: <color=white>{1}</color>", gold, mathGold);

        enemiesKilled_Text.text = string.Format("{0}: <color=white>{1}</color>", enemiesKilled, enemies);
        totalScore_Text.text = string.Format("{0}: <color=white>{1}</color>", totalScore, mathTotalScore);
        go_To_MainMenu_Text.text = string.Format("{0}", go_To_MainMenu);

        GameManager.Instance.Save();
    }
}
