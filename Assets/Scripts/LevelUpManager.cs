using UnityEngine;

public class LevelUpManager : Singleton<LevelUpManager>
{
    [SerializeField] GameObject canvasLevelUp;
    public bool maxLevel = false;

    private void Update()
    {
        Debug.Log(maxLevel);
    }

    public void WindowLevelState()
    {
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
