using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    public float updateInterval = 0.5f;
    private float accum = 0f;
    private int frames = 0;
    private float timeleft;
    public TMP_Text fpsText;
    public GameObject text_Go;
    public bool isActive;


    private void Start()
    {
        timeleft = updateInterval;
        PlayerData playerData = SaveManager.LoadPlayerData();
        isActive = playerData.FPS;

        text_Go.SetActive(isActive);
    }

    public void Fps(bool fps)
    {
        text_Go.SetActive(fps);
    }

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0f)
        {
            float fps = accum / frames;
            string format = string.Format("{0:F2} FPS", fps);
            fpsText.text = format;

            if (fps < 30)
            {
                fpsText.color = Color.yellow;
            }
            else if (fps < 10)
            {
                fpsText.color = Color.red;
            }
            else
            {
                fpsText.color = Color.green;
            }

            timeleft = updateInterval;
            accum = 0f;
            frames = 0;
        }
    }
}
