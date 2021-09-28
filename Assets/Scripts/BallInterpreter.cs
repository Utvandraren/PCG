using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInterpreter : Interpreter
{
    [SerializeField] GameObject defaultObject;
    [SerializeField] float angle = 22f;
    [SerializeField] Transform[] targetTransforms;
    [SerializeField] bool debug = false;


    Transform currentParent;
    Transform startParent;

    public override void GenerateObjects(GrammarRule[] grammar, string generatedObjects)
    {

        for (int i = 0; i < targetTransforms.Length; i++)
        {
            currentParent = targetTransforms[i];
            if(debug)
            {
                StopAllCoroutines();
                IEnumerator coroutine = CoroutineGenerate(grammar, generatedObjects);
                StartCoroutine(coroutine);
            }
            else
            {
                GenerateArm(grammar, generatedObjects);

            }
            
        }

        //currentPosition.z += 3f;
    }

    void GenerateArm(GrammarRule[] grammar, string generatedObjects)
    {
        Stack savedPosition = new Stack();
        Stack savedRotation = new Stack();

        Vector3 currentPosition = currentParent.position;
        Quaternion currenRotation = currentParent.rotation;

        startParent = currentParent;
        GameObject headObject = gameObject;
        bool firstObj = true;


        foreach (char letter in generatedObjects)
        {
            for (int i = 0; i < grammar.Length; i++)
            {

                if (letter == grammar[i].letter)
                {
                    //if (grammar[i].letter == 'a')
                    //    continue;

                    GameObject obj = Instantiate(grammar[i].objToInstantiate, startParent.position, Quaternion.identity);
                    objPool.Add(obj);
                    currentParent = obj.transform;
                    startParent.Translate(new Vector3(0, 1f, 0), Space.Self);

                    obj = Instantiate(grammar[i].objToInstantiate, startParent.position, Quaternion.identity);
                    objPool.Add(obj);
                    currentParent = obj.transform;
                    startParent.Translate(new Vector3(0, 1f, 0), Space.Self);

                    if (firstObj)
                    {
                        headObject = obj;
                        firstObj = false;
                    }
                }
                else if (letter == '[')
                {
                    //Pop a state from the stack and make it the current state of the turtle.
                    savedPosition.Push(startParent.position);
                    savedRotation.Push(startParent.rotation);
                }
                else if (letter == ']')
                {
                    //Push the current state of the turtle onto a pushdown stack.
                    startParent.position = (Vector3)savedPosition.Pop();
                    startParent.rotation = (Quaternion)savedRotation.Pop();

                }
                else if (letter == '<')
                {
                    //GameObject obj = Instantiate(defaultObject, currentPosition, Quaternion.identity, currentParent);
                    //objPool.Add(obj);
                    //currentParent = obj.transform;

                    //currentPosition.z -= movememntSteps;

                    startParent.Rotate(new Vector3(1, 0, 0), angle);
                }
                else if (letter == '>')
                {
                    //GameObject obj = Instantiate(defaultObject, currentPosition, Quaternion.identity, currentParent);
                    //objPool.Add(obj);
                    //currentParent = obj.transform;

                    //currentPosition.z += movememntSteps;
                    startParent.Rotate(new Vector3(1, 0, 0), -angle);

                }
                else if (letter == '+')
                {
                    //GameObject obj = Instantiate(defaultObject, currentPosition, Quaternion.identity, currentParent);
                    //objPool.Add(obj);
                    //currentParent = obj.transform;

                    //currentPosition.y -= movememntSteps;
                    startParent.Rotate(new Vector3(0, 0, 1), angle);

                }
                else if (letter == '-')
                {
                    //GameObject obj = Instantiate(defaultObject, currentPosition, Quaternion.identity, currentParent);
                    //objPool.Add(obj);
                    //currentParent = obj.transform;

                    //currentPosition.y -= movememntSteps;
                    startParent.Rotate(new Vector3(0, 0, 1), -angle);

                }
                else
                {
                    Debug.LogWarning("Unrecognised letter: " + letter.ToString());
                }
            }
        }
    }

    IEnumerator CoroutineGenerate(GrammarRule[] grammar, string generatedObjects)
    {
        Stack savedPosition = new Stack();
        Stack savedRotation = new Stack();

        Vector3 currentPosition = currentParent.position;
        Quaternion currenRotation = currentParent.rotation;

        startParent = currentParent;
        GameObject headObject = gameObject;
        bool firstObj = true;


        foreach (char letter in generatedObjects)
        {
            for (int i = 0; i < grammar.Length; i++)
            {

                if (letter == grammar[i].letter)
                {
                    yield return new WaitForSeconds(0.001f);


                    GameObject obj = Instantiate(grammar[i].objToInstantiate, startParent.position, Quaternion.identity);
                    objPool.Add(obj);
                    currentParent = obj.transform;
                    startParent.Translate(new Vector3(0, 1f, 0), Space.Self);

                    obj = Instantiate(grammar[i].objToInstantiate, startParent.position, Quaternion.identity);
                    objPool.Add(obj);
                    currentParent = obj.transform;
                    startParent.Translate(new Vector3(0, 1f, 0), Space.Self);

                    if (firstObj)
                    {
                        headObject = obj;
                        firstObj = false;
                    }
                }
                else if (letter == '[')
                {
                    //Pop a state from the stack and make it the current state of the turtle.
                    savedPosition.Push(startParent.position);
                    savedRotation.Push(startParent.rotation);
                }
                else if (letter == ']')
                {
                    //Push the current state of the turtle onto a pushdown stack.
                    startParent.position = (Vector3)savedPosition.Pop();
                    startParent.rotation = (Quaternion)savedRotation.Pop();

                }
                else if (letter == '<')
                {
                    //GameObject obj = Instantiate(defaultObject, currentPosition, Quaternion.identity, currentParent);
                    //objPool.Add(obj);
                    //currentParent = obj.transform;

                    //currentPosition.z -= movememntSteps;

                    startParent.Rotate(new Vector3(1, 0, 0), angle);
                }
                else if (letter == '>')
                {
                    //GameObject obj = Instantiate(defaultObject, currentPosition, Quaternion.identity, currentParent);
                    //objPool.Add(obj);
                    //currentParent = obj.transform;

                    //currentPosition.z += movememntSteps;
                    startParent.Rotate(new Vector3(1, 0, 0), -angle);

                }
                else if (letter == '+')
                {
                    //GameObject obj = Instantiate(defaultObject, currentPosition, Quaternion.identity, currentParent);
                    //objPool.Add(obj);
                    //currentParent = obj.transform;

                    //currentPosition.y -= movememntSteps;
                    startParent.Rotate(new Vector3(0, 0, 1), angle);

                }
                else if (letter == '-')
                {
                    //GameObject obj = Instantiate(defaultObject, currentPosition, Quaternion.identity, currentParent);
                    //objPool.Add(obj);
                    //currentParent = obj.transform;

                    //currentPosition.y -= movememntSteps;
                    startParent.Rotate(new Vector3(0, 0, 1), -angle);

                }
                else
                {
                    Debug.LogWarning("Unrecognised letter: " + letter.ToString());
                }
            }
        }
        //headObject.transform.position = startParent.position;
        //headObject.transform.rotation = startParent.rotation;
    }

}
