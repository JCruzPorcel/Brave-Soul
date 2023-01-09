using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    bool showDamage = true;
    bool showFps = false;
    bool fullscreen = true;
    bool highPerformance = true;
    bool colorblind = false;


    public AudioMixer mixer;

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public string musicVolumeParameterName = "MusicVolume";
    public string sfxVolumeParameterName = "SfxVolume";

    private void Start()
    {
        Screen.fullScreen = fullscreen;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        MenuManager.Instance.Exit();
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

    public void UpdateSfxVolume()
    {
        
        float dB = 20f * Mathf.Log10(sfxVolumeSlider.value);        

        mixer.SetFloat(sfxVolumeParameterName, dB);
    }

    public void UpdateMusicVolume()
    {
        float dB = 20f * Mathf.Log10(musicVolumeSlider.value);

        mixer.SetFloat(musicVolumeParameterName, dB);
    }
}
