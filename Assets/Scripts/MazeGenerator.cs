using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeNode nodePrefab;

    [SerializeField] MazeNode startNodePrefab;
    [SerializeField] MazeNode finishNodePrefab;
    [SerializeField] MazeNode simpleNodePrefab;

    //private void Start()
    //{
    //    StartCoroutine(GenerateMaze(new Vector2Int(3,3), ()=> { }));
    //}

    public void clearMaze()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    public IEnumerator GenerateMaze(Vector2Int size, Action action)
    {
        List<MazeNode> nodes = new List<MazeNode>();


        // Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f));
                MazeNode newNode;
                if (x == 0 && y == 0)
                {
                    newNode = Instantiate(startNodePrefab, nodePos, Quaternion.identity, transform);

                } else if (x == size.x-1 && y == size.y-1)
                {
                    newNode = Instantiate(finishNodePrefab, nodePos, Quaternion.identity, transform);

                } else
                {
                    newNode = Instantiate(simpleNodePrefab, nodePos, Quaternion.identity, transform);
                }
                nodes.Add(newNode);
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // Choose starting node
        currentPath.Add(nodes[UnityEngine.Random.Range(0, nodes.Count)]);
        currentPath[0].SetState(NodeState.Current);

        while (completedNodes.Count < nodes.Count)
        {
            // Check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                // Check node to the right of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if (currentNodeX > 0)
            {
                // Check node to the left of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if (currentNodeY < size.y - 1)
            {
                // Check node above the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if (currentNodeY > 0)
            {
                // Check node below the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // Choose next node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = UnityEngine.Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(Dirrections.LEFT);
                        currentPath[currentPath.Count - 1].RemoveWall(Dirrections.RIGHT);
                        break;
                    case 2:
                        chosenNode.RemoveWall(Dirrections.RIGHT);
                        currentPath[currentPath.Count - 1].RemoveWall(Dirrections.LEFT);
                        break;
                    case 3:
                        chosenNode.RemoveWall(Dirrections.BACKWARD);
                        currentPath[currentPath.Count - 1].RemoveWall(Dirrections.FORWARD);
                        break;
                    case 4:
                        chosenNode.RemoveWall(Dirrections.FORWARD);
                        currentPath[currentPath.Count - 1].RemoveWall(Dirrections.BACKWARD);
                        break;
                }

                currentPath.Add(chosenNode);
                chosenNode.SetState(NodeState.Current);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);
                currentPath[currentPath.Count - 1].SetState(NodeState.Completed);
                currentPath.RemoveAt(currentPath.Count - 1);
                yield return new WaitForSeconds(0.01f);
            }
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            MazeNode node = nodes[i];
            if (i == 0) node.SetType(NodeType.Respawn);
            else if (i == nodes.Count - 1) node.SetType(NodeType.Finish);
            else
            {
                int foo = size.y % 2 != 0 ? 0 : (int)Mathf.Floor(i / size.y);
                node.SetType(NodeType.Floor);
                node.SetFloorColor((i + foo) % 2 == 0 ? node.primaryColor : node.secondaryColor);
            }
        }

        // Should change game state to Generated
        action();
        yield break;


    }
}
