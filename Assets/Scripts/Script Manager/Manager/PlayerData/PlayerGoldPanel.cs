using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGoldPanel : Singleton<PlayerGoldPanel>
{
    [SerializeField] private TMP_Text playerGold_Text;


    private void Update()
    {
        playerGold_Text.text = GameManager.Instance.PlayerGold.ToString("n0");
    }
}
