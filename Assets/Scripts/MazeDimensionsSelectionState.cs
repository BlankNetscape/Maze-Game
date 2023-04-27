using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDimensionsSelectionState : MonoBehaviour
{
    GameStateManager gameStateManager;
    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = transform.GetComponent<GameStateManager>();
    }


    public void ConfirmMazeDimensions(Vector2Int size)
    {
        // Call the MazeGenerator script to generate the maze with the selected dimensions
        //MazeGenerator mazeGenerator = FindObjectOfType<MazeGenerator>();
        //mazeGenerator.clearMaze();
        // NOTE: GenerateMaze will change game state to MazeGenerated
        //mazeGenerator.StartCoroutine(mazeGenerator.GenerateMaze(size));

        // Transition to the next game state
        //gameStateManager.SetGameState(GameState.MazeGeneration);
    }
}
