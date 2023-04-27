using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Stopwatch))]
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject uiManagerHolder;
    [SerializeField] GameObject gameStateManagerHolder;
    [SerializeField] GameObject mazeGeneratorHolder;
    [SerializeField] GameObject player;

    private UIManager uiManager;
    private GameStateManager gameStateManager;
    private MazeGenerator mazeGenerator;
    private Stopwatch stopwatch;

    private Transform stopwathTextHolder;

    [SerializeField] private bool isDebug = true;
    private bool keepUpdateStopwatchText = false;

    private delegate void action();

    private void Start()
    {
        uiManager = uiManagerHolder.GetComponent<UIManager>();
        gameStateManager = gameStateManagerHolder.GetComponent<GameStateManager>();
        stopwathTextHolder = uiManagerHolder.transform.Find("StopwatchText");
        mazeGenerator = mazeGeneratorHolder.GetComponent<MazeGenerator>();

        stopwatch = transform.GetComponent<Stopwatch>();

    }

    public void clearMaze()
    {
        Debug.Log("Clearing..");
        mazeGenerator.clearMaze();
    }

    public void generateMaze()
    {
        Debug.Log("Generating...");
        var a = new Action(()=> { 
            gameStateManager.SetGameState(GameState.MazeGenerated); 
        });
        // Start Generate Maze Coroutine and Change State to Generated after generating finished
        StartCoroutine(mazeGenerator.GenerateMaze(uiManager.mazeDimensions, a));
    }


    // Move Players to start position.
    public void respawn()
    {
        Transform startNode = mazeGenerator.transform.GetChild(0);
        disablePlayers();
        player.transform.position = new Vector3(startNode.position.x, startNode.position.y + 1, startNode.position.z);
        enablePlayers();
    }

    // Make Players Invisible.
    public void disablePlayers()
    {
        player.transform.GetComponent<Renderer>().enabled = false;
        // NOTE: Fixes bug -> Player keeps triggering finish.
        player.transform.position = new Vector3(100, 100, 100);
    }

    // Make Players visible.
    public void enablePlayers()
    {
        player.transform.GetComponent<Renderer>().enabled = true;
    }

    // TODO: Rename methods: enablePlayerControls, disablePlayerControls to allowMovement or smt alike.
    public void enablePlayerControls() {
        player.GetComponent<Move>().isMovable = true;
    }
    public void disablePlayerControls() {
        player.GetComponent<Move>().isMovable = false;
    }

    // Starts Stopwatch coroutine & Update display text.
    public void startStopwatch() {
        stopwatch.StartStopwatch();
        keepUpdateStopwatchText = true;
        StartCoroutine(updateStopwatchText());
    }

    // Stops Stopwatch related coroutines.
    public void stopStopwatch() {
        stopwatch.StopStopwatch();
        keepUpdateStopwatchText = false;
        StopCoroutine(updateStopwatchText());
    }

    // Updates onscreen stopwatch each second
    private IEnumerator updateStopwatchText() {
        while (keepUpdateStopwatchText) 
        {
            string elapsedTime = stopwatch.GetElapsedTimeString();
            stopwathTextHolder.transform.GetComponent<TMPro.TextMeshProUGUI>().text = elapsedTime;
            if (isDebug) Debug.Log($"Stopwatch: {elapsedTime}");
            yield return new WaitForSeconds(1);
        }
    }
}

