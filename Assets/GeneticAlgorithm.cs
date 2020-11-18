using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GeneticAlgorithm : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    float mutationRate = 0f;
    [SerializeField]
    bool usingHumanEvaluation = false;
    [SerializeField]
    int my = 0;
    [SerializeField]
    int lambda = 0;


    string genotype;
    List<string> createdGrammar;
    List<candidate> candidates;
    candidate currentCandidate;
    LSystem manager;
    bool waitingForInput = false;

    static System.Random rnd = new System.Random();

    public struct candidate
    {
        public int fitness;
        public List<GrammarRule> grammarRule;

        public candidate(GrammarRule[] newGrammarRules)
        {
            grammarRule = new List<GrammarRule>();
            for (int i = 0; i < newGrammarRules.Length; i++)
            {
                grammarRule.Add(newGrammarRules[i]);
            }
            fitness = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //createdGrammar = new List<string>();
        //candidates = new List<candidate>();
    }

    void Update()
    {

    }

    /// <summary>
    /// Evolve a new generation of grammar rules based on fitnessfunctions supplied to it
    /// </summary>
    public void EvolveGrammar(GrammarRule[] grammar, LSystem lmanager)
    {
        manager = lmanager;
        candidates = new List<candidate>();
        createdGrammar = new List<string>();

        for (int i = 0; i < (my + lambda); i++)
        {          
            candidates.Add(new candidate(GenerateGrammar(grammar)));
        }

        foreach (candidate candidate in candidates)
        {
            manager.Generate();
            currentCandidate = candidate;
            waitingForInput = true;                 ////<---------here is when the person can decide if the generated grammar get a pass or not
            while (waitingForInput && usingHumanEvaluation)
            {

            }
            //    //Evaluate(candidate);   ---------Use this later

        }
        CrossReproduce();



        //candidates.RemoveAll(item => item.fitness > 0);

    }

    bool GreaterThan(int x, int y)
    {
        if (x > y)
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

        for (int i = 0; i < candidate.grammarRule.Count; i++)
        {
            if (rnd.NextDouble() < mutationRate)
            {
                for (int j = 0; j < candidate.grammarRule[i].createdGrammar.Length; j++)
                {
                    if (rnd.NextDouble() < mutationRate)
                    {
                        candidate.grammarRule[i].createdGrammar.Insert(i, RandomLetter().ToString());                //<<<-----Check so this works
                    }
                }

            }
        }
    }

    public static char RandomLetter()
    {

        char[] letters = { 'a', 'b', 'c', '+', '-', '[', ']' };
        //return letters[rnd.Next(0, 5)];
        return letters[UnityEngine.Random.Range(0, 7)];
    }

    public static GrammarRule[] GenerateGrammar(GrammarRule[] grammar)
    {
        System.Random rand = new System.Random();

        foreach (GrammarRule item in grammar)
        {
            string newGrammar = RandomLetter().ToString();
            int grammarLength = rand.Next(2, 7);

            for (int i = 0; i < grammarLength; i++)
            {
                newGrammar += RandomLetter();

            }
            item.createdGrammar = newGrammar;
        }
        return grammar;
    }

}
