using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    [SerializeField] Image arrowUI;
    [SerializeField] float distanceThreshold = 100f;
    private RectTransform arrowRectTransform;
    private RectTransform canvasRectTransform;
    private Vector2 onScreenPos;
    [SerializeField] float arrowRotation;
    Transform player;
    SpriteRenderer sr;
    [SerializeField] GameObject chest;

    void Start()
    {
        arrowRectTransform = arrowUI.GetComponent<RectTransform>();
        canvasRectTransform = arrowUI.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        player = GameObject.FindWithTag("Player").transform;

        sr = chest.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (transform.position.y < player.position.y + .03f)
        {
            sr.sortingOrder = 1;
        }
        else if (transform.position.y > player.position.y - .03f)
        {
            sr.sortingOrder = -1;
        }

        if (!IsChestOnScreen())
        {
            arrowUI.gameObject.SetActive(true);
            chest.SetActive(false);
            UpdateArrowDirection();
        }
        else
        {
            arrowUI.gameObject.SetActive(false);
            chest.SetActive(true);
        }
    }

    bool IsChestOnScreen()
    {
        Vector2 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    void UpdateArrowDirection()
    {
        onScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        onScreenPos.x = Mathf.Clamp(onScreenPos.x, distanceThreshold, canvasRectTransform.rect.width - distanceThreshold);
        onScreenPos.y = Mathf.Clamp(onScreenPos.y, distanceThreshold, canvasRectTransform.rect.height - distanceThreshold);
        arrowRectTransform.anchoredPosition = onScreenPos - canvasRectTransform.sizeDelta / 2f;

        Vector2 direction = (Vector2)(onScreenPos - canvasRectTransform.sizeDelta / 2f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - -arrowRotation;
        arrowRectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            Destroy(this.gameObject);
            MenuManager.Instance.OpenChest();
        }
    }
}
