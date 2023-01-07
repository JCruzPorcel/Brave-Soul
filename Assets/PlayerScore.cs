using TMPro;
using UnityEngine;

public class PlayerScore : Singleton<PlayerScore>
{
    public int enemiesKilled = 0;
    public string score_string = string.Empty;

    [SerializeField] TMP_Text enemiesKilled_Text;

    void Update()
    {
        enemiesKilled_Text.text = score_string + ": " + enemiesKilled.ToString();
    }
}
