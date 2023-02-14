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


    private void Update()
    {
        CurrentDevice();
    }

    public void OnSelectWeaponLeft(InputValue value)
    {
        if (MenuManager.Instance.currentMenuState != MenuState.LevelUp) return;

        WeaponManager.Instance.SelectWeaponLeft();
    }

    public void OnSelectWeaponMid(InputValue value)
    {
        if (MenuManager.Instance.currentMenuState != MenuState.LevelUp) return;

        WeaponManager.Instance.SelectWeaponMid();
    }

    public void OnSelectWeaponRight(InputValue value)
    {
        if (MenuManager.Instance.currentMenuState != MenuState.LevelUp) return;

        WeaponManager.Instance.SelectWeaponRight();
    }


    private void CurrentDevice()
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            if (showButton) return;


            showButton = true;

            MenuManager.Instance.ShowNavButton();

            SetDevice(DeviceType.gamepad);
        }
        else if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            if (!showButton) return;

            showButton = false;

            SetDevice(DeviceType.keyboard);
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
