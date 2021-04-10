using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionTester : MonoBehaviour
{
    // List all the reactions and their outputs as Strings in here
    // We can make a larger class than tuples if reactions have 
    // more than two reactants
    private Dictionary<Tuple<String, String>, String> reactions = new Dictionary<Tuple<String, String>, String>
    {
        { new Tuple<String, String>("reactant 1", "reactant 2"), "output"}
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReactionIsValid(String reactant1, String reactant2)
    {
        Tuple<String, String> pair1 = new Tuple<string, string>(reactant1, reactant2);
        Tuple<String, String> pair2 = new Tuple<string, string>(reactant1, reactant2);
        if (reactions.ContainsKey(pair1))
        {
            return true;
        }
        return reactions.ContainsKey(pair2);
    }

    public String CheckReaction(String reactant1, String reactant2)
    {
        Tuple<String, String> pair1 = new Tuple<string, string>(reactant1, reactant2);
        Tuple<String, String> pair2 = new Tuple<string, string>(reactant1, reactant2);
        if (reactions.ContainsKey(pair1))
        {
            return reactions[pair1];
        } else if (reactions.ContainsKey(pair2))
        {
            return reactions[pair2];
        } else
        {
            return "Invalid Reaction";
        }
    }
}
