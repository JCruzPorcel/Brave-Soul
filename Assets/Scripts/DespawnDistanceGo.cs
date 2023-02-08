using UnityEngine;

public class UIBottonDespawnDistance : MonoBehaviour
{
    public Transform player;
    [SerializeField] GameObject keyboardButtons;
    [SerializeField] GameObject gamepadButtons;


    private void Start()
    {
        keyboardButtons.transform.position = new Vector2(0f, -0.85f);
        gamepadButtons.transform.position = new Vector2(0f, -1.07f);
    }


    void Update()
    {
        Vector2 delta = transform.position - player.position;

        if (Mathf.Abs(delta.x) > 15 || Mathf.Abs(delta.y) > 12)
        {
            keyboardButtons.gameObject.SetActive(false);
            gamepadButtons.gameObject.SetActive(false);
        }
        else
        {
            keyboardButtons.gameObject.SetActive(true);
            gamepadButtons.gameObject.SetActive(true);
        }
    }
}
