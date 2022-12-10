[System.Serializable]
public class PlayerData
{

    //Player Data
    private int gold;

    public int Gold { get => gold; set => gold = value; }


    //Menu Data
    private float m_MusicVolume;
    private float m_EffectsVolume;

    public float MusicVolume { get => m_MusicVolume; set => m_MusicVolume = value; }
    public float EffectsVolume { get => m_EffectsVolume; set => m_EffectsVolume = value; }

    private bool m_Damage = true;
    private bool m_Fps = false;
    private bool m_FullScreen = true;
    private bool m_LowQuality = true;
    private bool m_Daltonism = false;

    public bool Damage { get => m_Damage; set => m_Damage = value; }
    public bool Fps { get => m_Fps; set => m_Fps = value; }
    public bool FullScreen { get => m_FullScreen; set => m_FullScreen = value; }
    public bool LowQuality { get => m_LowQuality; set => m_LowQuality = value; }
    public bool Daltonism { get => m_Daltonism; set => m_Daltonism = value; }

    public PlayerData(GameManager player)
    {
        //Player Data
        gold = player.PlayerGold;

        //Options Data
        m_MusicVolume = player.MusicVolume;
        m_EffectsVolume = player.MusicVolume;

        m_Damage = player.ShowDamage;
        m_Fps = player.ShowFps;
        m_FullScreen = player.ShowFullScreen;
        m_LowQuality = player.ShowLowQuality;
        m_Daltonism = player.ShowDaltonism;
    }
}