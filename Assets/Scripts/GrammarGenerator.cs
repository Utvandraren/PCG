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
        int grammarLength = rand.Next(1, 7);
        item.createdGrammar = CheckForMissingBrackets(GenerateWordSequence(grammarLength));
    }

    private static string GenerateWordSequence(int wordLength)
    {
        string newGrammar = "";

        for (int i = 0; i < wordLength; i++)
        {
            if(rand.Next(0,7) <= 1)
            {
                wordLength--;
                newGrammar += "[";
                newGrammar += GenerateWordSequence(wordLength);
                newGrammar += "]";
                continue;
            }
            wordLength--;
            newGrammar += RandomLetter();
        }
        return newGrammar;
    }

    public static char RandomLetter()
    {
        char[] letters = { 'a', 'b', 'c', '+', '-', '<', '>' };
        return letters[rand.Next(0, 6)];
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
