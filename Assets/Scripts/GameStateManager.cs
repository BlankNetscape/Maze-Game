using UnityEngine;

public enum GameState
{
    TestState,
    MazeSettings,
    MazeGeneration,
    MazeGenerated,
    MazeConfirmed,
    MazePlaying,
    MazeFinished,
}

public class GameStateManager : MonoBehaviour
{
    UIManager uiManager;
    GameManager gameManager;

    // Singleton instance of the GameStateManager
    public static GameStateManager Instance;

    // Current game state
    public GameState CurrentState { get; private set; }
    private GameStateScript CurrentStateSript { get; set; }

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

    private void SetGameStateScript(GameStateScript newScript)
    {
        if (CurrentStateSript != null) CurrentStateSript.Exit();
        CurrentStateSript = newScript;
        CurrentStateSript.Enter();
    }

    private void HandleGameStateChange(GameState newState)
    {
        // Test State
        if (newState == GameState.TestState)
        {
            Debug.Log("Game State Manager -> Handling Test event.");
        }

        // Maze Setup State
        if (newState == GameState.MazeSettings)
        {
            Debug.Log("Game State Manager -> Handling Maze Settings event.");
            SetGameStateScript(new MazeSetupScript(this));
        }

        // Maze Generating State
        if (newState == GameState.MazeGeneration)
        {
            Debug.Log("Game State Manager -> Handling Maze Generation event.");
            SetGameStateScript(new MazeGeneratingScript(this));
        }

        // Maze Generated State
        if (newState == GameState.MazeGenerated)
        {
            Debug.Log("Game State Manager -> Handling Maze Generated event.");
            SetGameStateScript(new MazeGeneratedScript(this));
        }

        // Maze Cinfirmed State
        if (newState == GameState.MazeConfirmed)
        {
            Debug.Log("Game State Manager -> Handling Maze Confirmed event.");
            SetGameStateScript(new MazeConfirmedScript(this));
        }

        // Level begun. Maze Play State
        if (newState == GameState.MazePlaying)
        {
            Debug.Log("Game State Manager -> Handling Maze Playing event.");
            SetGameStateScript(new MazePlayingScript(this));
        }

        // Level finished. Maze Finished State
        if (newState == GameState.MazeFinished)
        {
            Debug.Log("Game State Manager -> Handling Maze Finished event.");
            SetGameStateScript(new MazeFinishedScript(this));

        }
    }

    // State Classes
    private abstract class GameStateScript
    {
        public GameStateManager gameStateManager;

        public GameStateScript(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }

    private class MazeSetupScript : GameStateScript
    {
        public MazeSetupScript(GameStateManager gameStateManager) : base(gameStateManager)
        {
        }

        public override void Enter()
        {
            gameStateManager.uiManager.enableDimensionsDropdown();
            gameStateManager.uiManager.enableGenerateButton();
            gameStateManager.uiManager.disableConfirmButton();
            gameStateManager.gameManager.disablePlayers();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }

    private class MazeGeneratingScript : GameStateScript
    {
        public MazeGeneratingScript(GameStateManager gameStateManager) : base(gameStateManager)
        {
        }

        public override void Enter()
        {
            gameStateManager.uiManager.disableConfirmButton();
            gameStateManager.gameManager.disablePlayers();

            gameStateManager.gameManager.clearMaze();
            gameStateManager.gameManager.generateMaze();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }

    private class MazeGeneratedScript : GameStateScript
    {
        public MazeGeneratedScript(GameStateManager gameStateManager) : base(gameStateManager)
        {

        }

        public override void Enter()
        {
            gameStateManager.gameManager.bakeNavMesh();
            gameStateManager.uiManager.enableConfirmButton();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }

    private class MazeConfirmedScript : GameStateScript
    {
        public MazeConfirmedScript(GameStateManager gameStateManager) : base(gameStateManager)
        {
        }

        public override void Enter()
        {
            gameStateManager.gameManager.setupBot();


            gameStateManager.uiManager.disableDimensionsDropdown();
            gameStateManager.uiManager.disableGenerateButton();
            gameStateManager.uiManager.disableConfirmButton();
            gameStateManager.SetGameState(GameState.MazePlaying);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }

    private class MazePlayingScript : GameStateScript
    {
        public MazePlayingScript(GameStateManager gameStateManager) : base(gameStateManager)
        {
        }

        public override void Enter()
        {
            gameStateManager.gameManager.respawn();
            gameStateManager.gameManager.enablePlayers();

            gameStateManager.gameManager.startStopwatch();
            gameStateManager.gameManager.enablePlayerControls();
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
        }
    }

    private class MazeFinishedScript : GameStateScript
    {
        public MazeFinishedScript(GameStateManager gameStateManager) : base(gameStateManager)
        {
        }

        public override void Enter()
        {
            gameStateManager.gameManager.stopStopwatch();
            gameStateManager.gameManager.disablePlayerControls();

            gameStateManager.uiManager.enableDimensionsDropdown();
            gameStateManager.uiManager.enableGenerateButton();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}

