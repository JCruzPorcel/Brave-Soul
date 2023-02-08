using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    public Image arrowUI;
    public float distanceThreshold = 100f;
    private RectTransform arrowRectTransform;
    private RectTransform canvasRectTransform;
    private Vector2 onScreenPos;
    public float arrowRotation;

    void Start()
    {
        arrowRectTransform = arrowUI.GetComponent<RectTransform>();
        canvasRectTransform = arrowUI.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!IsChestOnScreen())
        {
            arrowUI.gameObject.SetActive(true);
            UpdateArrowDirection();
        }
        else
        {
            arrowUI.gameObject.SetActive(false);
        }
    }

    bool IsChestOnScreen()
    {
        Vector2 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return  screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
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
}
