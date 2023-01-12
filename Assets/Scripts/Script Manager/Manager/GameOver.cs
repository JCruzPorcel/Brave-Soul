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

    float mathGold = 0f;
    float factor_time = 1f;
    float factor_enemies = .1f;

    float mathTotalScore = 0f;
    float score_Factor_time = 10f;
    float score_Factor_enemies = 1f;

    float time = 0;
    int enemies = 0;


    private void Start()
    {
        enemies = PlayerScore.Instance.enemiesKilled;
        time =TimerScript.Instance.minutes;

        mathGold = Mathf.Round((time * factor_time) + (enemies * factor_enemies));

        mathTotalScore = Mathf.Round((time * score_Factor_time) + (enemies * score_Factor_enemies));



        timeSurvived_Text.text = string.Format("<b>{2}</b>:  <color=white><size=50>{0:00} : {1:00}</size></color>", TimerScript.Instance.minutes, TimerScript.Instance.seconds, timeSurvive);
        gold_Text.text = string.Format("{0}:  <color=white><size=50>{1}</size></color>", gold, mathGold);

        enemiesKilled_Text.text = string.Format("{0}:  <color=white><size=50>{1}</size></color>", enemiesKilled, enemies);
        totalScore_Text.text = string.Format("{0}:  <color=white><size=80>{1}</size></color>", totalScore, mathTotalScore);
        go_To_MainMenu_Text.text = string.Format("{0}", go_To_MainMenu);

        GameManager.Instance.PlayerGold += (int)mathGold;
        GameManager.Instance.Save();
    }
}
