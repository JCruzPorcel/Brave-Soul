using TMPro;
using UnityEngine;

public class TimerScript : Singleton<TimerScript>
{
    float timeLeft;
    public bool timerOn = true;
    public TMP_Text timerText;
    public float minutes;

    private void Update()
    {
        if (timerOn)
        {
            if (minutes < 30)
            {
                timeLeft += Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 30;
                timerOn = false;
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime++;

         minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
