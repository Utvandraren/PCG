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
    //string tempGeneratedObjects = "";

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

    public void GenerateRandomObject()
    {
        startGrammar = GrammarGenerator.GenerateGrammar(startGrammar);
        Generate(startGrammar);

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
            generatedObjects += GenerateGrammar(generatedObjects);
            interpreter.GenerateObjects(grammar, generatedObjects);
        }
    }

    public string GenerateGrammar(string generatedObjects)  //Translate the current letters to the next iterations instructions
    {
        string tempGeneratedObjects = "";

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

        return tempGeneratedObjects;
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
