using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGrammarRule", menuName = "new Rule")]
public class GrammarRuleObj : ScriptableObject
{
    public char letter;
    public string createdGrammar;
    public GameObject objToInstantiate;
    public float objDistance;

}
