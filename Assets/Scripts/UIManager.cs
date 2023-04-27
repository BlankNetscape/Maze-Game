using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Vector2Int mazeDimensions = new Vector2Int(0, 0);

    // Reference to UI items
    TMPro.TMP_Dropdown dimensionsDropdown;
    Button generateButton;
    Button confirmButton;

    // Reference to the Maze States
    MazeDimensionsSelectionState mazeDimensionsSelectionState;
    MazeConfirmedState mazeConfirmedState;

    private void Awake()
    {
        dimensionsDropdown = GameObject.Find("MazeDimensionsDropdown").GetComponent<TMPro.TMP_Dropdown>();
        generateButton = GameObject.Find("GenerateButton").GetComponent<Button>();
        confirmButton = GameObject.Find("ConfirmButton").GetComponent<Button>();

        mazeDimensionsSelectionState = GameObject.Find("Game State Manager").GetComponent<MazeDimensionsSelectionState>();
        mazeConfirmedState = GameObject.Find("Game State Manager").GetComponent<MazeConfirmedState>();
    }


    private void Start()
    {
        // Subscribe to the buttons click event
        generateButton.onClick.AddListener(OnGenerateButtonClick);
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
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
                mazeDimensions = new Vector2Int(2, 2);
                break;
        }

        // NOTE: Call the ConfirmMazeDimensions method of the MazeDimensionsSelectionState.
        GameObject.Find("Game State Manager").GetComponent<GameStateManager>().SetGameState(GameState.MazeGeneration);
    }

    private void OnConfirmButtonClick() {
        GameObject.Find("Game State Manager").GetComponent<GameStateManager>().SetGameState(GameState.MazeConfirmed);
    }

    public void disableGenerateButton() => generateButton.interactable = false;
    public void enableGenerateButton() => generateButton.interactable = true;

    public void disableConfirmButton() => confirmButton.interactable = false;
    public void enableConfirmButton() => confirmButton.interactable = true;

    public void disableDimensionsDropdown() => dimensionsDropdown.interactable = false;
    public void enableDimensionsDropdown() => dimensionsDropdown.interactable = true;

}
