using UnityEngine;

public class PlayerCombat : Singleton<PlayerCombat>
{
    float timer;
    [SerializeField] Transform playerContainer;
    [SerializeField] public CharacterData charData;
    [SerializeField] public WeaponData weaponData;
    [SerializeField] Crossbow crossbow;
    [SerializeField] SliderBar sliderBar;

    private void Start()
    {
        charData = GameManager.Instance.CharSelected;
        Instantiate(charData.CharPrefab, playerContainer); 
        weaponData = GameManager.Instance.CharSelected.StartWeapon;
    }

    private void Update()
    {
        sliderBar.MaxAttackSpeed(weaponData.AttackSpeed);

        if (weaponData.VarName == "Crossbow")
        {
            crossbow = weaponData.Prefab.GetComponent<Crossbow>();
            if (crossbow.MostNearbyEnemies() != null)
            {
                if (timer >= weaponData.AttackSpeed)
                {
                    timer = 0;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }

            if (crossbow != null)
                return;

            crossbow = weaponData.Prefab.GetComponent<Crossbow>();
        }
        else
        {
            if (timer >= weaponData.AttackSpeed)
            {
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        sliderBar.NextAttack(timer);
    }
}
