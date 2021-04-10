using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionTester : MonoBehaviour
{
    // List all the reactions and their outputs as Strings in here
    // We use a hashmap as a multiset, with each reactant mapping
    // to its quantity in the reaction
    private Dictionary<Dictionary<String, int>, String> reactions = new Dictionary<Dictionary<String, int>, String>
    {
        { new Dictionary<string, int>{ { "reactant 1" , 1}, { "reactant 2" , 2} }, "output"}
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReactionIsValid(Dictionary<String, int> reactants)
    {
        return reactions.ContainsKey(reactants);
    }

    public String CheckReaction(Dictionary<String, int> reactants)
    {
        if (reactions.ContainsKey(reactants))
        {
            return reactions[reactants];
        } else
        {
            return "Invalid Reaction";
        }
    }
}
