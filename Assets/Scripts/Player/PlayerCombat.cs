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
        charData = PersistentManager.Instance.CharSelected;
        Instantiate(charData.CharPrefab, playerContainer);

        weaponData = PersistentManager.Instance.CharSelected.StartWeapon;
        GameObject go = Instantiate(weaponData.Prefab, playerContainer);
        go.transform.position = new Vector3(playerContainer.position.x, playerContainer.position.y + .2f);

        if (weaponData.VarName == "Crossbow")
        {
            crossbow = weaponData.Prefab.GetComponent<Crossbow>();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.inGame) return;

        if (!PlayerController.Instance.IsDead)
        {
            sliderBar.MaxAttackSpeed(weaponData.AttackSpeed);

            if (weaponData.VarName == "Crossbow")
            {
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
}
