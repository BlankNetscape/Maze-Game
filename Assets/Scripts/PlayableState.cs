using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableState : MonoBehaviour
{
    GameManager gameManager;
    GameStateManager gameStateManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
    }

    public void Enter()
    {
        gameStateManager.SetGameState(GameState.MazePlaying);
        gameManager.respawn();
        //TODO:
        gameManager.startStopwatch();
        gameManager.enablePlayerControls();
    }

    public void Finished()
    {
        gameManager.stopStopwatch();
        gameManager.disablePlayerControls();

        gameStateManager.SetGameState(GameState.MazeFinished);
    }

}
