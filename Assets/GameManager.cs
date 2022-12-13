using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [SerializeField] private CharacterData charSelected;                                        //Character
    public CharacterData CharSelected { get => charSelected; set => charSelected = value; }    // Selected


    #region PlayerData and Console Commands
    
    private bool godMode;

    public bool GodMode { get => godMode; set => godMode = value; }

    private int playerGold;

    public int PlayerGold { get => playerGold; set => playerGold = value; }
    #endregion  


    private void Start()
    {
        PlayerData playerData = SaveManager.LoadPlayerData();
        playerGold = playerData.Gold;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }


    //GameSates

    public void MainMenu()
    {
        SetGameState(GameState.mainMenu);
    }

    public void InGame()
    {
        SetGameState(GameState.inGame);
        MenuManager.Instance.InGame();
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
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
        }
        else if (newGameSate == GameState.inGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (newGameSate == GameState.menu)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        this.currentGameState = newGameSate;
    }

    public void Save()
    {
        SaveManager.SavePlayerData(this);
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
