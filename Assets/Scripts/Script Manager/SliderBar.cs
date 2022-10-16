using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider attackSpeedSlider;
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
        if (playerController.FacingRight)
        {
            hpSlider.transform.position = player.position + new Vector3(offset.x, offset.y, 0f);
            attackSpeedSlider.transform.position = player.position + new Vector3(offset.x, offset.y - .088f, 0f);

        }
        else
        {
            hpSlider.transform.position = player.position + new Vector3(-offset.x, offset.y, 0f);
            attackSpeedSlider.transform.position = player.position + new Vector3(-offset.x, offset.y - .088f, 0f);
        }
    }

    public void SetMaxtHealth(int maxHealth)
    {
        hpSlider.maxValue = maxHealth;
        hpSlider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        hpSlider.value = health;
    }

    public void MaxAttackSpeed(float maxAttackSpeed)
    {
        attackSpeedSlider.maxValue = maxAttackSpeed;
        attackSpeedSlider.value = 0;
    }

    public void NextAttack(float currentTime)
    {
        attackSpeedSlider.value = currentTime;
    }
}
