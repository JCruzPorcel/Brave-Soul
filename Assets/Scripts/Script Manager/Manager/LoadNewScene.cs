using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    [SerializeField] private string m_Scene;

    public void NewScene()
    {
        SceneManager.LoadScene(m_Scene);
    }
}
