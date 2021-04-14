using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionTester : MonoBehaviour
{
    // put each molecule in here as a public field
    public GameObject h2o;
    public GameObject co2;
    public GameObject H2CO3;
    public GameObject HCl;
    public GameObject ClOH3;
    public GameObject NaOH;
    public GameObject NaCl;

    // List all the reactions and their outputs as GameObjects in here
    // We use a hashmap as a multiset, with each reactant mapping
    // to its quantity in the reaction
    private Dictionary<Tuple<string, string>, List<GameObject>> reactions; 

    // Start is called before the first frame update
    void Start()
    {
        reactions = new Dictionary<Tuple<string, string>, List<GameObject>> {
            { new Tuple<string, string>("reactant1", "reactant2"), new List<GameObject>(){h2o } },
            { new Tuple<string, string>(h2o.name, co2.name),  new List<GameObject>(){H2CO3 } }, // Test reaction
            { new Tuple<string, string>(h2o.name, HCl.name),   new List<GameObject>(){ClOH3 } },
            { new Tuple<string, string>(NaOH.name, HCl.name),  new List<GameObject>(){NaCl, h2o } }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReactionIsValid(string reactant1Name, string reactant2Name)
    {
        Tuple<string, string> pair1 = new Tuple<string, string>(reactant1Name, reactant2Name);
        if (reactions.ContainsKey(pair1))
        {
            return true;
        } else
        {
            Tuple<string, string> pair2 = new Tuple<string, string>(reactant2Name, reactant1Name);
            return reactions.ContainsKey(pair2);
        }
    }

    public List<GameObject> TryReaction(string reactant1Name, string reactant2Name)
    {

        Tuple<string, string> pair1 = new Tuple<string, string>(reactant1Name, reactant2Name);
        Tuple<string, string> pair2 = new Tuple<string, string>(reactant2Name, reactant1Name);
        if (reactions.ContainsKey(pair1))
        {
            return reactions[pair1];
        } else if (reactions.ContainsKey(pair2)) {
            return reactions[pair2];
        } else
        {
            return null;
        }
    }
}
