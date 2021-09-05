using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandoCreatureAI : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {   
        if (TryGetComponent(out agent))
            agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
