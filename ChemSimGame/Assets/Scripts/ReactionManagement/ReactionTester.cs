using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionTester : MonoBehaviour
{
    public GameObject h2ol;
    public GameObject h2og;
    public GameObject co2;
    public GameObject H2CO3;
    public GameObject HCl;
    public GameObject ClOH3;
    public GameObject NaOH;
    public GameObject NaCl;
    public GameObject NH3;
    public GameObject NH4;
    public GameObject OH;
    public GameObject H2SO4;
    public GameObject Na2SO4aq;
    public GameObject Na2SO4s;
    public GameObject Na;
    public GameObject SO4;
    public GameObject N2;
    public GameObject H2;

     

    // This dictionary holds a Tuple as the key, inwhich the contents are the two reactants
    // The values are a list containg all products
    private Dictionary<Tuple<string, string>, List<GameObject>> reactions;
    public List<GameObject> all;

    // Start is called before the first frame update
    void Start()
    {
        all.Add(h2ol);
        all.Add(h2og);
        all.Add(co2);
        all.Add(H2CO3);
        all.Add(HCl);
        all.Add(ClOH3);
        all.Add(NaOH);
        all.Add(NaCl);
        all.Add(NH3);
        all.Add(NH4);
        all.Add(OH);
        all.Add(H2SO4);
        all.Add(Na2SO4aq);
        all.Add(Na2SO4s);
        all.Add(Na);
        all.Add(SO4);
        all.Add(N2);
        all.Add(H2);

        foreach(GameObject i in all)
        {
            i.name = this.GetComponentInParent<ReactionManager>().ConvertName(i.name);
        }

        reactions = new Dictionary<Tuple<string, string>, List<GameObject>> {
            
            { new Tuple<string, string>(h2ol.name, co2.name),  new List<GameObject>(){H2CO3 } }, // Test reaction
            { new Tuple<string, string>(h2og.name, co2.name),  new List<GameObject>(){H2CO3 } },
            { new Tuple<string, string>(h2ol.name, HCl.name),   new List<GameObject>(){ClOH3 } },
            { new Tuple<string, string>(h2og.name, HCl.name),   new List<GameObject>(){ClOH3 } },
            { new Tuple<string, string>(NaOH.name, HCl.name),  new List<GameObject>(){NaCl, h2ol } },
            { new Tuple<string, string>(NH3.name, h2ol.name), new List<GameObject>(){NH4, OH } },
            { new Tuple<string, string>(H2SO4.name, NaOH.name),  new List<GameObject>(){Na2SO4aq, h2ol, h2ol } },
            { new Tuple<string, string>(Na2SO4s.name, null),  new List<GameObject>(){Na, Na, SO4 } },
            { new Tuple<string, string>(N2.name, H2.name),  new List<GameObject>(){NH3, NH3 } },
        };

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Called from ReactionManager. Checks to see if the reaction dictionary contains the reactants and if so,
    // it checks the amount that the player put in. It checks to make sure that no matter which reactant is entered first,
    // it still returns whether or not it is valid in the amounts
    public bool ReactionIsValid(string reactant1Name, string reactant2Name, int reactant1count, int reactant2count)
    {
        // Checks real fast on the NA2SO4(s) case
        Debug.Log(reactant1Name);
        Debug.Log(Na2SO4s.name);
        if (reactant1Name == Na2SO4s.name)
        {
            Debug.Log(1);
            if (reactant2Name == null)
            {
                if (reactant1count == 1)
                {
                    return true;
                }
            }
            
        }
        // Creates tuple with reactants player put in, in order
        Tuple<string, string> pair1 = new Tuple<string, string>(reactant1Name, reactant2Name);

        // checks if this tuple is in the parent reaction dictionary
        if (reactions.ContainsKey(pair1))
        {
            // Checks to make sure there is only one of each reactant, except in the case of H2SO4 or N2
            if ((reactant1count != 1 || reactant2count != 1) ||(reactant1Name == H2SO4.name) || (reactant1Name == N2.name))
            {
                if ((reactant1Name == H2SO4.name && reactant2Name == NaOH.name))
                {
                    if (reactant1count == 1 && reactant2count == 2)
                    {
                        return true;
                    }
                }
                if ((reactant1Name == N2.name && reactant2Name == H2.name))
                {
                    if (reactant1count == 1 && reactant2count == 3)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            // Now checks with reverse order that player put reactants in
            Tuple<string, string> pair2 = new Tuple<string, string>(reactant2Name, reactant1Name);
            if (reactions.ContainsKey(pair2))
            {
                if ((reactant1count != 1 || reactant2count != 1) || (reactant2Name == H2SO4.name) || (reactant2Name == N2.name))
                {
                    if ((reactant1Name == NaOH.name && reactant2Name == H2SO4.name))
                    {
                        if (reactant1count == 2 && reactant2count == 1)
                        {
                            return true;
                        }
                    }
                    if ((reactant1Name == H2.name && reactant2Name == N2.name))
                    {
                        if (reactant1count == 3 && reactant2count == 1)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }


    // If the amounts are valid then this function will be called to return the products.
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
