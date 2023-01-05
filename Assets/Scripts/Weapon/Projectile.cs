using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform player;

    public int damage;
    public int enemyPen;
    public float speed;
    public int level;

    public WeaponData arrowData;

    private void Start()
    {
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
        level = GetStatsManager.Instance.level_Arrow;

        switch (level)
        {
            case 1:
                damage = 5;
                speed = 7f;
                enemyPen = 0;
                break;

            case 2:
                damage = 15;
                speed = 12f;
                enemyPen = 1;
                break;

            case 3:
                damage = 35;
                speed = 25f;
                enemyPen = 2;
                break;

            case 4:
                damage = 40;
                speed = 32f;
                enemyPen = 3;
                break;

            case 5:
                damage = 50;
                speed = 50f;
                enemyPen = 5;
                break;
        }
    }

}
