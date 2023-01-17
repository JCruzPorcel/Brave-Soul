[System.Serializable]
public class PlayerData
{
    //Player Data
    private int gold = 0;
    public int Gold { get => gold; set => gold = value; }


    //Menu Data
    private float m_MusicVolume = .5f;
    private float m_EffectsVolume = .5f;

    public float MusicVolume { get => m_MusicVolume; set => m_MusicVolume = value; }
    public float EffectsVolume { get => m_EffectsVolume; set => m_EffectsVolume = value; }

    private bool m_Damage = false;
    private bool m_Fps = false;
    private bool m_FullScreen = true ;
    private bool m_HighPerformance = true;

    public bool Damage { get => m_Damage; set => m_Damage = value; }
    public bool FPS { get => m_Fps; set => m_Fps = value; }
    public bool FullScreen { get => m_FullScreen; set => m_FullScreen = value; }
    public bool HighPerformance { get => m_HighPerformance; set => m_HighPerformance = value; }

    private string language = string.Empty;
    public string Language { get => language; set => Language = value; }

    private bool m_character_Mouz = true;
    public bool Character_Mouz { get => m_character_Mouz; set => m_character_Mouz = value; }

    private bool m_character_Sweeper;
    public bool Character_Sweeper { get => m_character_Sweeper; set => m_character_Sweeper = value; }

    private bool m_character_Magus;
    public bool Character_Magus { get => m_character_Magus; set => m_character_Magus = value; }


    public PlayerData(GameManager player)
    {
        gold = player.PlayerGold;
        language = player.Previous_Language;

        m_Damage = player.GM_ShowDamage;
        m_Fps = player.GM_ShowFps;
        m_FullScreen = player.GM_FullScreen;
        m_HighPerformance = player.GM_HighPerformance;

        m_character_Mouz = player.Character_Mouz;
        m_character_Sweeper = player.Character_Sweeper;
        m_character_Magus = player.Character_Magus;
    }
}