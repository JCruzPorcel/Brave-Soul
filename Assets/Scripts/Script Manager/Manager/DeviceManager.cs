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

    GameObject keyboardButtonsMove;
    GameObject gamepadButtonsMove;

    public string keyboardButtons;
    public string gamepadButtons;

    private void LateUpdate()
    {
        CurrentDevice();

        if (GameManager.Instance.currentGameState == GameState.inGame)
        {
            if (keyboardButtonsMove == null || gamepadButtonsMove == null)
            {
                keyboardButtonsMove = GameObject.Find(keyboardButtons).gameObject;
                gamepadButtonsMove = GameObject.Find(gamepadButtons).gameObject;
            }
            else
            {
                if (currentDevice == DeviceType.gamepad)
                {
                    keyboardButtonsMove.SetActive(false);
                }
                else if (currentDevice == DeviceType.keyboard)
                {
                    gamepadButtonsMove.SetActive(false);
                }
            }            
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
