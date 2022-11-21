using UnityEngine;
using TMPro;

public class PlayerData : SingletonPersistent<PlayerData>
{
    private int currentGold = 0;
    [SerializeField] private TMP_Text currentGold_Text;

    public int CurrentGold { get => currentGold; set => currentGold = value; }



    private void Update()
    {
        currentGold_Text.text = currentGold.ToString("n0");

        if (Input.GetKey(KeyCode.E))
        {
            currentGold += 150;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            currentGold -= 150;
        }

       
    }


}