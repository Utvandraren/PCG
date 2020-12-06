using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GeneticAlgorithm : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    double mutationRate = 0f;
    [SerializeField]
    bool usingHumanEvaluation = false;
    [SerializeField]
    int my = 0;
    [SerializeField]
    int lambda = 0;

    List<candidate> candidates;
    candidate currentCandidate;
    LSystem manager;
    bool waitingForInput = false;
    int currentCandidateIndex = 0;

    static System.Random rnd = new System.Random();

    public struct candidate
    {
        public int fitness;
        public List<GrammarRule> grammarRules;

        public candidate(GrammarRule[] newGrammarRules)
        {
            grammarRules = new List<GrammarRule>();
            for (int i = 0; i < newGrammarRules.Length; i++)
            {
                grammarRules.Add(newGrammarRules[i]);
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

    public void InitializeGrammar(GrammarRule[] grammar, LSystem lmanager)
    {
        manager = lmanager;
        candidates = new List<candidate>();

        for (int i = 0; i < (my + lambda); i++)
        {
            candidates.Add(new candidate(GenerateGrammar(grammar)));
        }
        foreach (candidate candidate in candidates)
        {
            Mutate(candidate);
        }
        currentCandidate = candidates[currentCandidateIndex];
        manager.Generate(candidates[currentCandidateIndex].grammarRules.ToArray());

    }


    /// <summary>
    /// Evolve a new generation of grammar rules based on fitnessfunctions supplied to it
    /// </summary>
    public void EvolveGrammar()//<----------------Fix so this method work-----------the createdgrammar isnt modified so look in mutatefunction
    {
        Debug.Log("EVOLVING GRAMMAR");
        currentCandidate = candidates[currentCandidateIndex];
        manager.Generate(candidates[currentCandidateIndex].grammarRules.ToArray());  //sends the current candidate grammar to be generated so we can look at it and evaluate it   
        //foreach (GrammarRule rule in candidates[currentCandidateIndex].grammarRules)
        //{
        //    Debug.Log(rule.createdGrammar);
        //}
        currentCandidateIndex++;
        Debug.Log("Candidate Index: " + currentCandidateIndex.ToString());

        if (currentCandidateIndex == (my + lambda))
        {
            CrossReproduce();
            foreach (candidate candidate in candidates)
            {
                Mutate(candidate);
            }
            currentCandidateIndex = 0;
            EvolveGrammar();
        }
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
        Debug.Log("CrossReproducing");
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
        EvolveGrammar();
    }

    public void DoNotPassCandidate()
    {
        currentCandidate.fitness = 0;
        EvolveGrammar();
    }

    void Mutate(candidate candidate)
    {
        System.Random rnd = new System.Random();
        string newGrammar;
        string oldGrammar;



        foreach (GrammarRule rule in candidate.grammarRules)
        {
            //new WaitForSeconds(0.01f);              //-----------------------------------------------

            newGrammar = "";
            oldGrammar = rule.createdGrammar;
            foreach (char c in rule.createdGrammar)
            {
                if (rnd.NextDouble() < mutationRate)
                {
                    newGrammar += RandomLetter();
                }
                else
                {
                    newGrammar += c;
                }
            }
            rule.createdGrammar = newGrammar;
            //Debug.Log("newGrammar: " + newGrammar);
            //Debug.Log("oldGrammar: " + oldGrammar);


        }


        //for (int i = 0; i < candidate.grammarRules.Count; i++)  //look at each grammarrule in candidate
        //{

        //    for (int j = 0; j < candidate.grammarRules[i].createdGrammar.Length; j++) //foreach letter in each grammar rule
        //    {
        //        if (rnd.NextDouble() < mutationRate)
        //        {                      
        //            candidate.grammarRules[i].createdGrammar.Insert(i, RandomLetter().ToString());                //<<<-----Check so this works
        //        }
        //    }
        //}

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
