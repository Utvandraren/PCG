using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    Vector3 currentPosition;
    List<GameObject> objPool;

    void Start()
    {
        objPool = new List<GameObject>();
    }

    public void GenerateObjects(GrammarRule[] grammar, string generatedObjects) //Interpret phenotype based on the genotypes generated from grammar       --->>TODO: add symbols for moving and change rotation
    {
        Stack state = new Stack();
        foreach (GameObject obj in objPool)
        {
            Destroy(obj);
        }
        objPool.Clear();
        currentPosition = Vector3.zero;

        foreach (char letter in generatedObjects)
        {
            for (int i = 0; i < grammar.Length; i++)
            {
                if (letter == grammar[i].letter)
                {
                    objPool.Add(Instantiate(grammar[i].objToInstantiate, currentPosition, Quaternion.identity));
                    currentPosition.x += 1.0f;
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
}
