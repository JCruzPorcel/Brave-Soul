using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    public Slider experienceSlider;

    public PlayerController playerController;

    void Start()
    {
        experienceSlider.maxValue = playerController.nextLvl;
    }

    void Update()
    {
        experienceSlider.maxValue = playerController.nextLvl;
        experienceSlider.value = playerController.currentExp;
    }
}
