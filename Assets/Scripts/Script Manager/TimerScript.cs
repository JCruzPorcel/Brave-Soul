using TMPro;
using UnityEngine;

public class TimerScript : Singleton<TimerScript>
{
    float timeLeft;
    public bool timerOn = true;
    public TMP_Text timerText;
    public int minutes;
    public int seconds;
    [SerializeField] int maxTime;
    [SerializeField] float speed;
    public bool canStart = false;

    public int enemiesKilled = 0;

    [SerializeField] TMP_Text enemiesKilled_Text;


    private void Start()
    {
        minutes = 0;
        seconds = 0;
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame || !canStart) return;


        enemiesKilled_Text.text = enemiesKilled.ToString();


        if (timerOn)
        {
            if (minutes < maxTime)
            {
                timeLeft += Time.deltaTime + speed;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 30;
                timerOn = false;

                GameManager.Instance.Victory();
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime++;

        minutes = Mathf.FloorToInt(currentTime / 60);
        seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}

