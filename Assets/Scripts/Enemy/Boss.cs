using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] float minAttackRange = 1f;

    private void Start()
    {
        is_a_Boss = true;
    }


    private void Update()
    {
        if (isDead)
        {
            //Debug.Log("Spawn chest");
        }
    }

    public override void Attack()
    {

        float distance = Vector2.Distance(player.position, transform.position);


        if (distance < minAttackRange)
        {
            Debug.Log("Attack");
            animator.SetTrigger("Attack");
        }
    }


    private void OnBecameVisible()
    {
        //Debug.Log("print arrow look at chest");
    }

    private void OnBecameInvisible()
    {
        //Debug.Log("print chest");
    }
}
