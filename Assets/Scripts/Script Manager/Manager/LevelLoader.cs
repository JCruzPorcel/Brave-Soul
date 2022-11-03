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

    public List<MenuList> m_menuList = new List<MenuList>();
    private int m_currentMenu = 0;
    private int m_nextMenu = 2;

    private void Start()
    {
        m_menuList[0].menu.SetActive(true);
        m_menuList[1].menu.SetActive(true);
        for(int i = 2; i < m_menuList.Count; i++)
        {
            m_menuList[i].menu.SetActive(false);
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        m_animator.SetTrigger("Start");

        yield return new WaitForSeconds(m_transitionTime);

        SceneManager.LoadScene(levelIndex);     
    }

    public void CloseMenu(int currentMenu)
    {
        m_currentMenu = currentMenu;
    }

    public void OpenMenu(int nextMenu)
    {
        m_nextMenu = nextMenu;
        StartCoroutine(NextMenu());
    }

    IEnumerator NextMenu()
    {
        m_animator.SetTrigger("Start");

        yield return new WaitForSeconds(m_transitionTime);

        m_menuList[m_currentMenu].menu.SetActive(false);
        m_menuList[m_nextMenu].menu.SetActive(true);
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
