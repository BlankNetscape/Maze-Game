using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stopwatch))]
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject uiManagerHolder;
    [SerializeField] GameObject gameStateManagerHolder;
    [SerializeField] GameObject mazeGeneratorHolder;
    [SerializeField] GameObject player;

    private UIManager uiManager;
    private GameStateManager gameStatemanager;
    private MazeGenerator mazeGenerator;
    private Stopwatch stopwatch;

    private Transform stopwathTextHolder;

    [SerializeField] private bool isDebug = true;
    private bool keepUpdateStopwatchText = false;

    private void Start()
    {
        uiManager = uiManagerHolder.GetComponent<UIManager>();
        gameStatemanager = gameStateManagerHolder.GetComponent<GameStateManager>();
        stopwathTextHolder = uiManagerHolder.transform.Find("StopwatchText");
        mazeGenerator = mazeGeneratorHolder.GetComponent<MazeGenerator>();

        stopwatch = transform.GetComponent<Stopwatch>();

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

