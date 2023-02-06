using UnityEngine;

public class LevelUpManager : Singleton<LevelUpManager>
{
    [SerializeField] GameObject canvasLevelUp;

    public bool maxLevel = false;

    public int level_Necronomicon = -1;
    public int level_Axe = -1;
    public int level_Crossbow = -1;
    public int level_Arrow = -1;

    private void Update()
    {
        if (GameManager.Instance.currentGameState == GameState.gameOver)
        {
            canvasLevelUp.SetActive(false);
        }
    }

    public void WindowLevelState()
    {
        FindObjectOfType<AudioManager>().Play("Add Weapon SFX");

        if (PlayerController.Instance.pointsLvl <= 0)
        {
            canvasLevelUp.SetActive(false);
            PlayerController.Instance.pointsLvl = 0;
            MenuManager.Instance.InGame();
        }
        else
        {
            canvasLevelUp.SetActive(false);
            //Reset Weapons
            canvasLevelUp.SetActive(true);
        }
    }
}
