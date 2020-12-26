using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.IO;

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

    public void InitializeGrammar(GrammarRule[] grammar, LSystem lmanager)
    {
        manager = lmanager;
        candidates = new List<candidate>();

        for (int i = 0; i < (my + lambda); i++)
        {
            candidates.Add(new candidate(GrammarGenerator.GenerateGrammar(grammar)));
        }
        foreach (candidate candidate in candidates)
        {
            Mutate(candidate);
        }
        currentCandidate = candidates[currentCandidateIndex];
        manager.Generate(candidates[currentCandidateIndex].grammarRules.ToArray());

    }


    /// <summary>
    /// Evolve a new generation of grammar rules candidates based on fitnessfunctions supplied to it
    /// </summary>
    public void EvolveGrammar()//<----------------Fix so this method work-----------the createdgrammar isnt modified so look in mutatefunction
    {
        if (currentCandidateIndex == (my + lambda))
        {
            CrossReproduce();
            foreach (candidate candidate in candidates)
            {
                Mutate(candidate);
            }
            currentCandidateIndex = 0;
        }

        Debug.Log(currentCandidateIndex.ToString());
        currentCandidate = candidates[currentCandidateIndex];
        manager.Generate(candidates[currentCandidateIndex].grammarRules.ToArray());  //sends the current candidate grammar to be generated so we can look at it and evaluate it        
        currentCandidateIndex++;
        
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
                    newGrammar += GrammarGenerator.RandomLetter();
                }
                else
                {
                    newGrammar += c;
                }
            }
            rule.createdGrammar = CheckForMissingBrackets(newGrammar);
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

    /// <summary>
    /// check if the string is missing any bracket pairs to make it complete and then repair it if it is missing any
    /// </summary>
    /// <param name="stringToCheck"></param>
    public static string CheckForMissingBrackets(string stringToCheck)
    {
        int leftBrackets = 0;
        int rightBrackets = 0;

        foreach (char c in stringToCheck)
        {
            if (c == '[')
                leftBrackets++;
            else if (c == ']')
                rightBrackets++;
        }

        for (int i = 0; i < leftBrackets - rightBrackets; i++)
        {
            stringToCheck += ']';
        }

        for (int i = 0; i < rightBrackets - leftBrackets; i++)
        {
            stringToCheck += '[';
        }

        return stringToCheck;
    }

    public void SaveCurrentCandidateData()
    {
        RecordCandidateData(candidates[currentCandidateIndex]);
    }

    void RecordCandidateData(candidate candidate)
    {
        string path = "Assets/RecordedCandidates/" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".txt";
        StreamWriter writer = File.CreateText(path);

        foreach (GrammarRule rule in candidate.grammarRules)
        {
            string line = rule.letter.ToString();
            line += " => ";
            line += rule.createdGrammar;
            writer.WriteLine(line);
            
        }
        writer.Close();
        Debug.Log("Data Saved");
    }
}
