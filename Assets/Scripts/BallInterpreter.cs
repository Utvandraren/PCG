using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInterpreter : Interpreter
{
    [SerializeField] float movememntSteps = 1f;
    [SerializeField] Transform[] targetTransforms;

    Transform currentParent;
    Transform startParent;

    public override void GenerateObjects(GrammarRule[] grammar, string generatedObjects)
    {

        for (int i = 0; i < targetTransforms.Length; i++)
        {
            currentParent = targetTransforms[i];
            //transform.LookAt(targetTransform[currentArm]);
            GenerateArm(grammar, generatedObjects);
        }

        //currentPosition.z += 3f;
    }

    void GenerateArm(GrammarRule[] grammar, string generatedObjects)
    {
        Stack state = new Stack();
        Vector3 currentPosition = currentParent.position;
        startParent = currentParent;
        GameObject headObject = gameObject;
        bool firstObj = true;

        foreach (char letter in generatedObjects)
        {
            for (int i = 0; i < grammar.Length; i++)
            {
                if (letter == grammar[i].letter)
                {
                    GameObject obj = Instantiate(grammar[i].objToInstantiate, currentPosition, Quaternion.identity, currentParent);
                    objPool.Add(obj);
                    currentParent = obj.transform;
                    currentPosition.x += movememntSteps;

                    if (firstObj)
                    {
                        headObject = obj;
                        firstObj = false;
                    }
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
                else if (letter == '<')
                {
                    GameObject obj = Instantiate(grammar[i].objToInstantiate, currentPosition, Quaternion.identity, currentParent);
                    objPool.Add(obj);
                    currentParent = obj.transform;

                    currentPosition.z -= movememntSteps;
                }
                else if (letter == '>')
                {
                    GameObject obj = Instantiate(grammar[i].objToInstantiate, currentPosition, Quaternion.identity, currentParent);
                    objPool.Add(obj);
                    currentParent = obj.transform;

                    currentPosition.z += movememntSteps;
                }
                else if (letter == '+')
                {
                    GameObject obj = Instantiate(grammar[i].objToInstantiate, currentPosition, Quaternion.identity, currentParent);
                    objPool.Add(obj);
                    currentParent = obj.transform;

                    currentPosition.y -= movememntSteps;
                }
                else if (letter == '-')
                {
                    GameObject obj = Instantiate(grammar[i].objToInstantiate, currentPosition, Quaternion.identity, currentParent);
                    objPool.Add(obj);
                    currentParent = obj.transform;

                    currentPosition.y -= movememntSteps;
                }
                else
                {
                    Debug.LogWarning("Unrecognised letter: " + letter.ToString());
                }
            }                       
        }
        headObject.transform.position = startParent.position;
        headObject.transform.rotation = startParent.rotation;
    }



}
