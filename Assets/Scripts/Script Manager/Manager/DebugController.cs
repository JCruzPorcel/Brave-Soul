using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    bool showHelp;

    string input;

    public static DebugCommand<string> SET_MODE;
    public static DebugCommand<int> SET_GOLD;
    public static DebugCommand<int> SET_SPEED;
    public static DebugCommand HELP;

    public List<object> commandList;

    GameManager gameManager;

    public void OnToggleDebug(InputValue value)
    {
        showConsole = !showConsole;
    }

    public void OnSubmit(InputValue value)
    {
        if (showConsole)
        {
            HandleInput();
            input = string.Empty;
        }
    }

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        SET_MODE = new DebugCommand<string>("set_mode", "Sets game mode to god mode or normal.", "set_mode <game_mode>", (x) =>
        {
            gameManager.GameMode(x);
        });

        SET_GOLD = new DebugCommand<int>("set_gold", "Sets the amount of gold.", "set_gold <gold_amount>", (x) =>
        {
            gameManager.PlayerGold = x;
            SaveManager.SavePlayerData(gameManager);
        });

        SET_SPEED = new DebugCommand<int>("set_speed", "Sets the speed of the player.", "set_speed <speed_amount>", (x) =>
        {
            if (gameManager.currentGameState == GameState.inGame || gameManager.currentGameState == GameState.menu)
            {
                PlayerController.Instance.playerSpeed = x;
            }
        });

        HELP = new DebugCommand("help", "Shows a list of commands.", "help", () =>
        {
            showHelp = !showHelp;
        });



        commandList = new List<object>
        {
            SET_MODE,
            SET_GOLD,
            SET_SPEED,
            HELP,
        };
    }

    private void Update()
    {
        if (gameManager.currentGameState != GameState.inGame) return;


        if (showConsole)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    Vector2 scroll;

    private void OnGUI()
    {
        if (!showConsole) { return; }

        float y = 0f;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);


            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += 100;

        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }


    private void HandleInput()
    {

        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }else if (commandList[i] as DebugCommand<string> != null)
                {
                    (commandList[i] as DebugCommand<string>).Invoke(properties[1]);
                }
            }
        }
    }
}
