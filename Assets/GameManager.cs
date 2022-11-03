using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] PlayerInput playerInputs;

    private bool m_textDamage = true;
    private bool m_textFps = false;
    private bool m_fullScreen = true;
    private bool m_lowQuality = true;
    private bool m_daltonism = false;

    private void Start()
    {
        m_SceneTransition.SetActive(true);
    }

    private void Update()
    {

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
        DamageCheck.SetActive(m_textDamage);
    }

    public void ShowFPS(GameObject FpsCheck)
    {
        m_textFps = !m_textFps;
        FpsCheck.SetActive(m_textFps);
    }

    public void FullScreen(GameObject FullScreenCheck)
    {
        m_fullScreen = !m_fullScreen;
        FullScreenCheck.SetActive(m_fullScreen);
    }

    public void LowQuality(GameObject LowQualityCheck)
    {
        m_lowQuality = !m_lowQuality;
        LowQualityCheck.SetActive(m_lowQuality);
    }

    public void Daltonism(GameObject DaltonismCheck)
    {
        m_daltonism = !m_daltonism;
        DaltonismCheck.SetActive(m_daltonism);
    }

    public void CurrentDevice()
    {
        if (playerInputs.currentControlScheme == "Gamepad")
        {
            Debug.Log("asdasd");
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