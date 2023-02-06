using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;


public enum DeviceType
{
    keyboard,
    gamepad
}

public class DeviceManager : SingletonPersistent<DeviceManager>
{
    public DeviceType currentDevice = DeviceType.keyboard;
    public PlayerInput playerInput;
    [SerializeField] InputSystemUIInputModule inputSystemModule;

    public InputSystemUIInputModule InputSystemModule { get => inputSystemModule; set => inputSystemModule = value; }

    private bool showButton;

    public bool ShowButton { get => showButton; set => showButton = value; }

    public GameObject keyboardButtonsMove;
    public GameObject gamepadButtonsMove;

    private void LateUpdate()
    {
        CurrentDevice();

        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            if (keyboardButtonsMove == null || gamepadButtonsMove == null)
            {
                keyboardButtonsMove = GameObject.Find("WASD Buttons (PC)").gameObject;
                gamepadButtonsMove = GameObject.Find("PadButtons (Joystick)").gameObject;
            }

            keyboardButtonsMove.SetActive(!showButton);
            gamepadButtonsMove.SetActive(showButton);
        }
    }

    public void CurrentDevice()
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            if (showButton) return;

            MenuManager.Instance.ShowNavButton();

            SetDevice(DeviceType.gamepad);

            showButton = true;
        }
        else if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            if (!showButton) return;

            SetDevice(DeviceType.keyboard);

            showButton = false;
        }
    }

    private void SetDevice(DeviceType newDevice)
    {
        if (currentDevice == newDevice)
            return;

        if (newDevice == DeviceType.keyboard)
        {
            Cursor.lockState = CursorLockMode.None;

            inputSystemModule.deselectOnBackgroundClick = true;
        }
        if (newDevice == DeviceType.gamepad)
        {
            Cursor.lockState = CursorLockMode.Locked;

            inputSystemModule.deselectOnBackgroundClick = false;
        }

        this.currentDevice = newDevice;
    }

}
