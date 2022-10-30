using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create New Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string varName;
    [Header("Stats")]
    [SerializeField] private string itemName;
    [TextArea(5, 5)]
    [SerializeField] private string description;
    [SerializeField] private int damage;
    [SerializeField] private int level;
    [SerializeField] private int amount;
    [SerializeField] private float attackSpeed;
    [SerializeField] private bool canUpgrade;
    [Space(10)]
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite weaponImage;
    [SerializeField] private ProjectileData projectileType;

    public string VarName { get { return varName; } }

    public int Amount { get { return amount; } }
    public int Damage { get { return damage; } }
    public int Level { get { return level; } }

    public float AttackSpeed { get { return attackSpeed; } }

    public bool CanUpgrade { get { return canUpgrade; } }

    public Sprite WeaponImage { get { return weaponImage; } }

    public ProjectileData ProjectileType { get { return projectileType; } }

    public GameObject Prefab { get { return prefab; } }


    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => itemName; set => itemName = value; }
}
