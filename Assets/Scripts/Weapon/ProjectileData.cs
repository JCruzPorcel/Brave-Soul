using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Create New Projectile")]
public class ProjectileData : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] private string itemName;
    [TextArea(5, 5)]
    [SerializeField] private string description;

    [SerializeField] private int amount;
    [SerializeField] private bool canUpgrade;
    [Space(10)]
    [SerializeField] private GameObject prefab;
    [SerializeField] private WeaponData weapon;
    [SerializeField] private Sprite image;

    public int Amount { get { return amount; } }

    public bool CanUpgrade { get { return canUpgrade; } }

    public Sprite Image { get { return image; } }
    public Sprite WeaponUpgradeImage { get { return WeaponUpgradeImage; } }

    public GameObject Prefab { get { return prefab; } }

    public WeaponData Weapon { get { return weapon; } }


    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => description; set => description = value; }

}
