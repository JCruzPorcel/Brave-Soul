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
    [SerializeField] private Sprite image;
    [SerializeField] private ProjectileData projectileType;
    private float timer;


    public string VarName { get { return varName; } }

    public ProjectileData ProjectileType { get { return projectileType; } }

    public GameObject Prefab { get { return prefab; } }


    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => itemName; set => itemName = value; }

    public int Amount { get => amount; set => amount = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Level { get => level; set => level = value; }

    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float Timer { get => timer; set => timer = value; }

    public bool CanUpgrade { get => canUpgrade; set => canUpgrade = value; }

    public Sprite Image { get => image; set => image = value; }

}
