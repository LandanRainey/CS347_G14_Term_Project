using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StartNode : MonoBehaviour
{
    public void Spawn(GameObject agentPrefab)
    {
        Vector3 spawnPosition = transform.position;

        // Find a valid position on the NavMesh closest to the StartNode position
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPosition, out hit, 10.0f, NavMesh.AllAreas))
        {
            spawnPosition = hit.position;
        }
        else
        {
            Debug.LogError("Unable to find a valid position on the NavMesh.");
            return; // Abort spawning if a valid position couldn't be found
        }

        // Spawn the agent at the calculated position
        GameObject spawnedAgent = Instantiate(agentPrefab, spawnPosition, Quaternion.identity);

    }
}
