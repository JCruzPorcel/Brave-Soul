using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void Back()
    {
        MenuManager.Instance.MainMenu(); 
        FindObjectOfType<AudioManager>().Play("ClickButton SFX");
    }

    public void Options()
    {
        MenuManager.Instance.Options();
        FindObjectOfType<AudioManager>().Play("ClickButton SFX");
    }

    public void ExitGame()
    {
        MenuManager.Instance.ExitGame();
        FindObjectOfType<AudioManager>().Play("ClickButton SFX");
    }

    public void CharacterSelection()
    {
        MenuManager.Instance.CharacterSelection();
        FindObjectOfType<AudioManager>().Play("ClickButton SFX");
    }

    public void PowerUp()
    {
        MenuManager.Instance.PowerUp();
        FindObjectOfType<AudioManager>().Play("ClickButton SFX");
    }

    public void Credits()
    {
        MenuManager.Instance.Credits();
        FindObjectOfType<AudioManager>().Play("ClickButton SFX");
    }
}
