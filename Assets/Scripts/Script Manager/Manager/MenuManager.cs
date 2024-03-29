using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum MenuState
{
    PressToStart,
    MainMenu,
    CharacterSelection,
    PowerUp,
    Credits,
    Options,
    ExitGame,
    Transition,
    InGame, //InGame
    GameOver,
    LevelUp,
    OpenChest,
    Menu,
    Exit, //EXIT MATCH
    win,
}

[System.Serializable]
public class NavButtons
{
    public string name;
    public GameObject go;
}

public class MenuManager : Singleton<MenuManager>
{

    public MenuState currentMenuState = MenuState.PressToStart;
    public List<GameObject> menuList = new List<GameObject>();
    public List<NavButtons> buttonList = new List<NavButtons>();
    string lastState;
    public Animator m_animator;


    [SerializeField] float transitionDuration;

    [SerializeField] GameObject canvasGo;
    bool showMenu;

    private void Start()
    {
        //currentMenuState = MenuState.PressToStart;

        canvasGo.SetActive(true);
    }

    public void OnCancel(InputValue value)
    {
        if (IsRestrictedState(currentMenuState))
            return;

        if (currentMenuState == MenuState.InGame || currentMenuState == MenuState.Menu)
        {
            showMenu = !showMenu;

            if (showMenu)
            {
                Menu();
            }
            else
            {
                InGame();
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Back SFX");
            MainMenu();
        }
    }

    private bool IsRestrictedState(MenuState state)
    {
        return new HashSet<MenuState> { MenuState.Transition, MenuState.MainMenu, MenuState.PressToStart, MenuState.GameOver, MenuState.LevelUp, MenuState.OpenChest }.Contains(state);
    }

    #region Main Menu

    public void PressToStart()
    {
        if (currentMenuState == MenuState.Transition) return;

        StartCoroutine(BlackOut_Transition(MenuState.PressToStart));
    }

    public void MainMenu()
    {
        if (currentMenuState == MenuState.Transition) return;

        StartCoroutine(BlackOut_Transition(MenuState.MainMenu));
    }

    public void GameOver()
    {
        if (currentMenuState == MenuState.Transition) return;

        SetMenuState(MenuState.GameOver);
    }

    public void Victory()
    {
        if (currentMenuState == MenuState.Transition) return;

        SetMenuState(MenuState.win);
    }

    public void Options()
    {
        if (currentMenuState == MenuState.Transition) return;

        StartCoroutine(BlackOut_Transition(MenuState.Options));
    }

    public void PowerUp()
    {
        if (currentMenuState == MenuState.Transition) return;

        StartCoroutine(BlackOut_Transition(MenuState.PowerUp));
    }

    public void CharacterSelection()
    {
        if (currentMenuState == MenuState.Transition) return;

        StartCoroutine(BlackOut_Transition(MenuState.CharacterSelection));
    }

    public void Credits()
    {
        if (currentMenuState == MenuState.Transition) return;

        StartCoroutine(BlackOut_Transition(MenuState.Credits));
    }

    public void ExitGame()
    {
        if (currentMenuState == MenuState.Transition) return;

        StartCoroutine(BlackOut_Transition(MenuState.ExitGame));
    }


    /// Menu InGame


    public void InGame()
    {
        if (currentMenuState == MenuState.Transition) return;
        else if (GameManager.Instance.currentGameState == GameState.mainMenu) StartCoroutine(BlackOut_Transition(MenuState.InGame));
        else SetMenuState(MenuState.InGame);

    }

    public void Menu()
    {
        if (currentMenuState == MenuState.Transition) return;
        SetMenuState(MenuState.Menu);
    }

    public void LevelUp()
    {
        if (currentMenuState == MenuState.Transition) return;

        else if (currentMenuState == MenuState.InGame && !LevelUpManager.Instance.maxLevel)
        {
            SetMenuState(MenuState.LevelUp);
        }
    }

    public void OpenChest()
    {
        if (currentMenuState == MenuState.Transition) return;

        else if (currentMenuState == MenuState.InGame && !LevelUpManager.Instance.maxLevel)
        {
            SetMenuState(MenuState.OpenChest);
        }
    }

    public void Exit()
    {
        if (currentMenuState == MenuState.Transition) return;

        StartCoroutine(BlackOut_Transition(MenuState.Exit));
    }

    #endregion




    #region Transition

    IEnumerator BlackOut_Transition(MenuState newMenuState)
    {
        lastState = LastMenuState();

        currentMenuState = MenuState.Transition;


        m_animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionDuration);

        SetMenuState(newMenuState);
    }

