using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarGenerator : MonoBehaviour
{
    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGrammar(GrammarRuleObj[] grammar)
    {
        foreach (GrammarRuleObj item in grammar)
        {
            generate(item);
        }
    }

    private void generate(GrammarRuleObj item)
    {
        string newGrammar = "";
        int grammarLength = rand.Next(0, 7);

        for (int i = 0; i < grammarLength; i++)
        {
            int nmbr = rand.Next(0, 6);
            if (nmbr == 0)
            {
                newGrammar += "a";
            }
            else if(nmbr == 1)
            {
                newGrammar += "b";
            }
            else if (nmbr == 2)
            {
                newGrammar += "c";

            }
            else if (nmbr == 3)
            {
                newGrammar += "d";
            }
            else if (nmbr == 4)
            {
                newGrammar += "+";

            }
            else if (nmbr == 5)
            {
                newGrammar += "-";

            }          
        }

        item.createdGrammar = newGrammar;

    }
}
