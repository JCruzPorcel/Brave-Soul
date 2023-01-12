using TMPro;
using UnityEngine;

public class PlayerScore : Singleton<PlayerScore>
{
    public int enemiesKilled = 0;

    [SerializeField] TMP_Text enemiesKilled_Text;

    void Update()
    {
        enemiesKilled_Text.text = string.Format("{0}", enemiesKilled);
    }
}
