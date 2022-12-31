using UnityEngine;

public class PlayerCombat : Singleton<PlayerCombat>
{
    [SerializeField] Transform playerContainer;
    [SerializeField] public CharacterData charData;
    public SliderBar sliderBar;

    private void Start()
    {
        charData = GameManager.Instance.CharSelected;
        Instantiate(charData.CharPrefab, playerContainer);
    }
}