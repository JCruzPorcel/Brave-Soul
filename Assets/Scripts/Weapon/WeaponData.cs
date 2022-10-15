using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create New Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string varName;
    [Header("Stats")]
    [SerializeField] private string itemName;
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int level;
    [TextArea(5, 5)]
    [SerializeField] private string description;
    [Space(10)]
    [SerializeField] private int amount;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite image;
    [Header("Weapon Evolve")]
    [SerializeField] private bool canEvolve;
    [SerializeField] private string evName;
    [SerializeField] private string evDescription;
    [SerializeField] private Sprite evImage;
    [SerializeField] private ProjectileData projectileType;


    public string VarName { get { return varName; } }
    public string ItemName { get { return itemName; } }
    public string Description { get { return description; } }
    public string EvDescription { get { return evDescription; } }
    public string EvName { get { return evName; } }

    public int Amount { get { return amount; } }
    public int Damage { get { return damage; } }
    public int Level { get { return level; } }

    public float AttackSpeed { get { return attackSpeed; } }

    public bool CanEvolve { get { return canEvolve; } }

    public GameObject Prefab { get { return prefab; } }

    public Sprite Image { get { return image; } }
    public Sprite EvImage { get { return evImage; } }
    public ProjectileData ProjectileType { get { return projectileType; } }

}
