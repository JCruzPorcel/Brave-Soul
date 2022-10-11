using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create New Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int hp;
    public int damage;
    public float speed;
    public int exp;
    [Space(35)] public GameObject prefab;
}
