using UnityEngine;

public class BoomerangWeapon : Weapon
{
    public GameObject boomerangPrefab;
    public float boomerangSpeed;
    public float maxDistance;

    private GameObject boomerangInstance;
    private Vector3 initialPos;

    public override void Spawn()
    {
        boomerangInstance = Instantiate(boomerangPrefab, player.position, Quaternion.identity);
        initialPos = boomerangInstance.transform.position;
    }

    public override void Attack()
    {
        boomerangInstance.transform.position += boomerangInstance.transform.right * boomerangSpeed * Time.deltaTime;

        if (Vector3.Distance(boomerangInstance.transform.position, initialPos) >= maxDistance)
        {
            boomerangInstance.transform.right = (player.position - boomerangInstance.transform.position).normalized;
        }
    }

    public override void Level()
    {
        damage += 2;
        level++;
    }
}
