using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeFinishNode : MazeNode
{
    private void OnTriggerEnter(Collider collider)
    {
        if (nodeType != NodeType.Finish) return;

        Debug.Log("Collided with finish!");
        Debug.Log($"{collider.name} win!");
        GameObject.Find("Game State Manager").GetComponent<GameStateManager>().SetGameState(GameState.MazeFinished);
    }
}
