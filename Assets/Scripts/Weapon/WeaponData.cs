using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create New Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] private string varName;
    [SerializeField] private int Id;

    [SerializeField] private string itemName;
    [TextArea(5, 5)][SerializeField] private string description;
    [SerializeField] private bool canUpgrade;
    [Space(10)]
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite weaponImage;
    [SerializeField] private Sprite upgradeImage;


    public string VarName { get { return varName; } }
    public int ID { get { return Id; } }

    public bool CanUpgrade { get { return canUpgrade; } }

    public Sprite WeaponImage { get { return weaponImage; } }
    public Sprite UpgradeImage { get { return upgradeImage; } }

    public GameObject Prefab { get { return prefab; } }

    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => description; set => description = value; }
}
