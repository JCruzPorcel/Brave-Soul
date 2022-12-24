using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string m_name;


    [Space(10)][TextArea(5, 5)] public string description;


    public int damage;
    public int level;

    public float attackSpeed;

    public bool canUpgrade;

    public Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Spawn();
    }

    private void Update()
    {
        Attack();
    }

    public virtual void Spawn()
    {

    }


    public virtual void Attack()
    {
        
    }



    public virtual void Level()
    {
        switch (level)
        {
            case 0:

                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;

            default: break;
        }
    }
}
