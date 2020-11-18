using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystem : MonoBehaviour
{
    [SerializeField]  string axiom;
    [SerializeField]  int iterations = 1;
    [SerializeField]  GrammarRule[] grammar;
    [Space(5)]
    [SerializeField] bool randomlyGeneratedGrammar = false;
    [SerializeField] bool useGeneticAlgorithm = false;


    Interpreter interpreter;
    GrammarGenerator gramGen;
    GeneticAlgorithm genAlgo;


    Vector3 currentPosition;
    string generatedObjects = "";
    string tempGeneratedObjects = "";

    // Start is called before the first frame update
    void Start()
    {
        interpreter = GetComponent<Interpreter>();
        genAlgo = GetComponent<GeneticAlgorithm>();
        currentPosition = transform.position;
        generatedObjects = axiom;


        if (randomlyGeneratedGrammar)
        {
            grammar = GeneticAlgorithm.GenerateGrammar(grammar);
        }

        //if (useGeneticAlgorithm)
        //{
        //    genAlgo.EvolveGrammar(grammar,this);
        //}
        if (!useGeneticAlgorithm)

        {
            Generate();
        }

        
    }

    public void StartEvolving()
    {
        genAlgo.EvolveGrammar(this.grammar, this);
    }

    public void Generate() //Go through current instructions for what to generate
    {

        for (int i = 0; i < iterations; i++)
        {
            WriteDebugMessage();
            GenerateGrammar(generatedObjects);
            generatedObjects += tempGeneratedObjects;
            tempGeneratedObjects = "";
            //Interpret();
            interpreter.GenerateObjects(grammar, generatedObjects);

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
