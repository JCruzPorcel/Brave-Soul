using System.Collections.Generic;
using UnityEngine;

public class WheelBot : Enemy
{
    public float normalSpeed;

    // Bullet settings
    public GameObject bulletPrefab;
    GameObject bulletContainer;
    public Transform gun;

    List<GameObject> bulletList = new List<GameObject>();
    Queue<GameObject> bulletQueue = new Queue<GameObject>();

    // Attack settings
    public float range;
    float currentRange;
    public float attackTime;
    float timer = 0f;


    public override void Spawn()
    {
        normalSpeed = speed;

        bulletContainer = GameObject.Find("Enemy Bullet Container");

        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(bulletPrefab, gun);
            go.transform.SetParent(bulletContainer.transform);
            bulletList.Add(go);
            bulletQueue.Enqueue(go);

            go.SetActive(false);
        }
    }

    public override void Attack()
    {
        gun.localPosition = sr.flipX ? new Vector3(-.52f, gun.localPosition.y, gun.localPosition.z) : new Vector3(.52f, gun.localPosition.y, gun.localPosition.z);

        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletQueue.Enqueue(bulletList[i]);
            }
        }

        currentRange = Vector2.Distance(player.position, transform.position);

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }


        if (currentRange <= range)
        {
            if (timer <= 0)
            {
                animator.SetBool("Shoot", true);
            }
            speed = 0f;
        }
        else
        {
            speed = normalSpeed;
        }

        animator.SetFloat("Speed", speed);
    }

    public void Shoot()
    {
        GameObject go = bulletQueue.Peek();

        go.transform.position = gun.position;

        go.SetActive(true);
        bulletQueue.Dequeue();

        animator.SetBool("Shoot", false);

        timer = attackTime;
    }
}
