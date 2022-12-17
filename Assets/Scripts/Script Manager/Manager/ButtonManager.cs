using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void Back()
    {
        MenuManager.Instance.MainMenu();
    }

    public void Options()
    {
        MenuManager.Instance.Options();
    }

    public void ExitGame()
    {
        MenuManager.Instance.ExitGame();
    }

    public void CharacterSelection()
    {
        MenuManager.Instance.CharacterSelection();
    }

    public void PowerUp()
    {
        MenuManager.Instance.PowerUp();
    }

    public void Credits()
    {
        MenuManager.Instance.Credits();
    }


}
