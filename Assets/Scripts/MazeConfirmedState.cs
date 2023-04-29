using UnityEngine;

public class MazeConfirmedState : MonoBehaviour
{
    GameObject player;
    MazeGenerator mazeGenerator;
    GameStateManager gameStateManager;

    private void Start()
    {
        player = GameObject.Find("Player");
        mazeGenerator = GameObject.Find("Maze Generator").GetComponent<MazeGenerator>();
        gameStateManager = transform.GetComponent<GameStateManager>();
    }

    public void ConfirmMaze()
    {
        gameStateManager.SetGameState(GameState.MazeConfirmed);
    }
}
