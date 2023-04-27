using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerationState : MonoBehaviour
{
    GameStateManager gameStateManager;
    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = transform.GetComponent<GameStateManager>();
    }


}
