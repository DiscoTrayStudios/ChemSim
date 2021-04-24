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
    public GameObject reactionArrow;
    private GameObject reactionArrowInstance;

    public TextMeshProUGUI motionSwitchText;

    private bool reactantsMoving;

    private ReactionTester reactionTester;
    private GameObject reactant1;
    private string reactant1Name;
    private GameObject reactant2;
    private string reactant2Name;
    private GameObject reactionOutput;
    private string outputName;
    private List<GameObject>outputList;

    private Dictionary<string, Molecule> reactantValues;
    private int numReactants;
    private Molecule H2O;
    private Molecule H2OL;
    private Molecule H2OG;
    private Molecule CO2;
    private Molecule HCl;
    private Molecule NaOH;
    private Molecule NH3;
    private Molecule NH4;
    private Molecule OH;
    private Molecule H2SO4;
    private Molecule Na2SO4Aq;
    private Molecule Na2SO4S;
    private Molecule Na;
    private Molecule SO4;
    private Molecule N2;
    private Molecule H2;
    private bool ReactionDone = false;

    // Start is called before the first frame update
    void Start()
    {
        H2O = new Molecule("H2O", 0, 0, 0);
        H2OL = new Molecule("H2O (L)", -285.8, 69.9, -237.2);
        H2OG = new Molecule("H2O (G)", -45, 45, -45);
        CO2 = new Molecule("CO2", -393.5, 213.8, -394.4);
        HCl = new Molecule("HCl", -92.3, 186.9, -95.3);
        NaOH = new Molecule("NaOH", -425.6, 64.5, -379.5);
        NH3 = new Molecule("NH3", -46.1, 192.5, -16.5);
        NH4 = new Molecule("NH4+", -132.5, 113.4, -79.3);
        OH = new Molecule("OH-", -229.9, -10.5, -157.3);
        H2SO4 = new Molecule("H2SO4", -909.3, 20.1, -744.5);
        Na2SO4Aq = new Molecule("Na2SO4 (Aq)", -1389.5, 138.1, -1268.4);
        Na2SO4S = new Molecule("Na2SO4 (S)", -1384.5, 149.5, -1266.83);
        Na = new Molecule("Na+", -240.1, 59.0, -261.9);
        SO4 = new Molecule("SO42-", -909.3, 20.1, -744.6);
        N2 = new Molecule("N2", 0, 191.5, 0);
        H2 = new Molecule("H2", 0, 130.6, 0);
        reactionTester = gameObject.GetComponent<ReactionTester>();
        reactantValues = new Dictionary<string, Molecule>();
        numReactants = 0;
        if (!(reactantListText == null))
        {
            reactantListText.text = "";
        }
        outputList = new List<GameObject>();

        reactantValues.Add("H2O (L)", H2OL);
        reactantValues.Add("H2O (G)", H2OG);
        reactantValues.Add("H2O", H2O);
        reactantValues.Add("CO2", CO2);
        reactantValues.Add("HCl", HCl);
        reactantValues.Add("NaOH", NaOH);
        reactantValues.Add("NH3", NH3);
        reactantValues.Add("NH4+", NH4);
        reactantValues.Add("OH-", OH);
        reactantValues.Add("H2SO4", H2SO4);
        reactantValues.Add("Na2SO4 (Aq)", Na2SO4Aq);
        reactantValues.Add("Na2SO4 (S)", Na2SO4S);
        reactantValues.Add("Na+", Na);
        reactantValues.Add("SO42-", SO4);
        reactantValues.Add("N2", N2);
        reactantValues.Add("H2", H2);

        reactantsMoving = false;
        motionSwitchText.text = "Start Motion";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addReactant(GameObject reactantObject)
    {
        if (reactant1 == null || reactant2 == null || ReactionDone)
        {
            Vector3 position = new Vector3(-10 + (numReactants * 5), 0, 5);
            GameObject reactantInstance = Instantiate(reactantObject, position, reactantObject.transform.rotation);
            if (reactant1 == null)
            {
                reactant1 = reactantInstance;
                reactant1Name = reactantObject.name;
                reactant1.GetComponent<MoveAround>().isMoving = reactantsMoving;  
            } else
            {
                reactant2 = reactantInstance;
                reactant2Name = reactantObject.name;
                reactant2.GetComponent<MoveAround>().isMoving = reactantsMoving;
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
        if (reactionOutput != null)
        {
            text += " -> " + outputName;
        }
        reactantListText.text = text;
    }

    public void ClearReactants()
    {
        Destroy(reactant1);
        reactant1 = null;
        reactant1Name = null;
        Destroy(reactant2);
        reactant2 = null;
        reactant2Name = null;
        foreach (GameObject g in outputList)
        {
            Destroy(g);
        }
        reactionOutput = null;
        outputList.Clear();
        Destroy(reactionArrowInstance);
        reactionArrowInstance = null;
        numReactants = 0;
        UpdateText();
    }

    public void TryToReact()
    {
        if ((reactant1 != null && reactant2 != null)||( reactant1Name == "Na2SO4" && reactant2 ==null))
        {
            Debug.Log(1);
            Debug.Log(reactant1);
            Debug.Log(reactant2);
            if (reactionTester.ReactionIsValid(reactant1Name, reactant2Name))
            {
                List<GameObject> output = reactionTester.TryReaction(reactant1Name, reactant2Name);
                if (output != null)
                {
                    DoReaction(output);
                    UpdateText();
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

    private void DoReaction(List<GameObject> outputObject)
    {
        Debug.Log(numReactants);
        Vector3 arrowPosition = new Vector3(0, 0, 5);
        reactionArrowInstance = Instantiate(reactionArrow, arrowPosition, reactionArrow.transform.rotation);
        Vector3 outputPosition = new Vector3(5, 0, 5);
        outputName = "";
        foreach (GameObject molecule in outputObject)
        {
            reactionOutput = Instantiate(molecule, outputPosition, molecule.transform.rotation);
            MoveAround moveScript = reactionOutput.GetComponent<MoveAround>();
            moveScript.isMoving = reactantsMoving;
            outputName += molecule.name + " + ";
            outputList.Add(reactionOutput);
            if (molecule.name.Equals("H2O"))
            {
                moveScript.reactant = false;
            }
        }
        outputName = outputName.Substring(0, outputName.Length-3);
       
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

    // Causes molecules to start/stop moving
    public void ChangeMotion()
    {
        reactantsMoving = !reactantsMoving;
        if (reactantsMoving)
        {
            motionSwitchText.text = "Stop Motion";
            if (reactant1 != null)
            {
                reactant1.GetComponent<MoveAround>().StartMoving();
            }
            if (reactant2 != null)
            {
                reactant2.GetComponent<MoveAround>().StartMoving();
            }
            foreach (GameObject output in outputList)
            {
                output.GetComponent<MoveAround>().StartMoving();
            }
        }
        else
        {
            motionSwitchText.text = "Start Motion";
            if (reactant1 != null)
            {
                reactant1.GetComponent<MoveAround>().StopMoving();
            }
            if (reactant2 != null)
            {
                reactant2.GetComponent<MoveAround>().StopMoving();
            }
            foreach (GameObject output in outputList)
            {
                output.GetComponent<MoveAround>().StopMoving();
            }
        }
    }

    public double getMoleculedH(string name){return reactantValues[name].get_dH();}
    public double getMoleculedS(string name) { return reactantValues[name].get_dS(); }
    public double getMoleculedG(string name) { return reactantValues[name].get_dG(); }
}
