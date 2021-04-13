using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class ReactionManager : MonoBehaviour
{
    public TextMeshProUGUI reactantListText;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    private ReactionTester reactionTester;
    private GameObject reactant1;
    private string reactant1Name;
    private GameObject reactant2;
    private string reactant2Name;
    private Dictionary<string, int> currentReactantNames;
    private Dictionary<string, ArrayList> reactantGameObjects;
    private Dictionary<string, Molecule> reactantValues;
    private int numReactants;
    private Molecule H2O;
    private Molecule CO2;

    // Start is called before the first frame update
    void Start()
    {
        H2O = new Molecule("H2O", -285.8, 69.9, -237.2);
        CO2 = new Molecule("CO2", -393.5, 213.8, -394.4);
        reactionTester = gameObject.GetComponent<ReactionTester>();
        currentReactantNames = new Dictionary<string, int>();
        reactantGameObjects = new Dictionary<string, ArrayList>();
        reactantValues = new Dictionary<string, Molecule>();
        numReactants = 0;
        if (!(reactantListText == null))
        {
            reactantListText.text = "";
        }


        reactantValues.Add("H2O", H2O);
        reactantValues.Add("CO2", CO2);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addReactant(GameObject reactantObject)
    {
        if (reactant1 == null || reactant2 == null)
        {
            Vector3 position = new Vector3(-10 + (numReactants * 5), 0, 0);
            GameObject reactantInstance = Instantiate(reactantObject, position, reactantObject.transform.rotation);
            if (reactant1 == null)
            {
                reactant1 = reactantInstance;
                reactant1Name = reactantObject.name;
            } else
            {
                reactant2 = reactantInstance;
                reactant2Name = reactantObject.name;
            }
            UpdateText();
            numReactants += 1;
        } else
        {
            ShowDialogue("You cannot add more than two reactants");
        }
    }

    private void UpdateText()
    {
        string text = "";
        if (reactant1 != null)
        {
            text += reactant1Name;
            if (reactant2 != null)
            {
                text += " + " + reactant2Name;
            }
        }
        reactantListText.text = text;
    }

    public void ClearReactants()
    {
        Destroy(reactant1);
        reactant1 = null;
        Destroy(reactant2);
        reactant2 = null;
        numReactants = 0;
        UpdateText();
    }

    public void TryToReact()
    {
        if (reactant1 != null && reactant2 != null)
        {
            if (reactionTester.ReactionIsValid(reactant1Name, reactant2Name))
            {
                GameObject output = reactionTester.TryReaction(reactant1Name, reactant2Name);
                if (output != null)
                {
                    ClearReactants();
                    addReactant(output);
                }
                else
                {
                    ShowDialogue("There is not a model saved for this reaction.");
                }
            }
            else
            {
                ShowDialogue("That is not a valid reaction.");
            }
        }else
        {
            ShowDialogue("Please select two reactants.");
        }
    }

    public void ShowDialogue(string text)
    {
        dialogueText.text = text;
        dialogueBox.SetActive(true);
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
    }

    public double getMoleculedH(string name){return reactantValues[name].get_dH();}
    public double getMoleculedS(string name) { return reactantValues[name].get_dS(); }
    public double getMoleculedG(string name) { return reactantValues[name].get_dG(); }
}
