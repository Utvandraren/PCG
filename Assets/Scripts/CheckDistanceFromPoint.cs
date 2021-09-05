using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DitzelGames;

public class CheckDistanceFromPoint : MonoBehaviour
{
    Transform pointToCheck;
    [SerializeField] float distanceToCheck;
    [SerializeField] GameObject floorColliderPrefab;
    [SerializeField] DitzelGames.FastIK.FastIKFabric manager;

    float startValue;


    // Start is called before the first frame update
    void Start()
    {
        float randomTime = Random.Range(0.01f, 1f);
        Invoke("InitiateIK", randomTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(pointToCheck.position, transform.position) >= distanceToCheck )
        {
            distanceToCheck = startValue + Random.Range(0f, 2f);
            MovePoint();
        }
    }

    void InitiateIK()
    {
        pointToCheck = Instantiate(floorColliderPrefab, transform.position, Quaternion.identity).transform;
        manager.Target = pointToCheck;
        startValue = distanceToCheck;
    }

    void MovePoint()
    {
        pointToCheck.position = transform.position;
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawLine(pointToCheck.position, transform.position);
        Gizmos.DrawSphere(transform.position, 1f);

    }


}
