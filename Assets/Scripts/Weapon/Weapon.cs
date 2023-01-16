using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public int level;

    public float attackSpeed;
    [HideInInspector] public float timer;

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
