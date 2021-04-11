using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionTester : MonoBehaviour
{
    // put each molecule in here as a public field
    public GameObject h2o;

    // List all the reactions and their outputs as GameObjects in here
    // We use a hashmap as a multiset, with each reactant mapping
    // to its quantity in the reaction
    private Dictionary<Dictionary<String, int>, GameObject> reactions; 

    // Start is called before the first frame update
    void Start()
    {
        reactions = new Dictionary<Dictionary<String, int>, GameObject> {
            { new Dictionary<string, int>{ { "reactant 1" , 1}, { "reactant 2" , 2} }, h2o}
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReactionIsValid(Dictionary<String, int> reactants)
    {
        return reactions.ContainsKey(reactants);
    }

    public GameObject TryReaction(Dictionary<String, int> reactants)
    {
        if (reactions.ContainsKey(reactants))
        {
            return reactions[reactants];
        } else
        {
            return null;
        }
    }
}
