using UnityEngine;

public class LevelUpManager : Singleton<LevelUpManager>
{
    [SerializeField] GameObject canvasLevelUp;

    void Update()
    {
        
        if (PlayerController.Instance.pointsLvl <= 0)
        {
            canvasLevelUp.SetActive(false);
            PlayerController.Instance.pointsLvl = 0;
            MenuManager.Instance.InGame();
        }
    }
}
