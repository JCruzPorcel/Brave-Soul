using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    bool showDamage;
    bool showFps;
    bool fullscreen;
    bool highPerformance;
    bool colorblind;



    public void ShowDamage(GameObject checkmark)
    {
        showDamage = !showDamage;

        checkmark.SetActive(showDamage);
    }

    public void ShowFps(GameObject checkmark)
    {
        showFps = !showFps;

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
