using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public int level;

    public float attackSpeed;
    [HideInInspector] public float timer;

    [TextArea(6, 6)] public string desc_lvl_0;
    [TextArea(6, 6)] public string desc_lvl_1;
    [TextArea(6, 6)] public string desc_lvl_2;
    [TextArea(6, 6)] public string desc_lvl_3;
    [TextArea(6, 6)] public string desc_lvl_4;
    [TextArea(6, 6)] public string desc_lvl_5;

    public string[] texts;

    public WeaponData weaponData;

    public Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Spawn();
        timer = attackSpeed;
        WeaponLevel();
    }

    public virtual void Spawn() { }

    public virtual void Attack() { }

    public virtual void Level() { }

    public virtual void WeaponLevel() { }

}
