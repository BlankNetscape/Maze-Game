﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Stopwatch))]
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject uiManagerHolder;
    [SerializeField] GameObject gameStateManagerHolder;
    [SerializeField] GameObject mazeGeneratorHolder;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject botPrefab;
    private GameObject player;
    private GameObject bot;

    private NavMeshAgent agent;
    private UIManager uiManager;
    private GameStateManager gameStateManager;
    private MazeGenerator mazeGenerator;
    private Stopwatch stopwatch;

    private Transform stopwathTextHolder;

    [SerializeField] private bool isDebug = true;
    private bool keepUpdateStopwatchText = false;

    private delegate void action();

    private void Awake()
    {
        mazeGenerator = mazeGeneratorHolder.GetComponent<MazeGenerator>();

        uiManager = uiManagerHolder.GetComponent<UIManager>();
        gameStateManager = gameStateManagerHolder.GetComponent<GameStateManager>();
        stopwathTextHolder = uiManagerHolder.transform.Find("StopwatchText");

        stopwatch = transform.GetComponent<Stopwatch>();
    }

    private void Start()
    {
        
    }


    public void clearMaze()
    {
        Debug.Log("Clearing..");
        mazeGenerator.clearMaze();
    }

    public void generateMaze()
    {
        Debug.Log("Generating...");
        var a = new Action(() =>
        {
            gameStateManager.SetGameState(GameState.MazeGenerated);
        });
        // Start Generate Maze Coroutine and Change State to Generated after generating finished
        StartCoroutine(mazeGenerator.GenerateMaze(uiManager.mazeDimensions, a));
    }


    // Move Players to start position.
    public void respawn()
    {
        Transform startNode = mazeGenerator.transform.GetChild(0);

        // Respan Player
        player.transform.position = new Vector3(startNode.position.x, startNode.position.y + .1f, startNode.position.z);
    }



    // Make Players Invisible.
    public void disablePlayers()
    {
        if (player != null)
        {
            player.transform.GetComponent<Renderer>().enabled = false;
            // NOTE: Fixes bug -> Player keeps triggering finish.
            player.transform.position = new Vector3(100, 100, 100);
        }


        if (bot != null)
        {
            Destroy(bot); 
            bot = null;
        }
    }

    // Make Players visible.
    public void enablePlayers()
    {
        player.transform.GetComponent<Renderer>().enabled = true;
        bot.transform.GetComponent<Renderer>().enabled = true;
    }

    // TODO: Rename methods: enablePlayerControls, disablePlayerControls to allowMovement or smt alike.
    public void enablePlayerControls()
    {
        player.GetComponent<Move>().isMovable = true;
        agent.isStopped = false;
    }
    public void disablePlayerControls()
    {
        player.GetComponent<Move>().isMovable = false;
        agent.isStopped = true;
    }

    // Starts Stopwatch coroutine & Update display text.
    public void startStopwatch()
    {
        stopwatch.StartStopwatch();
        keepUpdateStopwatchText = true;
        StartCoroutine(updateStopwatchText());
    }

    // Stops Stopwatch related coroutines.
    public void stopStopwatch()
    {
        stopwatch.StopStopwatch();
        keepUpdateStopwatchText = false;
        StopCoroutine(updateStopwatchText());
    }

    // Updates onscreen stopwatch each second
    private IEnumerator updateStopwatchText()
    {
        while (keepUpdateStopwatchText)
        {
            string elapsedTime = stopwatch.GetElapsedTimeString();
            stopwathTextHolder.transform.GetComponent<TMPro.TextMeshProUGUI>().text = elapsedTime;
            if (isDebug) Debug.Log($"Stopwatch: {elapsedTime}");
            yield return new WaitForSeconds(1);
        }
    }

    public void bakeNavMesh()
    {
        if (mazeGeneratorHolder.GetComponent<NavMeshSurface>() == null) mazeGeneratorHolder.AddComponent<NavMeshSurface>();
        var surface = mazeGeneratorHolder.GetComponent<NavMeshSurface>();

        surface.BuildNavMesh();
    }

    public void initBot()
    {
        if (bot != null) return;

        Transform startNode = mazeGenerator.transform.GetChild(0);
        bot = Instantiate(botPrefab, startNode.position ,Quaternion.identity, transform);
        agent = bot.AddComponent<NavMeshAgent>();
    }

    public void initPlayer()
    {
        if (player != null) return;

        Transform startNode = mazeGenerator.transform.GetChild(0);
        player = Instantiate(playerPrefab, startNode.position, Quaternion.identity, transform);
        player.transform.GetComponent<Renderer>().enabled = false;
    }

    public void setupBot()
    {
        if (bot == null) return;

        agent.speed = uiManager.playerSpeed * uiManager.botSpeedMod;

        agent.destination = mazeGenerator.transform.GetChild(mazeGenerator.transform.childCount-1).position;
        agent.isStopped = true;
    }

    public void setupPlayer()
    {
        player.GetComponent<Move>().playerSpeed = uiManager.playerSpeed;
    }
}

