using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    Menu,
    Exit, //EXIT MATCH
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
        SetMenuState(MenuState.PressToStart);
        canvasGo.SetActive(true);
    }

    public void OnCancel(InputValue value)
    {
        if (currentMenuState == MenuState.Transition || currentMenuState == MenuState.MainMenu || currentMenuState == MenuState.PressToStart) return;

        else if (currentMenuState == MenuState.InGame || currentMenuState == MenuState.Menu) Menu();

        else MainMenu();
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



    /// <summary>
    /// Menu InGame
    /// </summary>

    public void InGame()
    {
        if (currentMenuState == MenuState.Transition) return;

        SetMenuState(MenuState.InGame);
    }

    public void Menu()
    {
        if (currentMenuState == MenuState.Transition) return;

        showMenu = !showMenu;

        if (showMenu)
            SetMenuState(MenuState.Menu);
        else
            InGame();
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

        this.currentMenuState = newMenuState;

        DeviceManager.Instance.ShowButton = false;
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
        }
        else if (newMenuState == MenuState.MainMenu)
        {

            foreach (GameObject menu in menuList)
            {
                if (menu.name == MenuState.MainMenu.ToString())
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
                if (menu.name == "Pause Menu")
                {
                    menu.SetActive(showMenu);
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
                if (menu.name == "Pause Menu")
                {
                    menu.SetActive(showMenu);
                }
            }

            GameManager.Instance.InGame();

            this.currentMenuState = newMenuState;

            return;

        }
        else if (newMenuState == MenuState.Exit)
        {
            SceneManager.LoadScene("MainMenu");
        }

        //Maybe it's a good idea to turn this into a coroutine (I'll think about it)...

        StartCoroutine(NewMenuState(newMenuState));

    }
}