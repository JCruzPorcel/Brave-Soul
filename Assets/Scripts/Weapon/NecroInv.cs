using UnityEngine;

public class NecroInv : MonoBehaviour
{

    int damage;
    int level;
    bool active = false;
    BoxCollider2D box;
    SpriteRenderer sr;

    private void Start()
    {
        WeaponLevel();
        sr = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        WeaponLevel();

        sr.enabled = active;
        box.enabled = active;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    public void WeaponLevel()
    {
        level = LevelUpManager.Instance.level_Necronomicon;

        switch (level)
        {
            case -1:
                damage = 10;
                active = false;
                break;

            case 0:
                damage = 10;
                active = false;
                break;
            case 1:
                damage = 12;
                active = false;
                break;
            case 2:
                damage = 15;
                active = false;
                break;

            case 3:
                damage = 20;
                active = false;
                break;

            case 4:
                damage = 20;
                active = true;
                break;

            case 5:
                damage = 25;
                active = true;
                break;
        }
    }
}
