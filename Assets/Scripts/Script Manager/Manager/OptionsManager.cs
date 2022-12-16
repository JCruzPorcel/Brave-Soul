using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    bool showDamage = true;
    bool showFps = false;
    bool fullscreen = true;
    bool highPerformance = true;
    bool colorblind = false;

    private void Start()
    {
        Screen.fullScreen = fullscreen;
    }


    public void ShowDamage(GameObject checkmark)
    {
        showDamage = !showDamage;

        checkmark.SetActive(showDamage);
    }

    public void ShowFps(GameObject checkmark)
    {
        showFps = !showFps;

        Screen.fullScreen = fullscreen;

        checkmark.SetActive(showFps);
    }

    public void FullScreen(GameObject checkmark)
    {
        fullscreen = !fullscreen;

        checkmark.SetActive(fullscreen);
    }

    public void HighPerformance(GameObject checkmark)
    {
        highPerformance = !highPerformance;

        checkmark.SetActive(highPerformance);
    }

    public void Colorblind(GameObject checkmark)
    {
        colorblind = !colorblind;

        checkmark.SetActive(colorblind);
    }

    public void OpenURL(string link)
    {
        Application.OpenURL(link);
    }
}
