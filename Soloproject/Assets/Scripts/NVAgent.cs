using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NVAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform destinationTransform;

    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destinationTransform == null)
        {
            destinationTransform = GameObject.FindWithTag("Destination").transform;
        }
        else
        {
            agent.destination = destinationTransform.position;
        }
    }
}
