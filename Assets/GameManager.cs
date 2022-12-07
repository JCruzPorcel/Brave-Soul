using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public enum GameState
{
    mainMenu,
    menu,
    inGame,
    gameOver,
    transition
}

public enum DeviceType
{
    keyboard,
    gamepad
}

public class GameManager : SingletonPersistent<GameManager>
{
    public GameState currentGameState = GameState.mainMenu;
    public DeviceType currentDevice = DeviceType.keyboard;

    [SerializeField] EventSystem m_eventSystem;

    [SerializeField] InputSystemUIInputModule inputSystemModule;

    [SerializeField] GameObject m_SceneTransition;
    [SerializeField] GameObject m_startButton;
    [SerializeField] GameObject m_lastButton;

    public PlayerInput playerInputs;
    PlayerActions playerActions;

    [SerializeField] private CharacterData charSelected;                                        //Character
    public CharacterData CharSelected { get => charSelected; set => charSelected = value; }    // Selected


    private bool m_textDamage = true;
    private bool m_textFps = false;
    private bool m_fullScreen = true;
    private bool m_lowQuality = true;
    private bool m_daltonism = false;

    private int closeMenu = 0;
    private int openMenu = 2; // Refactor with enum :(


    private void Start()
    {
        m_SceneTransition.SetActive(true);
        inputSystemModule = GameObject.Find("EventSystem").GetComponent<InputSystemUIInputModule>();
        m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        playerActions = new PlayerActions();
        playerActions.InGame.Enable();
        playerActions.InMenu.Enable();

        m_lastButton = m_startButton;
    }

    private void LateUpdate()
    {
        CurrentDevice();

        if (Input.GetMouseButtonUp(0))
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        if (currentGameState == GameState.mainMenu || currentGameState == GameState.menu)
        {
            playerActions.InMenu.Back.performed += BackToMenu;
        }
    }

    void BackToMenu(InputAction.CallbackContext context)
    {
        if (currentGameState! == GameState.mainMenu || currentGameState! == GameState.menu)
        {
            closeMenu = LevelLoader.Instance.m_NextMenu;
            openMenu = LevelLoader.Instance.m_CurrentMenu;


            if (closeMenu != 2 && closeMenu != 1 && closeMenu != 0)
            {
                m_startButton = m_lastButton;
                LevelLoader.Instance.CloseMenu(closeMenu);
                LevelLoader.Instance.OpenMenu(openMenu);
                m_eventSystem.SetSelectedGameObject(m_lastButton);
            }
        }
    }

    public void MainMenu()
    {
        SetGameState(GameState.mainMenu);
    }

    public void Transition()
    {
        SetGameState(GameState.transition);
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
        Screen.fullScreen = !m_fullScreen;
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

    public void SelectedMenuButton(GameObject newButtonSelected)
    {
        if (currentGameState! != GameState.transition)
        {
            m_lastButton = m_startButton;
            m_startButton = newButtonSelected;
            m_eventSystem.SetSelectedGameObject(null);

            if (playerInputs.currentControlScheme == "Gamepad")
                m_eventSystem.SetSelectedGameObject(m_startButton);
        }
    }

    public void CurrentDevice()
    {

        if (playerInputs.currentControlScheme == "Gamepad")
        {
            SetDevice(DeviceType.gamepad);
        }
        else if (playerInputs.currentControlScheme == "Keyboard&Mouse")
        {
            SetDevice(DeviceType.keyboard);
        }
    }

    private void SetDevice(DeviceType newDevice)
    {
        if (newDevice == currentDevice)
            return;

        if (newDevice == DeviceType.keyboard)
        {
            Cursor.lockState = CursorLockMode.None;

            inputSystemModule.deselectOnBackgroundClick = true;
        }
        if (newDevice == DeviceType.gamepad)
        {
            Cursor.lockState = CursorLockMode.Locked;

            m_eventSystem.SetSelectedGameObject(null);
            m_eventSystem.SetSelectedGameObject(m_startButton);

            inputSystemModule.deselectOnBackgroundClick = false;
        }

        this.currentDevice = newDevice;
    }

    private void SetGameState(GameState newGameSate)
    {
        if (newGameSate == GameState.mainMenu)
        {
            playerInputs.SwitchCurrentActionMap("InMenu");
        }
        else if (newGameSate == GameState.inGame)
        {
            playerInputs.SwitchCurrentActionMap("InGame");
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (newGameSate == GameState.menu)
        {
            playerInputs.SwitchCurrentActionMap("InMenu");
            Cursor.lockState = CursorLockMode.None;
        }

        this.currentGameState = newGameSate;
    }

}
