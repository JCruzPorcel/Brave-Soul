using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : SingletonPersistent<LevelLoader>
{
    [System.Serializable]
    public class MenuList
    {
        public GameObject menu;
    }

    public Animator m_animator;
    [SerializeField] private float m_transitionTime;
    [SerializeField] private float m_TransiionToNewGameStateTime;

    public List<MenuList> m_menuList = new List<MenuList>();

    private string nextSceneName { get; set; }
    private int m_currentMenu { get; set; }
    private int m_nextMenu { get; set; }

    public string NextSceneName { get { return nextSceneName; } }
    public int m_CurrentMenu { get { return m_currentMenu; } }
    public int m_NextMenu { get { return m_nextMenu; } }

    private void Start()
    {
        nextSceneName = "MainMenu";
        m_nextMenu = 2;
        m_currentMenu = 0;

        m_menuList[0].menu.SetActive(true);
        m_menuList[1].menu.SetActive(true);

        for (int i = 2; i < m_menuList.Count; i++)
        {
            m_menuList[i].menu.SetActive(false);
        }
    }

    public void OpenURL(string link)
    {
        Application.OpenURL(link);
    }

    public void LoadNextLevel(string levelName)
    {
        if (GameManager.Instance.currentGameState != GameState.transition)
        {
            nextSceneName = levelName;
            StartCoroutine(LoadLevel(nextSceneName));
        }
    }

    IEnumerator LoadLevel(string levelName)
    {
        m_animator.SetTrigger("Start");

        yield return new WaitForSeconds(m_transitionTime);

        SceneManager.LoadScene(levelName);
    }

    public void CloseMenu(int currentMenu)
    {
        if (GameManager.Instance.currentGameState != GameState.transition)
        {
            m_currentMenu = currentMenu;
        }
    }

    public void OpenMenu(int nextMenu)
    {
        if (GameManager.Instance.currentGameState != GameState.transition)
        {
            m_nextMenu = nextMenu;

            StartCoroutine(NextMenu());
        }
    }

    IEnumerator NextMenu()
    {
        m_animator.SetTrigger("Start");

        yield return new WaitForSeconds(m_transitionTime);


        m_menuList[m_currentMenu].menu.SetActive(false);
        m_menuList[m_nextMenu].menu.SetActive(true);

        StartCoroutine(GameStateAfterTransition());

    }

    IEnumerator GameStateAfterTransition()
    {
        yield return new WaitForSeconds(m_transitionTime - m_TransiionToNewGameStateTime);

        if (NextSceneName == "InGame")
        {
            GameManager.Instance.InGame();
        }
        else if (NextSceneName == "MainMenu")
        {
            GameManager.Instance.MainMenu();
        }
    }

    public void ExitGame()
    {
            StartCoroutine(ClosingGame());
    }

    IEnumerator ClosingGame()
    {
        m_animator.SetTrigger("Start");

        yield return new WaitForSeconds(m_transitionTime);

        Application.Quit();

#if UNITY_EDITOR

        EditorApplication.isPlaying = false;
#endif

    }
}
