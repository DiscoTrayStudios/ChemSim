using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// ReactionManager is the backbone of this game. It holds much of the code involved in the creation, deletion, and 
// interation of all the molecules.
public class ReactionManager : MonoBehaviour
{
    public bool askingTargetQuestions;
    public bool askingChangeQuestions;

    public TextMeshProUGUI reactantListText;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public GameObject reactionArrow;
    private GameObject reactionArrowInstance;

    public GameObject reactantsButton;
    public GameObject reactantsScreen;

    public TextMeshProUGUI motionSwitchText;
    public GameObject ReactionButton;

    private bool reactantsMoving;

    //public GameObject Panel;
    public GameObject Options;
    public GameObject OptionalOptions;

    private ReactionTester reactionTester;
    private GameObject reactant1;
    private string reactant1Name;
    private GameObject reactant2;
    private string reactant2Name;
    private GameObject reactionOutput;
    private string outputName;
    private List<String> outputNames;
    private List<GameObject>outputList;
    private int reactant1count;
    private int reactant2count;
    private List<GameObject> allReactants;

    private int numReactants;
    private bool ReactionDone = false;

    private Calculator calc = new Calculator();

    public GameObject questionBox;
    public TMP_InputField dHEntry;
    public TMP_InputField dGEntry;
    public TMP_InputField dSEntry;

    private double totalDH;
    private double totalDG;
    private double totalDS;

    public TextMeshProUGUI targetDhText;

    private double[] targetDHs = new double[5] { -30.5, -5.0, -92.2, -100, -200.6};
    private int currentTarget = 0;

    private bool productPresent;
    private bool clickAboveNine;

    public GameObject AllValuesText;
    public GameObject AllValuesBox;

