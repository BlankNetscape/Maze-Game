using UnityEngine;

public class MazeNode : MonoBehaviour
{
    [SerializeField] GameObject[] walls;
    [SerializeField] MeshRenderer floor;

    [SerializeField] public Color primaryColor;
    [SerializeField] public Color secondaryColor;

    protected NodeType nodeType = NodeType.Floor;

    [ExecuteAlways]
    private void Start()
    {
        floor.material.color = primaryColor;
    }

    // Sets the Floor color according to Node state
    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.Available:
                floor.material.color = Color.white;
                break;
            case NodeState.Current:
                floor.material.color = Color.yellow;
                break;
            case NodeState.Completed:
                floor.material.color = Color.blue;
                break;
        }
    }

    // Sets initial type of the node by MazeGenerator
    public void SetType(NodeType type)
    // NOTE: Tags will be used for fast locating by GameMangaer
    {
        if (type == NodeType.Finish)
        {
            tag = GameTags.Finish;
            nodeType = type;
            SetFloorColor(primaryColor);

            //BoxCollider bc = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
            //bc.size = new Vector3(.9f, .9f, .9f);
            //bc.isTrigger = true;
        }
        else if (type == NodeType.Respawn)
        {
            tag = GameTags.Respawn;
            nodeType = type;
            SetFloorColor(primaryColor);
        }
        else
        {
            // Default case
            tag = GameTags.Untagged;
            nodeType = NodeType.Floor;
            //SetFloorColor(Color.white);
        }
    }

    public void SetFloorColor(Color c)
    {
        floor.material.color = c;
    }

    public void RemoveWall(Dirrections wallDirrection)
    {
        /** 
         *  Walls objects should be imported to the list as following:
         *  [0] X pos   -> RIGHT 
         *  [1] X neg   -> LEFT
         *  [2] Z pos   -> FORWARD 
         *  [3] Z neg   -> BACKWARD
         */
        int wallIndex = (int)wallDirrection - 1;
        walls[wallIndex].gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (nodeType != NodeType.Finish) return;

        Debug.Log("Collided with finish!");
        Debug.Log($"{collider.name} win!");
        GameObject.Find("Game State Manager").GetComponent<GameStateManager>().SetGameState(GameState.MazeFinished);
    }
}


//public class MazeStartNode : MazeNode
//{
//    [SerializeField] GameManager flag;
//}

//public class MazeFinishNode : MazeNode
//{
//    private void OnTriggerEnter(Collider collider)
//    {
//        if (nodeType != NodeType.Finish) return;

//        Debug.Log("Collided with finish!");
//        Debug.Log($"{collider.name} win!");
//        GameObject.Find("Game State Manager").GetComponent<GameStateManager>().SetGameState(GameState.MazeFinished);
//    }
//}

//public class MazeSimpleNode : MazeNode
//{

//}