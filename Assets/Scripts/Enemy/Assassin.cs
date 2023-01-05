using UnityEngine;

public class Assassin : Enemy
{
    public override void GiveExp()
    {
        PlayerController.Instance.TakeExp(exp);
    }
}