    // Start is called before the first frame update
    void Start()
    {
        reactionTester = gameObject.GetComponent<ReactionTester>();
        numReactants = 0;
        if (!(reactantListText == null))
        {
            reactantListText.text = "";
        }
        outputList = new List<GameObject>();
        outputNames = new List<string>();

        reactantsMoving = false;
        motionSwitchText.text = "Start Motion";

        reactant1count = 0;
        reactant2count = 0;

        allReactants = new List<GameObject>();
        reactant2Name = null;
        if (askingTargetQuestions)
        {
            targetDhText.text = "Target ΔH (kJ/mol): " + targetDHs[currentTarget];
            ShowDialogue("What reaction gives a change in enthalpy of " + targetDHs[currentTarget] + " (kJ/mol)?");
        }
        productPresent = false;
        clickAboveNine = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!AllValuesBox.activeSelf && !dialogueBox.activeSelf) && clickAboveNine) { 
            ReactionButton.GetComponent<Button>().interactable = !productPresent;
        }
    }


    // addReactant is the method to go from clicking an option from the menu to having it appear on screen
    public void addReactant(GameObject reactantObject)
    {
        if (!ReactionDone)
        {
            // If the first reactant exists and the wanted reactant is the same, as well as there not already being 3 of that type
            if (reactant1 != null && reactantObject.tag == reactant1.tag && reactant1count < 3)
            {
                Vector3 position;
                // Positions new added reactant in either 2nd or 3rd reactant position
                if (reactant1count == 1)
                {
                    position = new Vector3(-12, 3, 5);
                }
                else
                {
                    position = new Vector3(-12, -3, 5);
                }
                // creates reactant, increases count, and adds to the total reactant list
                GameObject reactantInstance = Instantiate(reactantObject, (position), reactantObject.transform.rotation);
                reactantInstance.GetComponent<MoveAround>().isMoving = reactantsMoving;
                reactantInstance.GetComponent<MoveAround>().dh = getMoleculedH(reactant1Name);
                reactant1count++;
                allReactants.Add(reactantInstance);
            }
            // exact same as above, but for the second reactant
            else if (reactant2 != null && reactantObject.tag == reactant2.tag && reactant2count < 3)
            {
                Vector3 position;
                if (reactant2count == 1)
                {
                    position = new Vector3(-8, 3, 5);
                }
                else
                {
                    position = new Vector3(-8, -3, 5);
                }
                GameObject reactantInstance = Instantiate(reactantObject, (position), reactantObject.transform.rotation);
                reactantInstance.GetComponent<MoveAround>().isMoving = reactantsMoving;
                reactantInstance.GetComponent<MoveAround>().dh = getMoleculedH(reactant2Name);
                reactant2count++;
                allReactants.Add(reactantInstance);
            }

            // If both reactants are already selected, and player attempts to add more than three of either one of them this error shows
            else if (reactant1 != null && reactant2 != null && (reactantObject.tag == reactant2.tag || reactantObject.tag == reactant1.tag))
            {
                ShowDialogue("You cannot add more than three of each type of reactant");
            }

            // If no reactants have been selected
            else if (reactant1 == null || reactant2 == null)
            {
                Vector3 position = new Vector3(-12 + (numReactants * 4), 0, 5);
                GameObject reactantInstance = Instantiate(reactantObject, position, reactantObject.transform.rotation);
                if (reactant1 == null)
                // initializes the first reactant as whatever was selected as well as sets up the movement
                {
                    reactant1 = reactantInstance;
                    reactant1Name = ConvertName(reactantObject.name);
                    reactant1.GetComponent<MoveAround>().isMoving = reactantsMoving;
                    reactant1.GetComponent<MoveAround>().dh = getMoleculedH(reactant1Name);
                    reactant1count = 1;
                    allReactants.Add(reactant1);
                }
                else
                // Same as above, but for the second reactant
                {
                    reactant2 = reactantInstance;
                    reactant2Name = ConvertName(reactantObject.name);
                    reactant2.GetComponent<MoveAround>().isMoving = reactantsMoving;
                    reactant2.GetComponent<MoveAround>().dh = getMoleculedH(reactant2Name);
                    reactant2count = 1;
                    allReactants.Add(reactant2);
                }
                // Shows player necessary text if any and increments number of reactant counter
                UpdateText();
                numReactants += 1;

            }
            // Shows error if player tries to add 3rd type of reactant when 2 exist already
            else
            {
                ShowDialogue("You cannot have more than two types of reactants");
            }
        } else
        // shows error if player tries to add reactants while reaction is done
        {
            ShowDialogue("Please clear the reactants");
        }
    }


    // Shows current reaction in words on bottom of screen
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


    // Gets rid of all reactants on screen as well as setting reactant info to null
    public void ClearReactants()
    {
        foreach (GameObject g in allReactants)
        {
            Destroy(g);
        }
        reactant1 = null;
        reactant1Name = null;
        
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
        reactant1count = 0;
        reactant2count = 0;
        allReactants.Clear();
        ReactionDone = false;
        productPresent = false;
        outputNames = new List<string>();
    }


    // This function is called when a player tries to react their reactants
    public void TryToReact()
    {
        // Checks to make sure there are two reactants to try
        // OR if there is just Na2SO4 (which dissasociates spontaneously) 
        if ((reactant1 != null && reactant2 != null)||( reactant1Name == "Na2SO4 (S)" && reactant2 ==null))
        {
            if (reactionTester.ReactionIsValid(reactant1Name, reactant2Name, reactant1count, reactant2count))
            {
                List<GameObject> output = reactionTester.TryReaction(reactant1Name, reactant2Name);
                if (output != null)
                {
                    productPresent = true;
                    DoReaction(output);
                    UpdateText();
                    CalculateChanges(output);
                    if (askingTargetQuestions)
                    {
                        CheckTarget();
                    } else if (askingChangeQuestions)
                    {
                        AskQuestion();
                    }
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
            ShowDialogue("That is not a valid reaction.");
        }
    }


    //creates product to be visible for player
    private void DoReaction(List<GameObject> outputObject)
    {
        int count = 0;
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
            moveScript.dh = getMoleculedH(molecule.name);
            outputName += molecule.name + " + ";
            outputNames.Add(molecule.name);
            outputList.Add(reactionOutput);
            if (molecule.name.Equals("H2O (L)") || molecule.name.Equals("Na2SO4 (Aq)") || molecule.name.Equals("NH3"))
            {
                moveScript.reactant = false;
            }
            if (count == 0)
            {
                outputPosition.y = 2;
            }
            else
            {
                outputPosition.y = -2;
            }
            count += 1;
        }
        outputName = outputName.Substring(0, outputName.Length-3);
        ReactionDone = true;
    }

    public void ShowDialogue(string text)
    {
        dialogueText.text = text;
        dialogueBox.SetActive(true);
        //Disable();
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        //Enable();
    }

    // Causes molecules to start/stop moving
    public void ChangeMotion()
    {
        reactantsMoving = !reactantsMoving;
        if (reactantsMoving)
        {
            motionSwitchText.text = "Stop Motion";
            foreach (GameObject g in allReactants)
            {
                g.GetComponent<MoveAround>().StartMoving();
            }
            foreach (GameObject output in outputList)
            {
                output.GetComponent<MoveAround>().StartMoving();
            }
        }
        else
        {
            motionSwitchText.text = "Start Motion";
            foreach (GameObject g in allReactants)
            {
                g.GetComponent<MoveAround>().StopMoving();
            }
            foreach (GameObject output in outputList)
            {
                output.GetComponent<MoveAround>().StopMoving();
            }
        }
    }


    // Finds the DH, DG, and DS of the reaction
    private void CalculateChanges(List<GameObject> output)
    {
        Dictionary<string, int> input = new Dictionary<string, int>();
        input.Add(reactant1Name, reactant1count);
        if (reactant2Name != null)
        {
            input.Add(reactant2Name, reactant2count);
        }
        double[] changes = calc.CalculateChanges(input, output);
        totalDH = changes[0];
        totalDG = changes[1];
        totalDS = changes[2];
    }


    // If the player is being asked to find a target value, this will check if they are right and give the appropriate response
    private void CheckTarget()
    {
        if (askingTargetQuestions)
        {
            if (totalDH == targetDHs[currentTarget])
            {
                currentTarget += 1;
                if (currentTarget < targetDHs.Length)
                {
                    ShowDialogue("That's right!\nNow what reaction gives a change in enthalpy of " + targetDHs[currentTarget] + " (kJ/mol)?");
                    targetDhText.text = "Target ΔH (kJ/mol): " + targetDHs[currentTarget];
                }
                else
                {
                    askingTargetQuestions = false;
                    ShowDialogue("That's right!\nYou've found all the target changes. Now can you find all the change values of a reaction?");
                    targetDhText.gameObject.SetActive(false);
                }
            }
            else
            {
                ShowDialogue("That's not right. The reaction you gave has a ΔH of " + totalDH);
            }
        }
    }

    private void AskQuestion()
    {
        dHEntry.text = "";
        dGEntry.text = "";
        dSEntry.text = "";
        questionBox.SetActive(true);
    }

    public void SubmitAnswer()
    {
        if (dHEntry.text.Length > 0 && dSEntry.text.Length > 0 && dGEntry.text.Length > 0)
        {
            double entereddH = Math.Round(double.Parse(dHEntry.text), 2);
            double entereddG = Math.Round(double.Parse(dGEntry.text), 2);
            double entereddS = Math.Round(double.Parse(dSEntry.text), 2);
            if (entereddG == totalDG && entereddH == totalDH && entereddS == totalDS)
            {
                questionBox.SetActive(false);
                ShowDialogue("You were right!");
            }
            else
            {
                questionBox.SetActive(false);
                ShowDialogue("The correct answer is \nΔH (kJ/mol): " + totalDH + "\nΔG (kJ/mol): " + totalDG + "\nΔS (J/molK): " + totalDS);
            }
        }
    }

    public double getMoleculedH(string name){ return GameManager.Instance.getMoleculedH(name); }
    public double getMoleculedS(string name) { return GameManager.Instance.getMoleculedS(name); }
    public double getMoleculedG(string name) { return GameManager.Instance.getMoleculedG(name); }




    // Disable() and Enable() are used to limit player interactability depending on what may on the screen
    // currently uses Panel which we do not need, will need to reimplement these functions later. 
    // These are used in ShowDialogue(), CloseDialogue(), ShowValueText(), and CloseAllValues() 
    //public void Disable()
    //{
    //    foreach (Transform child in Panel.transform)
    //    {
            
    //        if (child.gameObject.GetComponent<Button>() != null)
    //        {
    //            child.gameObject.GetComponent<Button>().interactable = false;
    //        }
    //        if (child.gameObject.GetComponent<TMP_Dropdown>() != null)
    //        {
    //            child.gameObject.GetComponent<TMP_Dropdown>().interactable = false;
    //        }
    //    }
    //    foreach (Transform child in Options.transform)
    //    {
    //        if (child.gameObject.GetComponent<Button>() != null)
    //        {
    //            child.gameObject.GetComponent<Button>().interactable = false;
    //        }
    //    }
    //    if (OptionalOptions != null)
    //    {
    //        foreach (Transform child in OptionalOptions.transform)
    //        {
    //            if (child.gameObject.GetComponent<Button>() != null)
    //            {
    //                child.gameObject.GetComponent<Button>().interactable = false;
    //            }
    //        }
    //    }
    //}
    //public void Enable()
    //{
    //    foreach (Transform child in Panel.transform)
    //    {
    //        if (child.gameObject.GetComponent<Button>() != null)
    //        {
    //            child.gameObject.GetComponent<Button>().interactable = true;
    //        }
    //        if (child.gameObject.GetComponent<TMP_Dropdown>() != null)
    //        {
    //            child.gameObject.GetComponent<TMP_Dropdown>().interactable = true;
    //        }
    //    }
    //    foreach (Transform child in Options.transform)
    //    {
    //        if (child.gameObject.GetComponent<Button>() != null)
    //        {
    //            child.gameObject.GetComponent<Button>().interactable = true;
    //        }
    //    }
    //    if (OptionalOptions != null)
    //    {
    //        foreach (Transform child in OptionalOptions.transform)
    //        {
    //            if (child.gameObject.GetComponent<Button>() != null)
    //            {
    //                child.gameObject.GetComponent<Button>().interactable = true;
    //            }
    //        }
    //    }
    //}

    public void clickSet(bool click)
    {
        clickAboveNine = click;
    }


    // Shows large list of values for reference
    public void ShowValues()
    {
        string text = "";
        if (reactant1Name != null)
        {
            text += ValueString(reactant1Name);
        }
        if (reactant2Name != null)
        {
            text += "\n" + ValueString(reactant2Name);
        }
        foreach (string name in outputNames)
        {
            text += "\n" + ValueString(name);
        }
        ShowDialogue(text);
    }


    
    private string ValueString(string moleculeName)
    {
        moleculeName = ConvertName(moleculeName);
        string text = "";
        text += moleculeName + " ";
        text += "ΔH (kJ/mol): " + getMoleculedH(moleculeName) + "  ";
        text += "ΔG (kJ/mol): " + getMoleculedG(moleculeName) + "  ";
        text += "ΔS (J/molK): " + getMoleculedS(moleculeName) + "  ";
        return text;
    }

    public void GetAllValues()
    {
        string text = "";
        Dictionary<string, Molecule> reactionValues = GameManager.Instance.getReactionTable();
        foreach (KeyValuePair<string,Molecule> pair in reactionValues)
        {
            text += ValueString(pair.Key);
            text += "\n";
        }
        AllValuesText.GetComponent<TMP_Text>().text = text;
    }


    public void ShowValueText()
    {
        AllValuesBox.SetActive(true);
        GetAllValues();
        //Disable();
    }

    public void CloseAllValues()
    {
        AllValuesBox.SetActive(false);
        //Enable();
    }

    public void ToggleReactantView()
	{
        if(reactantsScreen.activeInHierarchy)
		{
            reactantsScreen.SetActive(false);
            reactantsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Show Reactants";
		}
        else
		{
            reactantsScreen.SetActive(true);
            reactantsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Hide Reactants";
        }
	}

    public string ConvertName(string name)
	{
        if(name == "H2O (L)")
		{
            name = "H<sub>2</sub>O(l)";
		}
        else if (name == "H2O (G)")
        {
            name = "H<sub>2</sub>O(g)";
        }
        else if (name == "CO2")
        {
            name = "CO<sub>2</sub>";
        }
        //HCl needs no change, so it is skipped
        else if (name == "H2SO4")
        {
            name = "H<sub>2</sub>SO<sub>4</sub>";
        }
        //NaOH needs no change
        else if (name == "NH3")
        {
            name = "NH<sub>3</sub>";
        }
        else if (name == "N2")
        {
            name = "N<sub>3</sub>";
        }
        else if (name == "Na2SO4 (Aq)")
        {
            name = "Na<sub>2</sub>SO<sub>4</sub>(aq)";
        }
        else if (name == "Na2SO4 (S)")
        {
            name = "Na<sub>2</sub>SO<sub>4</sub>(s)";
        }
        else if (name == "H2")
		{
            name = "H<sub>2</sub>";
		}
        else if (name == "NH4+")
		{
            name = "NH<sub>4</sub>+";
		}
        else if (name == "H2CO3")
		{
            name = "H<sub>2</sub>CO<sub>3</sub>";
		}
        return name;
    }

}
