using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReactionManager : MonoBehaviour
{
    public bool askingTargetQuestions;
    public bool askingChangeQuestions;

    public TextMeshProUGUI reactantListText;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public GameObject reactionArrow;
    private GameObject reactionArrowInstance;

    public TextMeshProUGUI motionSwitchText;
    public GameObject ReactionButton;

    private bool reactantsMoving;

    public GameObject Panel;
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

    private double[] targetDHs = new double[5] { -30.5, -5.0, -92.2, -100, -111.6};
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
            targetDhText.text = "Target ΔH: " + targetDHs[currentTarget];
            ShowDialogue("What reaction gives a change in enthalpy of " + targetDHs[currentTarget] + "?");
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

    public void addReactant(GameObject reactantObject)
    {
        if (!ReactionDone)
        {
            if (reactant1 != null && reactantObject.tag == reactant1.tag && reactant1count < 3)
            {
                Vector3 position;
                if (reactant1count == 1)
                {
                    position = new Vector3(-12, 3, 5);
                }
                else
                {
                    position = new Vector3(-12, -3, 5);
                }
                GameObject reactantInstance = Instantiate(reactantObject, (position), reactantObject.transform.rotation);
                reactantInstance.GetComponent<MoveAround>().isMoving = reactantsMoving;
                reactantInstance.GetComponent<MoveAround>().dh = getMoleculedH(reactant1Name);
                reactant1count++;
                allReactants.Add(reactantInstance);
            }

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

            else if (reactant1 != null && reactant2 != null && (reactantObject.tag == reactant2.tag || reactantObject.tag == reactant1.tag))
            {
                ShowDialogue("You cannot add more than three of each type of reactant");
            }

            else if (reactant1 == null || reactant2 == null)
            {
                Vector3 position = new Vector3(-12 + (numReactants * 4), 0, 5);
                GameObject reactantInstance = Instantiate(reactantObject, position, reactantObject.transform.rotation);
                if (reactant1 == null)
                {
                    reactant1 = reactantInstance;
                    reactant1Name = reactantObject.name;
                    reactant1.GetComponent<MoveAround>().isMoving = reactantsMoving;
                    reactant1.GetComponent<MoveAround>().dh = getMoleculedH(reactant1Name);
                    reactant1count = 1;
                    allReactants.Add(reactant1);
                }
                else
                {
                    reactant2 = reactantInstance;
                    reactant2Name = reactantObject.name;
                    reactant2.GetComponent<MoveAround>().isMoving = reactantsMoving;
                    reactant2.GetComponent<MoveAround>().dh = getMoleculedH(reactant2Name);
                    reactant2count = 1;
                    allReactants.Add(reactant2);
                }
                UpdateText();
                numReactants += 1;

            }
            else
            {
                ShowDialogue("You cannot have more than two types of reactants");
            }
        } else
        {
            ShowDialogue("Please clear the reactants");
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

    public void TryToReact()
    {
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
            moveScript.dh = getMoleculedH(molecule.name);
            outputName += molecule.name + " + ";
            outputNames.Add(molecule.name);
            outputList.Add(reactionOutput);
            if (molecule.name.Equals("H2O (L)") || molecule.name.Equals("Na2SO4 (Aq)") || molecule.name.Equals("NH3"))
            {
                moveScript.reactant = false;
            }
            outputPosition.x+=3;
        }
        outputName = outputName.Substring(0, outputName.Length-3);
        ReactionDone = true;
    }

    public void ShowDialogue(string text)
    {
        dialogueText.text = text;
        dialogueBox.SetActive(true);
        Disable();
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        Enable();
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

    private void CheckTarget()
    {
        if (askingTargetQuestions)
        {
            if (totalDH == targetDHs[currentTarget])
            {
                currentTarget += 1;
                if (currentTarget < targetDHs.Length)
                {
                    ShowDialogue("That's right!\nNow what reaction gives a change in enthalpy of " + targetDHs[currentTarget] + "?");
                    targetDhText.text = "Target Δh: " + targetDHs[currentTarget];
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
                ShowDialogue("The correct answer is \nΔH: " + totalDH + "\nΔG: " + totalDG + "\nΔS: " + totalDS);
            }
        }
    }

    public double getMoleculedH(string name){ return GameManager.Instance.getMoleculedH(name); }
    public double getMoleculedS(string name) { return GameManager.Instance.getMoleculedS(name); }
    public double getMoleculedG(string name) { return GameManager.Instance.getMoleculedG(name); }

    public void Disable()
    {
        foreach (Transform child in Panel.transform)
        {
            
            if (child.gameObject.GetComponent<Button>() != null)
            {
                child.gameObject.GetComponent<Button>().interactable = false;
            }
            if (child.gameObject.GetComponent<TMP_Dropdown>() != null)
            {
                child.gameObject.GetComponent<TMP_Dropdown>().interactable = false;
            }
        }
        foreach (Transform child in Options.transform)
        {
            if (child.gameObject.GetComponent<Button>() != null)
            {
                child.gameObject.GetComponent<Button>().interactable = false;
            }
        }
        if (OptionalOptions != null)
        {
            foreach (Transform child in OptionalOptions.transform)
            {
                if (child.gameObject.GetComponent<Button>() != null)
                {
                    child.gameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
    public void Enable()
    {
        foreach (Transform child in Panel.transform)
        {
            if (child.gameObject.GetComponent<Button>() != null)
            {
                child.gameObject.GetComponent<Button>().interactable = true;
            }
            if (child.gameObject.GetComponent<TMP_Dropdown>() != null)
            {
                child.gameObject.GetComponent<TMP_Dropdown>().interactable = true;
            }
        }
        foreach (Transform child in Options.transform)
        {
            if (child.gameObject.GetComponent<Button>() != null)
            {
                child.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        if (OptionalOptions != null)
        {
            foreach (Transform child in OptionalOptions.transform)
            {
                if (child.gameObject.GetComponent<Button>() != null)
                {
                    child.gameObject.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    public void clickSet(bool click)
    {
        clickAboveNine = click;
    }

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
        string text = "";
        text += moleculeName + " ";
        text += "ΔH: " + getMoleculedH(moleculeName) + "  ";
        text += "ΔG: " + getMoleculedG(moleculeName) + "  ";
        text += "ΔS: " + getMoleculedS(moleculeName) + "  ";
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
        Disable();
    }

    public void CloseAllValues()
    {
        AllValuesBox.SetActive(false);
        Enable();
    }


}
