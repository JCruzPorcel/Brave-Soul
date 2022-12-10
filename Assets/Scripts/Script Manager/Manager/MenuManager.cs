using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : SingletonPersistent<MenuManager>
{
    bool showOptions;

    [SerializeField] GameObject m_Menu;
    [SerializeField] GameObject m_MenuOptions;


    private void Update()
    {

        if (GameManager.Instance.currentGameState == GameState.inGame || GameManager.Instance.currentGameState == GameState.menu)
        {
            if (m_Menu == null)
            {
                m_Menu = GameObject.Find("Canvas Menu");
            }

            if (showOptions)
            {

                GameManager.Instance.Menu();
            }
            else
            {

                GameManager.Instance.InGame();
            }



        }

    }


    public void OnPause(InputValue value)
    {
        if (GameManager.Instance.currentGameState == GameState.inGame || GameManager.Instance.currentGameState == GameState.menu)
        {
            showOptions = !showOptions;

            m_MenuOptions.SetActive(showOptions);
        }
    }
}
