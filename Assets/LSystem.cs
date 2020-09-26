using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystem : MonoBehaviour
{
    [SerializeField] private int iterations = 1;
    [SerializeField] private Alphabet[] grammar;

    Vector3 currentPosition;
    string generatedObjects = "a";
    string tempGeneratedObjects = "";


    [System.Serializable]
    public struct Alphabet
    {
        public char startSymbol;
        public string rule;
        public GameObject objToInstantiate;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
        Generate();
    }

    void Generate() //Go through current instructions for what to do
    {
        for (int i = 0; i < iterations; i++)
        {
            foreach (char letter in generatedObjects)
            {
                Interpret(letter);
            }
            generatedObjects += tempGeneratedObjects;
            tempGeneratedObjects = "";
            WriteDebugMessage();
            GenerateObjects();
        }       
    }

    void Interpret(char letter)  //Translate the current letter to the next instructions
    {
        for (int i = 0; i < grammar.Length; i++)
        {
            if (letter == grammar[i].startSymbol)
            {
                foreach (char result in grammar[i].rule)
                {
                    tempGeneratedObjects += result;
                }
            }
        }
    }

    void GenerateObjects() //Generate phenotypes based on the gentypes generated
    {
        foreach (char letter in generatedObjects)
        {
            for (int i = 0; i < grammar.Length; i++)
            {
                if(letter == grammar[i].startSymbol)
                {
                    Instantiate(grammar[i].objToInstantiate, currentPosition, Quaternion.identity);
                    currentPosition.x += 2f;
                }
            }
        }
        currentPosition.z += 1f;
    }

    void WriteDebugMessage()
    {
        Debug.Log("Iteration:" + iterations.ToString() + "||" + generatedObjects);
    }

}
