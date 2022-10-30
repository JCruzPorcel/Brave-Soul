using UnityEngine;

public enum GameState
{
    mainMenu,
    menu,
    inGame,
    gameOver,
}

public class GameManager : SingletonPersistent<GameManager>
{
    public GameState currentGameState = GameState.mainMenu;
    [SerializeField] GameObject m_SceneTransition;


    private bool m_textDamage = true;
    private bool m_textFps = false;
    private bool m_fullScreen = true;
    private bool m_lowQuality = true;
    private bool m_daltonism = false;


    private void Start()
    {
        m_SceneTransition.SetActive(true);
    }

    public void MainMenu()
    {
        SetGameState(GameState.mainMenu);
    }

    public void InGame()
    {
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void Menu()
    {
        SetGameState(GameState.menu);
    }

    public void ShowDamage(GameObject DamageCheck)
    {
        m_textDamage = !m_textDamage;

        if (m_textDamage)
        {
            DamageCheck.SetActive(true);
        }
        else
        {
            DamageCheck.SetActive(false);
        }
    }

    public void ShowFPS(GameObject FpsCheck)
    {
        m_textFps = !m_textFps;

        if (m_textFps)
        {
            FpsCheck.SetActive(true);
        }
        else
        {
            FpsCheck.SetActive(false);
        }
    }

    public void FullScreen(GameObject FullScreenCheck)
    {
        m_fullScreen = !m_fullScreen;

        if (m_fullScreen)
        {
            FullScreenCheck.SetActive(true);
        }
        else
        {
            FullScreenCheck.SetActive(false);
        }
    }

    public void LowQuality(GameObject LowQualityCheck)
    {
        m_lowQuality = !m_lowQuality;

        if (m_lowQuality)
        {
            LowQualityCheck.SetActive(true);
        }
        else
        {
            LowQualityCheck.SetActive(false);
        }
    }

    public void Daltonism(GameObject DaltonismCheck)
    {
        m_daltonism = !m_daltonism;

        if (m_daltonism)
        {
            DaltonismCheck.SetActive(true);
        }
        else
        {
            DaltonismCheck.SetActive(false);
        }
    }

    private void SetGameState(GameState newGameSate)
    {
        if (newGameSate == GameState.mainMenu)
        {

        }

        this.currentGameState = newGameSate;
    }
}