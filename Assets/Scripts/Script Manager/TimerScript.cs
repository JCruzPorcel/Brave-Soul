using TMPro;
using UnityEngine;

public class TimerScript : Singleton<TimerScript>
{
    float timeLeft;
    public bool timerOn = true;
    public TMP_Text timerText;
    public float minutes;
    public float seconds;
    [SerializeField] int maxTime;
    [SerializeField] float speed;

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame) return;


        if (timerOn)
        {
            if (minutes < maxTime)
            {
                timeLeft += Time.deltaTime * speed;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 30;
                timerOn = false;

                GameManager.Instance.GameOver();
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

