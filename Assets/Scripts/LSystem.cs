using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystem : MonoBehaviour
{
    [SerializeField]  string axiom;
    [SerializeField]  int iterations = 1;
    [SerializeField]  GrammarRule[] startGrammar;
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
            startGrammar = GrammarGenerator.GenerateGrammar(startGrammar);
        }
    }

    public void StartEvolving()
    {
        if (!useGeneticAlgorithm)
        {
            Generate(startGrammar);
        }
        else
        {
            genAlgo.InitializeGrammar(startGrammar, this);
            //genAlgo.EvolveGrammar();
        }
    }

    public void Generate(GrammarRule[] grammar) //Go through current instructions for what to generate
    {
        generatedObjects = axiom;
        interpreter.RemoveOldObjs();

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

    public void GenerateGrammar(string generatedObjects)  //Translate the current letters to the next instructions
    {
        foreach (char letter in generatedObjects)
        {
            for (int i = 0; i < startGrammar.Length; i++)
            {
                if (letter == startGrammar[i].letter)
                {
                    foreach (char result in startGrammar[i].createdGrammar)
                    {
                        tempGeneratedObjects += result;
                    }
                }               
            }          
        }
    }

    //void Interpret() //Generate phenotype based on the genotypes generated from grammar       --->>TODO: add symbols for moving and change rotation
    //{
    //    Stack state = new Stack();

    //    foreach (char letter in generatedObjects)
    //    {
    //        for (int i = 0; i < startGrammar.Length; i++)
    //        {
    //            if (letter == startGrammar[i].letter)
    //            {
    //                Instantiate(startGrammar[i].objToInstantiate, currentPosition, Quaternion.identity);
    //                currentPosition.x += startGrammar[i].objDistance;
    //            }
    //            else if (letter == '[')
    //            {
    //                //Pop a state from the stack and make it the current state of the turtle.
    //                state.Push(currentPosition);
    //            }
    //            else if (letter == ']')
    //            {
    //                //Push the current state of the turtle onto a pushdown stack.
    //                currentPosition = (Vector3)state.Pop();
    //            }
    //            else if (letter == '+')
    //            {
    //                currentPosition.z += 0.5f;
    //            }
    //            else if (letter == '-')
    //            {
    //                currentPosition.z -= 0.5f;
    //            }
    //            else
    //            {
    //                Debug.LogWarning("Unrecognised letter");
    //            }
    //        }
    //    }
    //    //currentPosition.z += 3f;
    //}

    void WriteDebugMessage()
    {
        //Debug.Log("Iteration:" + iterations.ToString() + "||" + generatedObjects);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(currentPosition, new Vector3(1, 1, 1));
    }

    

}
