using TMPro;
using UnityEngine;

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

    private void Start()
    {
        timeSurvived_Text.text = string.Format("{2}: {0:00} : {1:00}", TimerScript.Instance.minutes, TimerScript.Instance.seconds, timeSurvive);
        gold_Text.text = string.Format("{0}: ", gold);
        enemiesKilled_Text.text = string.Format("{0}: {1}", enemiesKilled, PlayerScore.Instance.enemiesKilled);
        totalScore_Text.text = string.Format("{0}: ", totalScore);
        go_To_MainMenu_Text.text = string.Format("{0}: ", go_To_MainMenu);

    }
}
