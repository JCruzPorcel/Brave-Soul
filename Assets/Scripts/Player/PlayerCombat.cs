using UnityEngine;

public class PlayerCombat : Singleton<PlayerCombat>
{
    [SerializeField] WeaponData startWeapon;
    [SerializeField] SliderBar sliderBar;
    float timer;

    private void Update()
    {
        sliderBar.MaxAttackSpeed(startWeapon.AttackSpeed);

        
        if (timer >= startWeapon.AttackSpeed)
        {
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

        sliderBar.NextAttack(timer);
    }
}
