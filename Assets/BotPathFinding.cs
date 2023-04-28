using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BotPathFinding : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            Debug.Log("Agent -> SetDestination");
            agent.SetDestination(target.position);
            agent.Resume();
        } else
        {
            agent.Stop();
        }
    }
}
