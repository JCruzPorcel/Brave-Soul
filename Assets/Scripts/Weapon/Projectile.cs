using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform player;

    public int damage;
    public int enemyPen;
    public float speed;
    public int level;

    public WeaponData weaponData;

    public string[] texts;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        WeaponLevel();
    }

    private void Update()
    {
        level = LevelUpManager.Instance.level_Arrow;
        DespawnDistance();
        WeaponLevel();
    }
    
    private void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            transform.position += (transform.up * speed) * Time.fixedDeltaTime;
        }
    }

    void DespawnDistance()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) > 15 || Mathf.Abs(transform.position.y - player.position.y) > 12)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        WeaponLevel();
    }

    private void OnDisable()
    {
        WeaponLevel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (enemyPen == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                enemyPen--;
            }

            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void WeaponLevel()
    {
        level = LevelUpManager.Instance.level_Arrow;
        texts = LanguageManager.Instance.texts;

        weaponData.ItemName = texts[weaponData.ID];


        if (level <= -1)
        {
            weaponData.Description = texts[weaponData.ID + 1];
        }
        else if (level <= 5)
        {
            weaponData.Description = texts[weaponData.ID + level + 2];
        }

        switch (level)
        {
            case -1:
                damage = 5;
                speed = 7f;
                enemyPen = 0;
                break;

            case 0:
                damage = 7;
                speed = 9f;
                enemyPen = 0;
                break;

            case 1:
                damage = 15;
                speed = 15f;
                enemyPen = 0;
                break;

            case 2:
                damage = 17;
                speed = 20f;
                enemyPen = 0;
                break;

            case 3:
                damage = 25;
                speed = 20f;
                enemyPen = 1;
                break;

            case 4:
                damage = 35; 
                speed = 32f;
                enemyPen = 2;
                break;

            case 5:
                damage = 40;
                speed = 40f;
                enemyPen = 3;
                break;
        }
    }

}
