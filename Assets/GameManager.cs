using TMPro;
using UnityEditor;
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

    private float m_MusicVolume;
    private float m_EffectsVolume;

    public float MusicVolume { get => m_MusicVolume; set => m_MusicVolume = value; }
    public float EffectsVolume { get => m_EffectsVolume; set => m_EffectsVolume = value; }


    #region MenuOptions
    private bool m_ShowDamage = true;
    private bool m_ShowFps = false;
    private bool m_ShowFullScreen = true;
    private bool m_ShowLowQuality = true;
    private bool m_ShowDaltonism = false;

    public bool ShowDamage { get => m_ShowDamage; set => m_ShowDamage = value; }
    public bool ShowFps { get => m_ShowFps; set => m_ShowFps = value; }
    public bool ShowFullScreen { get => m_ShowFullScreen; set => m_ShowFullScreen = value; }
    public bool ShowLowQuality { get => m_ShowLowQuality; set => m_ShowLowQuality = value; }
    public bool ShowDaltonism { get => m_ShowDaltonism; set => m_ShowDaltonism = value; }
    #endregion


    #region MenuNav
    private int closeMenu = 0;
    private int openMenu = 2; // Refactor with enum :(
    #endregion


    #region PlayerData and Console Commands
    private int playerGold = 0;
    [SerializeField] private TMP_Text currentGold_Text;

    public int PlayerGold { get => playerGold; set => playerGold = value; }

    private bool godMode;

    public bool GodMode { get => godMode; set => godMode = value; }

#endregion



    //Method's

    private void Start()
    {
        PlayerData playerData = SaveManager.LoadPlayerData();
        playerGold = playerData.Gold;

        m_SceneTransition.SetActive(true);
        inputSystemModule = GameObject.Find("EventSystem").GetComponent<InputSystemUIInputModule>();
        m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        playerActions = new PlayerActions();
        playerActions.InGame.Enable();
        playerActions.InMenu.Enable();

        m_lastButton = m_startButton;
    }

    private void Update()
    {
        if (currentGameState == GameState.mainMenu)
        {
            currentGold_Text.text = playerGold.ToString("n0");
        }
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




    //Button Inputs

    void BackToMenu(InputAction.CallbackContext context)
    {
        if (currentGameState! == GameState.mainMenu)
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

    public void OnPause(InputValue value)
    {
/*
        if (GameManager.Instance.currentGameState == GameState.inGame || GameManager.Instance.currentGameState == GameState.menu)
        {
            MenuManager.Instance.showOptions = !MenuManager.Instance.showOptions;

            MenuManager.Instance.m_Menu.SetActive(MenuManager.Instance.showOptions);

            if (MenuManager.Instance.showOptions)
            {
                GameManager.Instance.Menu();
            }
            else
            {
                GameManager.Instance.InGame();
            }
        }*/
    }




    //GameSates

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




    //Options Menu
    public void Damage(GameObject DamageCheck)
    {
        m_ShowDamage = !m_ShowDamage;
        DamageCheck.SetActive(m_ShowDamage);
    }

    public void FPS(GameObject FpsCheck)
    {
        m_ShowFps = !m_ShowFps;
        FpsCheck.SetActive(m_ShowFps);
    }

    public void FullScreen(GameObject FullScreenCheck)
    {
        m_ShowFullScreen = !m_ShowFullScreen;
        Screen.fullScreen = !m_ShowFullScreen;
        FullScreenCheck.SetActive(m_ShowFullScreen);
    }

    public void LowQuality(GameObject LowQualityCheck)
    {
        m_ShowLowQuality = !m_ShowLowQuality;
        LowQualityCheck.SetActive(m_ShowLowQuality);
    }

    public void Daltonism(GameObject DaltonismCheck)
    {
        m_ShowDaltonism = !m_ShowDaltonism;
        DaltonismCheck.SetActive(m_ShowDaltonism);
    }




    //Menu Nav
    public void SelectedMenuButton(GameObject newButtonSelected)
    {
        if (currentGameState! != GameState.transition)
        {
            m_lastButton = m_startButton;
            m_startButton = newButtonSelected;
            m_eventSystem.SetSelectedGameObject(null);
            m_eventSystem.SetSelectedGameObject(m_startButton);
        }
    }




    //GameState and Device

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




    //Console Debug

    public void GameMode(int x)
    {
        switch (x)
        {
            case 0:
                godMode = false;
                break;

            case 1:
                godMode = true;
                break;

            default:
                Debug.Log("Error.");
                break;
        }
    }
}
