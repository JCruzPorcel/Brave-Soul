using UnityEngine;

public class Boss : Enemy
{
    public override void Spawn()
    {
        is_a_Boss = true;
    }

    public override void Death()
    {
        
    }
}
