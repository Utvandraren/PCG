using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarGenerator : MonoBehaviour
{
    static System.Random rand = new System.Random();

    public static GrammarRule[] GenerateGrammar(GrammarRule[] grammar)
    {
        rand = new System.Random();
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
    private static void GenerateGrammarRule(GrammarRule item)
    {
        string newGrammar = "";
        int grammarLength = rand.Next(1, 7);

        for (int i = 0; i < grammarLength; i++)
        {
            newGrammar += RandomLetter();
        }

        item.createdGrammar = CheckForMissingBrackets(newGrammar);

    }

    //public static string NextLetter(int letterlength) //Not finished
    //{
    //    string newGrammar = "";

    //    if (Random.Range(0,3) < 1)
    //    {
    //        newGrammar = newGrammar + '[';
    //    }
    //    else
    //    {
    //        return newGrammar + ']';
    //    }

    //}

    public static char RandomLetter()
    {
        //rand = new System.Random();

        char[] letters = { 'a', 'b', 'c', '+', '-' };
        return letters[rand.Next(0, 5)];
        //return letters[UnityEngine.Random.Range(0, 5)]; //Trying unitys random to see if there is any difference
    }

    

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
}
