using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootFloorConnector : MonoBehaviour
{
    [SerializeField] LayerMask layersToUse;
    [SerializeField] Transform rayOrigin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        RaycastHit hit;
        //Vector3 rayOrigin = transform.position;
        if (Physics.Raycast(rayOrigin.position, Vector3.down, out hit, 100f, ~layersToUse))
        {
            transform.position = hit.point;
        }
        //if (Physics.SphereCast(rayOrigin, 2f, Vector3.down, out hit, 10f, layersToUse))
        //{
        //    transform.position = hit.point + posOffset;
        //}
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawRay(rayOrigin.position, Vector3.down);
        Gizmos.DrawLine(rayOrigin.position, transform.position);
    }
}
