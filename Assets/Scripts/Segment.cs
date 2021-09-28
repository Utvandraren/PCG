using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    //[HideInInspector]FixedJoint joint;

    // Start is called before the first frame update
    void Start()
    {
       // TryGetComponent(out joint);
        //joint.autoConfigureConnectedAnchor = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectJoint(Rigidbody bodyToConnect)
    {
        //joint.connectedBody = bodyToConnect;
    }
}
