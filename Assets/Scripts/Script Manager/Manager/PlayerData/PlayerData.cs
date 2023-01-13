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

    private bool m_Damage;
    private bool m_Fps;
    private bool m_FullScreen ;
    private bool m_HighPerformance;

    public bool Damage { get => m_Damage; set => m_Damage = value; }
    public bool FPS { get => m_Fps; set => m_Fps = value; }
    public bool FullScreen { get => m_FullScreen; set => m_FullScreen = value; }
    public bool HighPerformance { get => m_HighPerformance; set => m_HighPerformance = value; }

    private string language;
    public string Language { get => language; set => Language = value; }

    public PlayerData(GameManager player)
    {
        gold = player.PlayerGold;
        language = player.Previous_Language;

        m_Damage = player.GM_ShowDamage;
        m_Fps = player.GM_ShowFps;
        m_FullScreen = player.GM_FullScreen;
        m_HighPerformance = player.GM_HighPerformance;
    }
}