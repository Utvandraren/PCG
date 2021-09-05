using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    FixedJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out joint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