    IEnumerator NewMenuState(MenuState newMenuState)
    {

        yield return new WaitForSeconds(transitionDuration);


        DeviceManager.Instance.ShowButton = false;

        this.currentMenuState = newMenuState;
    }

    #endregion


    #region Buttons

    public string LastMenuState()
    {
        lastState = string.Empty;

        if (currentMenuState == MenuState.Options)
        {
            lastState = "Option";
            return lastState;
        }
        else if (currentMenuState == MenuState.CharacterSelection)
        {
            lastState = "CharacterSelection";
            return lastState;
        }
        else if (currentMenuState == MenuState.PowerUp)
        {
            lastState = "PowerUp";
            return lastState;
        }
        else if (currentMenuState == MenuState.Credits)
        {
            lastState = "Credits";
            return lastState;
        }
        else
        {
            lastState = "StartGame";
            return lastState;
        }
    }

    public void ShowNavButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //FindObjectOfType<AudioManager>().Play("Back SFX");


        if (currentMenuState == MenuState.MainMenu)
        {
            if (lastState == "Option")
            {
                foreach (NavButtons button in buttonList)
                {
                    if (button.name == "Option")
                    {
                        EventSystem.current.SetSelectedGameObject(button.go);
                    }
                }
            }
            else if (lastState == "CharacterSelection")
            {
                foreach (NavButtons button in buttonList)
                {
                    if (button.name == "StartGame")
                    {
                        EventSystem.current.SetSelectedGameObject(button.go);
                    }
                }
            }
            else if (lastState == "PowerUp")
            {
                foreach (NavButtons button in buttonList)
                {
                    if (button.name == "PowerUp")
                    {
                        EventSystem.current.SetSelectedGameObject(button.go);
                    }
                }
            }
            else if (lastState == "Credits")
            {
                foreach (NavButtons button in buttonList)
                {
                    if (button.name == "Credits")
                    {
                        EventSystem.current.SetSelectedGameObject(button.go);
                    }
                }
            }
            else
            {
                foreach (NavButtons button in buttonList)
                {
                    if (button.name == "StartGame")
                    {
                        EventSystem.current.SetSelectedGameObject(button.go);
                    }
                }
            }

        }
        else if (currentMenuState == MenuState.CharacterSelection)
        {
            foreach (NavButtons button in buttonList)
            {
                if (button.name == "CharBox")
                {
                    EventSystem.current.SetSelectedGameObject(button.go);
                }
            }
        }
        else if (currentMenuState == MenuState.Options)
        {
            foreach (NavButtons button in buttonList)
            {
                if (button.name == "BackOption")
                {
                    EventSystem.current.SetSelectedGameObject(button.go);
                }
            }
        }
        else if (currentMenuState == MenuState.PowerUp)
        {
            foreach (NavButtons button in buttonList)
            {
                if (button.name == "BackPowerUp")
                {
                    EventSystem.current.SetSelectedGameObject(button.go);
                }
            }
        }
        else if (currentMenuState == MenuState.Credits)
        {
            foreach (NavButtons button in buttonList)
            {
                if (button.name == "BackCredits")
                {
                    EventSystem.current.SetSelectedGameObject(button.go);
                }
            }
        }
    }

    #endregion


    IEnumerator NextTheme(string theme)
    {
        yield return new WaitForSeconds(1);

        FindObjectOfType<AudioManager>().Play(theme);
    }



    //Menu State

    private void SetMenuState(MenuState newMenuState)
    {
        if (newMenuState == MenuState.PressToStart)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == MenuState.PressToStart.ToString())
                {
                    menu.SetActive(true);
                }
                else
                {
                    menu.SetActive(false);
                }
            }

            //FindObjectOfType<AudioManager>().Play("MainMenu Theme");
        }
        else if (newMenuState == MenuState.MainMenu)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == MenuState.MainMenu.ToString())
                {
                    menu.SetActive(true);
                }
                else if (menu.name == "GoldPanel".ToString())
                {
                    menu.SetActive(true);
                }
                else
                {
                    menu.SetActive(false);
                }
            }
        }
        else if (newMenuState == MenuState.Options)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == MenuState.Options.ToString())
                {
                    menu.SetActive(true);
                }
                else
                {
                    menu.SetActive(false);
                }

                if (menu.name == "GoldPanel".ToString())
                {
                    menu.SetActive(true);
                }
            }
        }
        else if (newMenuState == MenuState.CharacterSelection)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == MenuState.CharacterSelection.ToString())
                {
                    menu.SetActive(true);
                }
                else
                {
                    menu.SetActive(false);
                }

                if (menu.name == "GoldPanel".ToString())
                {
                    menu.SetActive(true);
                }
            }
        }
        else if (newMenuState == MenuState.PowerUp)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == MenuState.PowerUp.ToString())
                {
                    menu.SetActive(true);
                }
                else
                {
                    menu.SetActive(false);
                }

                if (menu.name == "GoldPanel".ToString())
                {
                    menu.SetActive(true);
                }
            }
        }
        else if (newMenuState == MenuState.Credits)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == MenuState.Credits.ToString())
                {
                    menu.SetActive(true);
                }
                else
                {
                    menu.SetActive(false);
                }

                if (menu.name == "GoldPanel".ToString())
                {
                    menu.SetActive(true);
                }
            }
        }
        else if (newMenuState == MenuState.ExitGame)
        {
            Application.Quit();

#if UNITY_EDITOR

            EditorApplication.isPlaying = false;
#endif

        }
        else if (newMenuState == MenuState.Menu)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == "Options")
                {
                    menu.SetActive(true);
                }
                if (menu.name == "Timer")
                {
                    menu.SetActive(false);
                }
            }

            GameManager.Instance.Menu();

            this.currentMenuState = newMenuState;

            return;

        }
        else if (newMenuState == MenuState.InGame)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == "Options")
                {
                    menu.SetActive(false);
                }
                if (menu.name == "Timer")
                {
                    menu.SetActive(true);
                }
                if (menu.name == "LevelUp")
                {
                    menu.SetActive(false);
                }
            }

            GameManager.Instance.InGame();

            this.currentMenuState = newMenuState;

            return;

        }
        else if (newMenuState == MenuState.Exit)
        {
            GameManager.Instance.MainMenu();
        }
        else if (newMenuState == MenuState.GameOver)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == "Game Over")
                {
                    menu.SetActive(true);
                }
                if (menu.name == "Timer")
                {
                    menu.SetActive(false);
                }
                if (menu.name == "Options")
                {
                    menu.SetActive(false);
                }
            }

            StartCoroutine(NextTheme("GameOver Theme"));

        }
        else if (newMenuState == MenuState.win)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == "Victory")
                {
                    menu.SetActive(true);
                }
                if (menu.name == "Timer")
                {
                    menu.SetActive(false);
                }
                if (menu.name == "Options")
                {
                    menu.SetActive(false);
                }
            }

            MusicFader musicFader = FindObjectOfType<MusicFader>();

            if (musicFader != null)
            {
                musicFader.FadeOut(.5f);
            }

            StartCoroutine(NextTheme("Victory Theme"));

        }
        else if (newMenuState == MenuState.LevelUp)
        {
            foreach (GameObject menu in menuList)
            {
                if (menu.name == "LevelUp")
                {
                    menu.SetActive(true);
                }
            }

            GameManager.Instance.Menu();

            this.currentMenuState = newMenuState;

            return;
        }
        else if (newMenuState == MenuState.OpenChest)
        {

            foreach (GameObject menu in menuList)
            {
                if (menu.name == "Randomized Chest")
                {
                    menu.SetActive(true);
                }
            }

            GameManager.Instance.Menu();

            this.currentMenuState = newMenuState;
        }

        //Maybe it's a good idea to turn this into a coroutine (I'll think about it)...

        StartCoroutine(NewMenuState(newMenuState));


    }
}