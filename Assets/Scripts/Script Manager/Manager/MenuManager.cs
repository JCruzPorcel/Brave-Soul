using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    public bool showOptions;
    public GameObject m_Menu;



    public void MainMenu()
    {
        LevelLoader.Instance.ResetScene();
    }
}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
pressToStart,
mainMenu,
startGame,
powerUp,
credits,
options
}

[System.Serializable]
public class MenuList
{
public string name;
public GameObject go;
}


public class MenuManager : Singleton<MenuManager>
{

public MenuState currentMenuState = MenuState.pressToStart;
public List<MenuList> menuList = new List<MenuList>();

Animator m_animator;

[SerializeField] float transitionDuration;

private void Start()
{
    ClearWindows(currentMenuState);
}

public void PressToStart()
{
    StartCoroutine(c_PressToStart());
}

public void MainMenu()
{
    StartCoroutine(c_MainMenu());
}

public void StartGame()
{
    StartCoroutine(c_StartGame());
}






IEnumerator c_PressToStart()
{
    m_animator.SetTrigger("Start");

    yield return new WaitForSeconds(transitionDuration);

    SetMenuState(MenuState.pressToStart);
}

IEnumerator c_MainMenu()
{
    m_animator.SetTrigger("Start");

    yield return new WaitForSeconds(transitionDuration);

    SetMenuState(MenuState.mainMenu);
}

IEnumerator c_StartGame()
{
    m_animator.SetTrigger("Start");

    yield return new WaitForSeconds(transitionDuration);

    SetMenuState(MenuState.startGame);
}




public void ClearWindows(MenuState newMenuState)
{
    foreach (MenuList menu in menuList)
    {
        if (menu.name == newMenuState.ToString())
        {
            menu.go.SetActive(true);
        }
        else
        {
            menu.go.SetActive(false);
        }
    }
}

private void SetMenuState(MenuState newMenuState)
{

    ClearWindows(newMenuState);


    if (newMenuState == MenuState.pressToStart)
    {

    }
    else if (newMenuState == MenuState.mainMenu)
    {

    }
    else if (newMenuState == MenuState.startGame)
    {

    }

    this.currentMenuState = newMenuState;
}
}*/