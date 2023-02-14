
using UnityEngine;

public class Boss : Enemy
{
    public GameObject m_Chest;
    public bool isSpawned;

    public override void Spawn()
    {
        is_a_Boss = true;
    }

    public override void Death()
    {
        if (!isSpawned)
        {
            GameObject go = Instantiate(m_Chest);
            go.transform.position = this.transform.position;
            isSpawned = true;
        }
    }
}
