using UnityEngine;
using UnityEngine.AI;


public class BotPathFinding : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //    if (Input.GetButton("Jump"))
        //    {
        //        Debug.Log("Agent -> SetDestination");
        //        agent.SetDestination(target.position);
        //        agent.Resume();
        //    }
        //    else
        //    {
        //        agent.Stop();
        //    }
    }
}
