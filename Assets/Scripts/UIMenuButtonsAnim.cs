using UnityEngine;

public class UIMenuButtonsAnim : MonoBehaviour
{
    [SerializeField] GameObject UIButtons;

    ChangeAnimatorLayer animatorLayer;

    public string m_KeyboardController_Anim;
    public string m_GamepadController_Anim;
    public bool modifyOffset;
    public Vector2 offsetKeyboard;
    public Vector2 offsetGamepad;

    RectTransform rectTransform;

    private void Start()
    {
        animatorLayer = GetComponent<ChangeAnimatorLayer>();
        rectTransform = UIButtons.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (DeviceManager.Instance.currentDevice == DeviceType.keyboard)
        {
            animatorLayer.ChangeAnimation(m_KeyboardController_Anim);

            if (modifyOffset)
                rectTransform.sizeDelta = offsetKeyboard;

        }
        else if (DeviceManager.Instance.currentDevice == DeviceType.gamepad)
        {
            animatorLayer.ChangeAnimation(m_GamepadController_Anim);

            if (modifyOffset)
                rectTransform.sizeDelta = offsetGamepad;
        }
    }
}
