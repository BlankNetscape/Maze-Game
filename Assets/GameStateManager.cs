using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    TestState,
    MazeSettings,
    MazeGeneration,
    MazeGenerated,
    MazeConfirmed,
    MazePlaying,
    MazeFinished
}

public class GameStateManager : MonoBehaviour
{
    UIManager uiManager;
    GameManager gameManager;

    // Singleton instance of the GameStateManager
    public static GameStateManager Instance;

    // Current game state
    public GameState CurrentState { get; private set; }

    // Event triggered when the game state changes
    public delegate void OnGameStateChangeHandler(GameState newState);
    public event OnGameStateChangeHandler OnGameStateChange;

    private void Awake()
    {
        // Singleton pattern: make sure there's only one instance of the GameStateManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        uiManager = GameObject.Find("ControllCanvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


        OnGameStateChange += HandleGameStateChange;

        
        SetGameState(GameState.MazeSettings);
    }


    public void SetGameState(GameState newState)
    {
        if (CurrentState != newState)
        {
            CurrentState = newState;
            OnGameStateChange?.Invoke(newState);
        }
    }

    private void HandleGameStateChange(GameState newState)
    {

        if (newState == GameState.TestState)
        {
            Debug.Log("Game State Manager -> Handling Test event.");
        }

        if (newState == GameState.MazeSettings)
        {
            Debug.Log("Game State Manager -> Handling Maze Settings event.");
            uiManager.enableDimensionsDropdown();
            uiManager.enableGenerateButton();
            uiManager.disableConfirmButton();
            gameManager.disablePlayers();
        }

        if (newState == GameState.MazeGeneration)
        {
            Debug.Log("Game State Manager -> Handling Maze Generation event.");
            uiManager.disableConfirmButton();
            gameManager.disablePlayers();
        }

        if (newState == GameState.MazeGenerated)
        {
            Debug.Log("Game State Manager -> Handling Maze Generated event.");
            uiManager.enableConfirmButton();
        }

        if (newState == GameState.MazeConfirmed)
        {
            Debug.Log("Game State Manager -> Handling Maze Confirmed event.");
            uiManager.disableDimensionsDropdown();
            uiManager.disableGenerateButton();
            uiManager.disableConfirmButton();
            transform.GetComponent<PlayableState>().Enter();
        }

        if (newState == GameState.MazePlaying)
        {
            Debug.Log("Game State Manager -> Handling Maze Playing event.");
        }

        if (newState == GameState.MazeFinished)
        {
            Debug.Log("Game State Manager -> Handling Maze Finished event.");
            uiManager.enableDimensionsDropdown();
            uiManager.enableGenerateButton();
            uiManager.disableConfirmButton();
        }
    }



}
