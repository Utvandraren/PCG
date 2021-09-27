using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandoCreatureAI : MonoBehaviour
{
    [SerializeField] Transform[] targets;
    NavMeshAgent agent;

    int currentTargetindex = 0;
    int maxTarget;
    List<Segment> segmentList;

    // Start is called before the first frame update
    void Start()
    {
        segmentList = new List<Segment>();
        maxTarget = targets.Length;
        if (TryGetComponent(out agent))
            agent.SetDestination(targets[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= 5f)
            GetNextTarget();
    }

    void GetNextTarget()
    {
        currentTargetindex++;
        
        agent.SetDestination(targets[currentTargetindex].position);
    }
}
