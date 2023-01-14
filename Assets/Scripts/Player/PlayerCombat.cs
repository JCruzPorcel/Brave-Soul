using UnityEngine;

public class PlayerCombat : Singleton<PlayerCombat>
{
    [SerializeField] Transform playerContainer;
    [SerializeField] public CharacterData charData;
    public SliderBar sliderBar;

    private void Start()
    {
        charData = GameManager.Instance.CharSelected;
        Instantiate(charData.CharPrefab, playerContainer);
        
        WeaponData weaponData = GameManager.Instance.CharSelected.StartWeapon;

        if (weaponData.VarName == "Axe")
        {
            LevelUpManager.Instance.level_Axe = 1;
        }
        if (weaponData.VarName == "Arrow")
        {
            LevelUpManager.Instance.level_Arrow = 1;
        }
        if (weaponData.VarName == "Crossbow")
        {
            LevelUpManager.Instance.level_Crossbow = 1;
        }
        if (weaponData.VarName == "Necronomicon")
        {
            LevelUpManager.Instance.level_Necronomicon = 1;
        }
    }
}