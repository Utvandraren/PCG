using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystem : MonoBehaviour
{
    [SerializeField] private string axiom;
    [SerializeField] private int iterations = 1;
    [SerializeField] private GrammarRuleObj[] grammar;

    private Interpreter interpreter;

    Vector3 currentPosition;
    string generatedObjects = "";
    string tempGeneratedObjects = "";


    [System.Serializable]
    public struct Alphabet
    {
        public char letter;
        public string createdGrammar;
        public GameObject objToInstantiate;
        public float objDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        interpreter = GetComponent<Interpreter>();
        currentPosition = transform.position;
        generatedObjects = axiom;
        Generate();
    }

    void Generate() //Go through current instructions for what to generate
    {

        for (int i = 0; i < iterations; i++)
        {
            WriteDebugMessage();
            GenerateGrammar(generatedObjects);
            generatedObjects += tempGeneratedObjects;
            tempGeneratedObjects = "";
            Interpret();
            //interpreter.GenerateObjects(grammar, tempGeneratedObjects);

        }
    }

    void GenerateGrammar(string generatedObjects)  //Translate the current letters to the next instructions
    {
        foreach (char letter in generatedObjects)
        {
            for (int i = 0; i < grammar.Length; i++)
            {
                if (letter == grammar[i].letter)
                {
                    foreach (char result in grammar[i].createdGrammar)
                    {
                        tempGeneratedObjects += result;
                    }
                }
                
            }
           
        }

        


    }

    void Interpret() //Generate phenotype based on the genotypes generated from grammar       --->>TODO: add symbols for moving and change rotation
    {
        Stack state = new Stack();

        foreach (char letter in generatedObjects)
        {
            for (int i = 0; i < grammar.Length; i++)
            {
                if (letter == grammar[i].letter)
                {
                    Instantiate(grammar[i].objToInstantiate, currentPosition, Quaternion.identity);
                    currentPosition.x += grammar[i].objDistance;
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
                    Debug.LogWarning("Unrecognised letter");
                }
            }
        }
        //currentPosition.z += 3f;
    }

    void WriteDebugMessage()
    {
        Debug.Log("Iteration:" + iterations.ToString() + "||" + generatedObjects);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(currentPosition, new Vector3(1, 1, 1));
    }

}
