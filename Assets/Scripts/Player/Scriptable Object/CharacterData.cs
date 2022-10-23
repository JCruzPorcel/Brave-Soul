using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Create New Character")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string varName;
    [SerializeField] private string charName;
    [TextArea(5,6)]
    [SerializeField] private string description;
    [SerializeField] private int armor;
    [SerializeField] private int damage;
    [SerializeField] private int MaxHp;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float hpRegen;
    [SerializeField] private float speed;
    [SerializeField] private Sprite image;
    [SerializeField] private WeaponData startWeapon;
    [SerializeField] private GameObject prefab;




    /* DELETE THIS BEFORE USE
    Transform player;
    [SerializeField] WeaponData weaponData;
    [SerializeField] GameObject weaponContainer;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        GameObject go = Instantiate(weaponData.Prefab, weaponContainer.transform);
        go.transform.position = new Vector3(player.position.x, player.position.y + .2f);
    }*/
}
