using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Patrol : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination, cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {
        // If the agent has reached the destination and isn't waiting for a path
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Start the waiting coroutine with a random delay
            StartCoroutine(WaitBeforeNextPoint());
        }
    }

    // Coroutine to wait between 5 to 10 seconds before moving to the next point
    IEnumerator WaitBeforeNextPoint()
    {
        // Disable the agent from moving
        agent.isStopped = true;

        // Generate a random wait time between 5 and 10 seconds
        float waitTime = Random.Range(5f, 10f);

        // Wait for the random time before continuing
        yield return new WaitForSeconds(waitTime);

        // Enable the agent again and go to the next point
        agent.isStopped = false;
        GotoNextPoint();
    }
}
