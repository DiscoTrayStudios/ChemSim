using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class ReactionManager : MonoBehaviour
{
    public bool askingChangeQuestions;

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

        reactantsMoving = false;
        motionSwitchText.text = "Start Motion";

        reactant1count = 0;
        reactant2count = 0;

        allReactants = new List<GameObject>();
        reactant2Name = null;
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    position = new Vector3(-10, 3, 5);
                }
                else
                {
                    position = new Vector3(-10, -3, 5);
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
                    position = new Vector3(-7, 3, 5);
                }
                else
                {
                    position = new Vector3(-7, -3, 5);
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
                Vector3 position = new Vector3(-10 + (numReactants * 3), 0, 5);
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
                    DoReaction(output);
                    UpdateText();
                    CalculateChanges(output);
                    if (askingChangeQuestions)
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
            moveScript.dh = getMoleculedH(molecule.name);
            outputName += molecule.name + " + ";
            outputList.Add(reactionOutput);
            if (molecule.name.Equals("H2O") || molecule.name.Equals("Na2SO4") || molecule.name.Equals("NH3"))
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
                ShowDialogue("The correct answer is \ndH: " + totalDH + "\ndG: " + totalDG + "\ndS: " + totalDS);
            }
        }
    }

    public double getMoleculedH(string name){ return GameManager.Instance.getMoleculedH(name); }
    public double getMoleculedS(string name) { return GameManager.Instance.getMoleculedS(name); }
    public double getMoleculedG(string name) { return GameManager.Instance.getMoleculedG(name); }
}
