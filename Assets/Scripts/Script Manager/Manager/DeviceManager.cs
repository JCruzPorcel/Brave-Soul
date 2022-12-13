using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField] EventSystem eventSystem;
    [SerializeField] InputSystemUIInputModule inputSystemModule;

    private bool showButton;


    public bool ShowButton { get => showButton; set => showButton = value; }

    private void LateUpdate()
    {
        CurrentDevice();
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
