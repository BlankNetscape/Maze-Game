using UnityEngine;
using UnityEngine.AI;

public class SetTarget : MonoBehaviour
{
    public Transform destination;

    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        // Add the NavMeshExplorer component to the game object
        NavMeshExplorer explorer = gameObject.AddComponent<NavMeshExplorer>();
        explorer.target = destination;
    }
}
