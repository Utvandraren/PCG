using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneticAlgorithm : MonoBehaviour
{
    string genotype;
    List<string> createdGrammar;
    List<string> candidates;



    // Start is called before the first frame update
    void Start()
    {
        createdGrammar = new List<string>();
        candidates = new List<string>();
    }

   
    /// <summary>
    /// Evolve a new generation of grammar rules based on fitnessfunctions supplied to it
    /// </summary>
    public void EvolveGrammar()
    {
        

        foreach(string grammar in createdGrammar)
        {
            if (FitnessFunction())
            {
                candidates.Add(grammar);
            }
        }
    }

    void Permutate()
    {

    }

    public bool FitnessFunction()
    {
        return true;
    }

    public void AddGrammar(string value)
    {
        createdGrammar.Add(value);
    }
}
