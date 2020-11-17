using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GeneticAlgorithm : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)]
    float mutationRate = 0f;
    [SerializeField]
    bool usingHumanEvaluation = false;
    [SerializeField]
    int my = 0;
    [SerializeField]
    int lambda = 0;
    [SerializeField]
    int iterations = 0;


    string genotype;
    List<string> createdGrammar;
    List<candidate> candidates;
    candidate currentCandidate;


    bool waitingForInput = false;
 
    public struct candidate
    {
        public int fitness;
        public GrammarRule[] grammarRule;

        public candidate(GrammarRule[] newGrammarRules)
        {
            grammarRule = newGrammarRules;
            fitness = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        createdGrammar = new List<string>();
        candidates = new List<candidate>();
    }

    /// <summary>
    /// Evolve a new generation of grammar rules based on fitnessfunctions supplied to it
    /// </summary>
    public void EvolveGrammar(GrammarRule[] grammar)
    {
        for (int i = 0; i < iterations; i++)
        {
            foreach (candidate candidate in candidates)
            {
                currentCandidate = candidate;
                waitingForInput = true;
                while (waitingForInput && usingHumanEvaluation)
                {

                }
                //Evaluate(candidate);   ---------Use this later
            }
            CrossReproduce();

        }


        //candidates.RemoveAll(item => item.fitness > 0);

    }

    bool GreaterThan(int x, int y)
    {
        if(x > y)
        {
            return true;
        }
        return false;
    }

    void Evaluate(candidate candidate)
    {
        
    }
    
    void CrossReproduce()
    {
        //Take the halv of the candidates that has the highest fitness and 
        candidates.Sort((x, y) => x.fitness.CompareTo(y.fitness));
        candidates.RemoveRange(candidates.Count / 2, candidates.Count / 2);

        //add new offspring of the best candiates to list so its my + lamda amount again
        for (int i = 0; i < lambda; i++)
        {
            candidates.Add(candidates[i % my]);
        }

        //---------------------Not fully made 
    }

    public void PassCandidate()
    {
        currentCandidate.fitness = 1;
        waitingForInput = false;
    }

    public void DoNotPassCandidate()
    {
        currentCandidate.fitness = 0;
        waitingForInput = false;

    }

    void Mutate(candidate candidate)
    {
        System.Random rnd = new System.Random();

        for (int i = 0; i < candidate.grammarRule.Length; i++)
        {
            if (rnd.NextDouble() < mutationRate)
            {
                candidate.grammarRule.Insert(i, RandomLetter().ToString());                //<<<-----Check so this works
            }
        }
    }

    public static char RandomLetter()
    {
        System.Random rnd = new System.Random();
        char[] letters = { 'a', 'b', 'c', 'x', '-' };
        return letters[rnd.Next(0, 5)];
    }


    public static GrammarRule[] GenerateGrammar(GrammarRule[] grammar)
    {
        foreach (GrammarRule item in grammar)
        {
            string newGrammar = "";
            int grammarLength = rand.Next(0, 7);

            for (int i = 0; i < grammarLength; i++)
            {
                newGrammar += GeneticAlgorithm.RandomLetter();

            }

            item.createdGrammar = newGrammar;
        }

        return grammar;
    }
    /// <summary>
    /// Randomly generate grammar to interpret
    /// </summary>
    /// <param name="item"></param>
    private void GenerateGrammarRule(GrammarRule item)
    {
        string newGrammar = "";
        System.Random rand = new System.Random();
        int grammarLength = rand.Next(0, 7);

        for (int i = 0; i < grammarLength; i++)
        {
            newGrammar += GeneticAlgorithm.RandomLetter();
 
        }

        item.createdGrammar = newGrammar;

    }

    public void AddGrammar(string value)
    {
        candidates.Add(new candidate(value));
    }
}
