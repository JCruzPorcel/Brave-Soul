using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pressToStart;
    [SerializeField] private GameObject m_MainMenu;

    private void Awake()
    {
        m_MainMenu.SetActive(false);
        pressToStart.SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            pressToStart.SetActive(false);
            m_MainMenu.SetActive(true);
        }
    }
}
