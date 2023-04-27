using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UIManager uiManager;
    GameStateManager gameStatemanager;
    GameObject player;
    MazeGenerator mazeGenerator;
    Stopwatch stopwatch;
    [SerializeField] GameObject stopwathText;

    bool keepLogLoop = false;

    private void Start()
    {
        gameStatemanager = transform.GetComponent<GameStateManager>();
        //gameStatemanager.SetGameState(GameState.MazeDimensionsSelection);

        uiManager = GameObject.Find("ControllCanvas").GetComponent<UIManager>();

        player = GameObject.Find("Player");

        mazeGenerator = GameObject.Find("Maze Generator").GetComponent<MazeGenerator>();

        stopwatch = transform.GetComponent<Stopwatch>();
    }


    public void respawn()
    {
        Transform startNode = mazeGenerator.transform.GetChild(0);
        disablePlayers();
        player.transform.position = new Vector3(startNode.position.x, startNode.position.y + 1, startNode.position.z);
        enablePlayers();
    }

    public void disablePlayers()
    {
        player.transform.GetComponent<Renderer>().enabled = false;
        // Fixes bug -> Player keeps triggering finish.
        player.transform.position = new Vector3(100, 100, 100);
    }

    public void enablePlayers()
    {
        player.transform.GetComponent<Renderer>().enabled = true;
    }

    public void enablePlayerControls() {
        player.GetComponent<Move>().isMovable = true;
    }
    public void disablePlayerControls() {
        player.GetComponent<Move>().isMovable = false;
    }

    public void startStopwatch() {
        stopwatch.StartStopwatch();
        keepLogLoop = true;
        StartCoroutine(logStopwatch());
    }
    public void stopStopwatch() {
        stopwatch.StopStopwatch();
        keepLogLoop = false;
        StopCoroutine(logStopwatch());
    }
    private void displayStopwatch() {
        
    }

    private IEnumerator logStopwatch()
    {
        while (keepLogLoop)
        {
            stopwathText.transform.GetComponent<TMPro.TextMeshProUGUI>().text = stopwatch.GetElapsedTimeString();
            Debug.Log($"Stopwatch: {stopwatch.GetElapsedTimeString()}");
            yield return new WaitForSeconds(1f);
        }
    }
}

