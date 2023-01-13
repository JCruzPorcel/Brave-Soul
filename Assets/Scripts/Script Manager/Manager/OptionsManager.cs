using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : Singleton<OptionsManager>
{
    bool fullscreen;
    bool highPerformance;
    bool showFps;
    bool showDamage;

    public AudioMixer mixer;

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public string musicVolumeParameterName = "MusicVolume";
    public string sfxVolumeParameterName = "SfxVolume";

    public GameObject m_Fullscreen_CheckMark;
    public GameObject m_HighPerformance_CheckMark;
    public GameObject m_ShowFPS_CheckMark;
    public GameObject m_showDamage_CheckMark;

    public FpsCounter fpsCounter;

    private void Start()
    {
        AudioManager.Instance.OnSoundSettingsSaved();

        PlayerData playerData = SaveManager.LoadPlayerData();

        fullscreen = playerData.FullScreen;
        highPerformance = playerData.HighPerformance;
        showFps = playerData.FPS;
        showDamage = playerData.Damage;

        m_showDamage_CheckMark.SetActive(showDamage);
        m_ShowFPS_CheckMark.SetActive(showFps);
        m_Fullscreen_CheckMark.SetActive(fullscreen);
        m_HighPerformance_CheckMark.SetActive(highPerformance);
    }

    public void Exit()
    {
        GameManager.Instance.Save();
        MenuManager.Instance.Exit();
    }

    public void ShowDamage()
    {
        showDamage = !showDamage;

        m_showDamage_CheckMark.SetActive(showDamage);

        GameManager.Instance.GM_ShowDamage = showDamage;
        GameManager.Instance.Save();
    }

    public void ShowFps()
    {
        showFps = !showFps;

        m_ShowFPS_CheckMark.SetActive(showFps);
        fpsCounter.Fps(showFps);

        GameManager.Instance.GM_ShowFps = showFps;
        GameManager.Instance.Save();
    }

    public void FullScreen()
    {
        fullscreen = !fullscreen;

        m_Fullscreen_CheckMark.SetActive(fullscreen);

#if UNITY_EDITOR
        EditorWindow.focusedWindow.maximized = fullscreen;
#endif


        Screen.fullScreen = fullscreen;

        GameManager.Instance.GM_FullScreen = fullscreen;
        GameManager.Instance.Save();
    }

    public void HighPerformance()
    {
        highPerformance = !highPerformance;

        m_HighPerformance_CheckMark.SetActive(highPerformance);

        if (highPerformance)
        {
            QualitySettings.SetQualityLevel(0);
        }
        else
        {
            QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1);
        }

        GameManager.Instance.GM_HighPerformance = highPerformance;
        GameManager.Instance.Save();
    }


    public void OpenURL(string link)
    {
        Application.OpenURL(link);
    }

    public void UpdateSfxVolume()
    {
        float dB = 20f * Mathf.Log10(sfxVolumeSlider.value);

        mixer.SetFloat(sfxVolumeParameterName, dB);
        AudioManager.Instance.OnVolumeChanged();
        GameManager.Instance.Save();
    }

    public void UpdateMusicVolume()
    {
        float dB = 20f * Mathf.Log10(musicVolumeSlider.value);

        mixer.SetFloat(musicVolumeParameterName, dB);
        AudioManager.Instance.OnVolumeChanged();
        GameManager.Instance.Save();
    }
}
