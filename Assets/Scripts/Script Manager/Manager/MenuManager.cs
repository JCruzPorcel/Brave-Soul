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

    [SerializeField] float transitionDuration;

    public void PressToStart()
    {
        StartCoroutine(Transition());
        SetMenuState(MenuState.pressToStart);
    }

    public void MainMenu()
    {
        StartCoroutine(Transition());
        SetMenuState(MenuState.mainMenu);
    }

    public void StartGame()
    {
        StartCoroutine(Transition());
        SetMenuState(MenuState.startGame);
    }


    private void SetMenuState(MenuState newMenuState)
    {
        foreach (MenuList menu in menuList)
        {
            if(menu.name == newMenuState.ToString())
            {
                menu.go.SetActive(true);
            }
            else
            {
                menu.go.SetActive(false);
            }
        }



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

    IEnumerator Transition()
    {
        Debug.Log("espera");
        yield return new WaitForSeconds(transitionDuration);
        Debug.Log("listo");

    }
}
