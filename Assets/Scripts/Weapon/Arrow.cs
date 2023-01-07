using UnityEngine;

public class Arrow : Projectile
{

    private void OnEnable()
    {
            FindObjectOfType<AudioManager>().Play("Arrow SFX");
    }
}
