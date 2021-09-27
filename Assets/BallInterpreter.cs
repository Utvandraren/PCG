using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInterpreter : Interpreter
{
    Transform currentParent;
    [SerializeField]Transform[] targetTransforms;
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


                    //currentTransform.localPosition += movementTick;
                    //currentTransform.localPosition *= 2f;

                    GameObject obj = Instantiate(grammar[i].objToInstantiate, currentPosition, Quaternion.identity, currentParent);
                    objPool.Add(obj);
                    currentParent = obj.transform;
                    currentPosition.x += 2.0f;

                    if (firstObj)
                    {
                        headObject = obj;
                        firstObj = false;
                    }
                }
                else if (letter == '[')
                {
                    //Pop a state from the stack and make it the current state of the turtle.
                    state.Push(currentParent);
                }
                else if (letter == ']')
                {
                    //Push the current state of the turtle onto a pushdown stack.
                    currentParent = (Transform)state.Pop();
                }
                else if (letter == '+')
                {
                    //currentPosition.z += 0.5f;
                    Vector3 newPos = currentParent.position;
                    currentPosition.z += 0.5f;
                    currentParent.position = newPos;
                }
                else if (letter == '-')
                {
                    //currentPosition.z -= 0.5f;
                    Vector3 newPos = currentParent.position;
                    currentPosition.z -= 0.5f;
                    currentParent.localPosition = newPos;
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
