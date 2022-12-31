using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform player;

    public int damage;
    public int armorPen;
    public float speed;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            transform.position += (transform.up * speed) * Time.fixedDeltaTime;
            DisableGO();
        }
    }

    void DisableGO()
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
            if (armorPen == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                armorPen--;
            }

            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
