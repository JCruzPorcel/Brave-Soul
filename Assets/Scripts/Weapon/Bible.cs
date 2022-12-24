using UnityEngine;


public class Bible : Weapon
{

    float currentTime;

    int currentBibles;
    [SerializeField] int maxBibles;

    public override void Attack()
    {
        transform.Rotate(0, 0, 40 * Time.deltaTime);
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, -1), 40 * Time.deltaTime);
    }

    public override void Spawn()
    {
        //transform.position = new Vector2(player.position.x, transform.position.y + 1);        

    }

}
