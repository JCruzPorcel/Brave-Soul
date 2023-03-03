using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum GameState
{
    mainMenu,
    menu,
    inGame,
    gameOver,
    win,
}

public class GameManager : SingletonPersistent<GameManager>
{
    public GameState currentGameState = GameState.mainMenu;

    [SerializeField] private CharacterData charSelected;                                        //Character
    public CharacterData CharSelected { get => charSelected; set => charSelected = value; }    // Selected



    #region PlayerData and Console Commands

    private bool m_Damage;
    private bool m_Fps;
    private bool m_FullScreen;
    private bool m_HighPerformance;

    public bool GM_ShowDamage { get => m_Damage; set => m_Damage = value; }
    public bool GM_ShowFps { get => m_Fps; set => m_Fps = value; }
    public bool GM_FullScreen { get => m_FullScreen; set => m_FullScreen = value; }
    public bool GM_HighPerformance { get => m_HighPerformance; set => m_HighPerformance = value; }

    private bool godMode;
    public bool GodMode { get => godMode; set => godMode = value; }

    private int playerGold;
    public int PlayerGold { get => playerGold; set => playerGold = value; }

    private string previous_Language;
    public string Previous_Language { get => previous_Language; set => previous_Language = value; }

    private bool character_Mouz;
    public bool Character_Mouz { get => character_Mouz; set => character_Mouz = value; }

    private bool character_Sweeper;
    public bool Character_Sweeper { get => character_Sweeper; set => character_Sweeper = value; }

    private bool character_Magus;
    public bool Character_Magus { get => character_Magus; set => character_Magus = value; }

    List<CharacterData> characterData = new List<CharacterData>();
    #endregion


    private void Start()
    {
        PlayerData playerData = SaveManager.LoadPlayerData();

        if (playerData != null)
        {
            playerGold = playerData.Gold;
            previous_Language = playerData.Language;
            m_Damage = playerData.Damage;
            m_Fps = playerData.FPS;
            m_FullScreen = playerData.FullScreen;
            m_HighPerformance = playerData.HighPerformance;
            character_Mouz = playerData.Character_Mouz;
            character_Sweeper = playerData.Character_Sweeper;
            character_Magus = playerData.Character_Magus;

            Character_Owned_Check();

        }

        Application.targetFrameRate = 300;
    }

    /*
    public void DeleteData()
    {
        if (Input.GetKey(KeyCode.F))
        {
            previous_Language = null;
            Save();
        }
    }*/

    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void Character_Owned_Check()
    {
        foreach (CharacterData charData in characterData)
        {
            if (charData.name == "Mouz")
            {
                charData.IsOwned = character_Mouz;
            }
            if (charData.name == "Sweeper")
            {
                charData.IsOwned = character_Sweeper;
            }
            if (charData.name == "Magus")
            {
                charData.IsOwned = character_Magus;
                charData.StartWeapon.Prefab.GetComponent<Necronomicon>().WeaponLevel();
            }
        }
    }

    //GameSates

    public void MainMenu()
    {
        SetGameState(GameState.mainMenu);
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void Victory()
    {
        SetGameState(GameState.win);
    }

    public void InGame()
    {
        SetGameState(GameState.inGame);
    }

    public void Menu()
    {
        SetGameState(GameState.menu);
    }

    //GameState

    private void SetGameState(GameState newGameSate)
    {
        if (newGameSate == GameState.mainMenu)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (newGameSate == GameState.inGame)
        {
            Cursor.lockState = CursorLockMode.Locked;

            if (currentGameState == GameState.mainMenu)
            {
                SceneManager.LoadScene("InGame");
            }
        }
        else if (newGameSate == GameState.menu)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (newGameSate == GameState.gameOver)
        {
            Cursor.lockState = CursorLockMode.None;

            MenuManager.Instance.GameOver();
        }

        else if (newGameSate == GameState.win)
        {
            Cursor.lockState = CursorLockMode.None;

            MenuManager.Instance.Victory();
        }

        this.currentGameState = newGameSate;
    }

    public void Save()
    {
        SaveManager.SavePlayerData(this);
    }


    //Console Debug

    public void GameMode(string x)
    {
        switch (x)
        {
            case "normal":
                godMode = false;
                break;

            case "god":
                godMode = true;
                break;

            default:
                Debug.Log("Game Mode Error");
                break;
        }
    }
}
