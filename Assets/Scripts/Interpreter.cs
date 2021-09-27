using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    protected Vector3 currentPosition;
    protected List<GameObject> objPool;
    protected Segment lastSegment;

    void Start()
    {
        objPool = new List<GameObject>();
    }

    public void RemoveOldObjs()
    {
        foreach (GameObject obj in objPool)
        {
            Destroy(obj);
        }
        objPool.Clear();
    }

    public virtual void GenerateObjects(GrammarRule[] grammar, string generatedObjects) //Interpret phenotype based on the genotypes generated from grammar       --->>TODO: add symbols for moving and change rotation
    {
        Stack state = new Stack();
        currentPosition = Vector3.zero;

        foreach (char letter in generatedObjects)
        {
            for (int i = 0; i < grammar.Length; i++)
            {
                if (letter == grammar[i].letter)
                {
                    GameObject obj = Instantiate(grammar[i].objToInstantiate, currentPosition, Quaternion.identity, transform);

                    if (obj.TryGetComponent(out Segment segment))
                    {
                        if (lastSegment == null)
                            lastSegment = segment;
                        else
                        {
                            Rigidbody body = segment.GetComponent<Rigidbody>();
                            lastSegment.ConnectJoint(body);
                            lastSegment = segment;
                        }
                    }

                    currentPosition.x += 1.0f;
                    objPool.Add(obj);

                }
                else if (letter == '[')
                {
                    //Pop a state from the stack and make it the current state of the turtle.
                    state.Push(currentPosition);
                }
                else if (letter == ']')
                {
                    //Push the current state of the turtle onto a pushdown stack.
                    currentPosition = (Vector3)state.Pop();
                }
                else if (letter == '+')
                {
                    currentPosition.z += 0.5f;
                }
                else if (letter == '-')
                {
                    currentPosition.z -= 0.5f;
                }
                else
                {
                    Debug.LogWarning("Unrecognised letter: " + letter.ToString());
                }
            }
        }
        //currentPosition.z += 3f;
    }

    //void ConnectJoint(FixedJoint joint)
    //{
    //    for (int j = objPool.Count; j > 0; j--)
    //    {
    //        if (objPool[j].TryGetComponent(out Rigidbody rigid))
    //        {
    //            joint.connectedBody = rigid;
    //            return;
    //        }
    //    }
    //}
}
