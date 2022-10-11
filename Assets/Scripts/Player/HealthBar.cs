using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Vector3 offset;
    Transform player;
    PlayerController playerController;


    private void Start()
    {
        player = GameObject.Find("Player").transform;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        //slider.transform.position = Camera.main.WorldToScreenPoint(playerTransform.position + offset);

        if (playerController.FacingRight)
        {
            slider.transform.position = player.position + new Vector3(offset.x, offset.y, 0f);

        }
        else
        {
            slider.transform.position = player.position + new Vector3(-offset.x, offset.y, 0f);
        }

    }

    public void SeMaxtHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

}
