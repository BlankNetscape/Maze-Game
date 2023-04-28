using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshExplorer : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    private float STEP = 1;
    private float TOL = 0.1f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(ExploreMaze());
    }

    IEnumerator ExploreMaze()
    {
        List<Vector3> visitedNodes = new List<Vector3>();
        Queue<Vector3> queue = new Queue<Vector3>();
        queue.Enqueue(transform.position);

        visitedNodes.Add(transform.position);

        int c = 0;

        //while (queue.Count > 0 && queue.Count < 100000)
        while (queue.Count > 0)
            {
            c++;

            Vector3 currentNode = queue.Dequeue();

            // Check if we have found the exit
            var dist = Vector3.Distance(currentNode, target.position);
            Debug.Log($"Dist to target -> {dist}");



            if (dist <= STEP + TOL)
            {
                Debug.Log("Foo");
                List<Vector3> path = new List<Vector3>();
                path.Add(currentNode);


                // Backtrack from the exit to the starting position
                int a = 0;
                while (Vector3.Distance(currentNode, target.position) >= STEP - TOL && a < 100)
                {
                    a++;
                    //Debug.Log(NavMesh.SamplePosition(visitedNodes[3], out NavMeshHit hit, 1, NavMesh.AllAreas));
                    //Debug.Log(visitedNodes[3]);
                    //Debug.Log(hit.position);
                    //agent.SetDestination(hit.position);
                    //break;
                    foreach (Vector3 visitedNode in visitedNodes)
                    {
                        
                        if (NavMesh.SamplePosition(visitedNode, out NavMeshHit hit, 0.5f, NavMesh.AllAreas))
                        {
                            //Debug.Log(Vector3.Distance(currentNode, hit.position) < 1 ? true : "");
                            if (Vector3.Distance(currentNode, hit.position) < STEP)
                            {
                                path.Add(currentNode);
                                            currentNode = visitedNode;
                                            break;
                            }
                        }
                    }
                }

                Debug.Log("Bar");
                //// Reverse the path so it starts at the starting position
                path.Reverse();

                ////// Set the destination for the NavMesh Agent
                foreach (Vector3 node in path)
                {
                    //    Debug.Log($"Move to -> {node}");
                    agent.SetDestination(node);
                    //    //agent.SetDestination(new Vector3(2, 0, -2));
                    yield return new WaitForSeconds(0.1f);
                }
                yield break;
                break;
            }

            //Debug.Log($"step -> {agent.speed * Time.deltaTime}");


            // Explore the neighboring nodes
            NavMeshPath newPath = new NavMeshPath();
            List<Vector3> neighbors = GetNeighbors(currentNode);

            Debug.Log($"neighbors c -> {neighbors.Count}");
            Debug.Log($"visited c -> {visitedNodes.Count}");

            foreach (Vector3 neighbor in neighbors)
            {
                for (int i = 0; i < visitedNodes.Count; i++)
                {
                    float d = Vector3.Distance(neighbor, visitedNodes[i]);

                    // Contains
                    if (d < STEP)
                    {
                        Debug.Log("Contains");
                        break;
                    }
                }

                Debug.Log($"Added -> {neighbor}");
                visitedNodes.Add(neighbor);
                queue.Enqueue(neighbor);
                

                //if ( (Mathf.Abs(Vector3.Distance(neighbor, visitedNodes[i])) > agent.speed*Time.deltaTime - 0.1) ) {
                //    if (agent.CalculatePath(neighbor, newPath))
                //    {
                //        Debug.Log($"visited added -> {neighbor}");
                //        visitedNodes.Add(neighbor);
                //        queue.Enqueue(neighbor);
                //    }
                //}

                //if (!visitedNodes.Contains(neighbor))
                //{
                //        if (agent.CalculatePath(neighbor, newPath))
                //        {
                //            visitedNodes.Add(neighbor);
                //            queue.Enqueue(neighbor);
                //        }
                //}
            }
        }
        Debug.Log($"Exited the loop -> {queue.Count}");
    }

    List<Vector3> GetNeighbors(Vector3 position)
    {
        List<Vector3> neighbors = new List<Vector3>();
        NavMeshPath path = new NavMeshPath();

        //float step = agent.speed * Time.deltaTime;
        float step = STEP;

        if (agent.CalculatePath(position + new Vector3(0,0,step), path))
            neighbors.Add(position + new Vector3(0, 0, step));

        if (agent.CalculatePath(position + new Vector3(0, 0, -step), path))
            neighbors.Add(position + new Vector3(0, 0, -step));

        if (agent.CalculatePath(position + new Vector3(step, 0, 0), path))
            neighbors.Add(position + new Vector3(step, 0, 0));

        if (agent.CalculatePath(position + new Vector3(-step, 0, 0), path))
            neighbors.Add(position + new Vector3(-step, 0, 0));

        return neighbors;
    }
}
