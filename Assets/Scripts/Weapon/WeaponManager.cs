using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    Transform player;
    [SerializeField] WeaponData weaponData;
    [SerializeField] GameObject weaponContainer;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        GameObject go = Instantiate(weaponData.Prefab, weaponContainer.transform);
        go.transform.position = new Vector3(player.position.x, player.position.y + .2f);
    }

     
}