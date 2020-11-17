using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarGenerator : MonoBehaviour
{
    System.Random rand = new System.Random();

    public GrammarRule[] GenerateGrammar(GrammarRule[] grammar)
    {
        foreach (GrammarRule item in grammar)
        {
            GenerateGrammarRule(item);
        }

        return grammar;
    }
    /// <summary>
    /// Raqndomly generate grammar to interpret
    /// </summary>
    /// <param name="item"></param>
    private void GenerateGrammarRule(GrammarRule item)
    {
        string newGrammar = "";
        int grammarLength = rand.Next(0, 7);

        for (int i = 0; i < grammarLength; i++)
        {
            newGrammar += GeneticAlgorithm.RandomLetter();

            //int nmbr = rand.Next(0, 6);
            //if (nmbr == 0)
            //{
            //    newGrammar += "a";
            //}
            //else if(nmbr == 1)
            //{
            //    newGrammar += "b";
            //}
            //else if (nmbr == 2)
            //{
            //    newGrammar += "c";

            //}
            //else if (nmbr == 3)
            //{
            //    newGrammar += "d";
            //}
            //else if (nmbr == 4)
            //{
            //    newGrammar += "+";

            //}
            //else if (nmbr == 5)
            //{
            //    newGrammar += "-";

            //}          
        }

        item.createdGrammar = newGrammar;

    }
}
