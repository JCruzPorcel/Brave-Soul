using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform player;

    public int damage;
    public int enemyPen;
    public float speed;
    public int level;

    [TextArea(6, 6)] public string desc_lvl_0;
    [TextArea(6, 6)] public string desc_lvl_1;
    [TextArea(6, 6)] public string desc_lvl_2;
    [TextArea(6, 6)] public string desc_lvl_3;
    [TextArea(6, 6)] public string desc_lvl_4;
    [TextArea(6, 6)] public string desc_lvl_5;

    public WeaponData arrowData;

    private void Start()
    {
        player = GetComponent<Transform>();
        WeaponLevel();
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            transform.position += (transform.up * speed) * Time.fixedDeltaTime;
            DespawnDistance();
            WeaponLevel();
        }
    }

    void DespawnDistance()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) > 12 || Mathf.Abs(transform.position.y - player.position.y) > 10)
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

    public virtual void WeaponLevel()
    {
        level = LevelUpManager.Instance.level_Arrow;

        switch (level)
        {
            case 0:
                damage = 5;
                speed = 7f;
                enemyPen = 0;
                arrowData.Description = desc_lvl_0;
                break;

            case 1:
                damage = 6;
                speed = 8f;
                enemyPen = 0;
                arrowData.Description = desc_lvl_1;
                break;

            case 2:
                damage = 15;
                speed = 12f;
                enemyPen = 1;
                arrowData.Description = desc_lvl_2;
                break;

            case 3:
                damage = 35;
                speed = 25f;
                enemyPen = 2;
                arrowData.Description = desc_lvl_3;
                break;

            case 4:
                damage = 40;
                speed = 32f;
                enemyPen = 3;
                arrowData.Description = desc_lvl_4;
                break;

            case 5:
                damage = 50;
                speed = 40f;
                enemyPen = 5;
                arrowData.Description = desc_lvl_5;
                break;

            default:
                break;
        }
    }

}
