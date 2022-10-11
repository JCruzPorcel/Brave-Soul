using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    Transform player;
    [SerializeField] WeaponData weaponData;
    [SerializeField] GameObject weaponContainer;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject go = Instantiate(weaponData.Prefab, weaponContainer.transform);
            go.transform.position = player.position;
        }
    }
}