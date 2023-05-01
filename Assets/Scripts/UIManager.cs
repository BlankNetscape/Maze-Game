using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public Vector2Int mazeDimensions = new Vector2Int(0, 0);
    [HideInInspector] public float playerSpeed;
    [HideInInspector] public float botSpeedMod;
    // TODO: public userInput speed & mod

    // Reference to UI items
    TMPro.TMP_Dropdown dimensionsDropdown;
    TMPro.TMP_Dropdown playerSpeedDropdown;
    TMPro.TMP_Dropdown botSpeedModDropdown;
    Button generateButton;
    Button confirmButton;
    Button exitButton;

    // Reference to the Maze States
    MazeDimensionsSelectionState mazeDimensionsSelectionState;
    MazeConfirmedState mazeConfirmedState;

    private void Awake()
    {
        dimensionsDropdown = GameObject.Find("MazeDimensionsDropdown").GetComponent<TMPro.TMP_Dropdown>();
        playerSpeedDropdown = GameObject.Find("PlayerSpeedDropdown").GetComponent<TMPro.TMP_Dropdown>();
        botSpeedModDropdown = GameObject.Find("BotSpeedModDropdown").GetComponent<TMPro.TMP_Dropdown>();
        generateButton = GameObject.Find("GenerateButton").GetComponent<Button>();
        confirmButton = GameObject.Find("ConfirmButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        mazeDimensionsSelectionState = GameObject.Find("Game State Manager").GetComponent<MazeDimensionsSelectionState>();
        mazeConfirmedState = GameObject.Find("Game State Manager").GetComponent<MazeConfirmedState>();
    }


    private void Start()
    {
        // Subscribe to the buttons click event
        generateButton.onClick.AddListener(OnGenerateButtonClick);
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnGenerateButtonClick()
    {
        // Set maze dimensions
        switch (dimensionsDropdown.value)
        {
            case 0:
                mazeDimensions = new Vector2Int(5, 5);
                break;
            case 1:
                mazeDimensions = new Vector2Int(10, 10);
                break;
            case 2:
                mazeDimensions = new Vector2Int(15, 15);
                break;
            case 3:
                mazeDimensions = new Vector2Int(20, 20);
                break;
            default:
                mazeDimensions = new Vector2Int(2,2); // Default 2x2 maze size
                break;
        }

        // NOTE: Call the ConfirmMazeDimensions method of the MazeDimensionsSelectionState.
        GameObject.Find("Game State Manager").GetComponent<GameStateManager>().SetGameState(GameState.MazeGeneration);
    }

    private void OnConfirmButtonClick()
    {
        switch (playerSpeedDropdown.value)
        {
            case 0:
                playerSpeed = SpeedValues.NORMAL;
                break;
            case 1:
                playerSpeed = SpeedValues.SLOW;
                break;
            case 2:
                playerSpeed = SpeedValues.FAST;
                break;
            default:
                playerSpeed = SpeedValues.SLOW;
                break;
        }

        switch (botSpeedModDropdown.value)
        {
            case 0:
                botSpeedMod = BotSpeedModValues.FAST;
                break;
            case 1:
                botSpeedMod = BotSpeedModValues.NORMAL;
                break;
            case 2:
                botSpeedMod = BotSpeedModValues.SLOW;
                break;
            default:
                botSpeedMod = BotSpeedModValues.SLOW;
                break;
        }

        GameObject.Find("Game State Manager").GetComponent<GameStateManager>().SetGameState(GameState.MazeConfirmed);
    }

    public void disableGenerateButton() => generateButton.interactable = false;
    public void enableGenerateButton() => generateButton.interactable = true;

    public void disableConfirmButton() => confirmButton.interactable = false;
    public void enableConfirmButton() => confirmButton.interactable = true;

    public void disableDimensionsDropdown() => dimensionsDropdown.interactable = false;
    public void enableDimensionsDropdown() => dimensionsDropdown.interactable = true;

    public void disableDropdowns()
    {
        dimensionsDropdown.interactable = false;
        botSpeedModDropdown.interactable = false;
        playerSpeedDropdown.interactable = false;
    }

    public void enableDropdowns()
    {
        dimensionsDropdown.interactable = true;
        botSpeedModDropdown.interactable = true;
        playerSpeedDropdown.interactable = true;
    }
}
