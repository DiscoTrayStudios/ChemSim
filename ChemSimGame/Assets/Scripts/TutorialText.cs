using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialText : MonoBehaviour
{

    public TextMeshProUGUI main;
    public TextMeshProUGUI reactants;
    public TextMeshProUGUI buttons;
    public TextMeshProUGUI display;
    public GameObject backToMenuButton;
    public GameObject H2ODropdown;
    public GameObject CO2Button;
    public GameObject tryReactionButton;
    public GameObject allReactants;
    public GameObject allOptions;

    private int clicks;
    private bool co2Clicked;
    private bool h2oclicked;
    private bool certainSelection;
    // Start is called before the first frame update
    void Start()
    {
        main.text = "Welcome to the ChemSim Tutorial! We're going to go over some of the basics so you can get more comfortable with the system. Please click to continue!";
        clicks = 0;
        co2Clicked = false;
        h2oclicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !certainSelection)
        {
            clicks += 1;
            click(clicks);
        }
        if (certainSelection)
        {
            if (clicks == 4)
            {
                Disable();
                H2ODropdown.GetComponent<TMP_Dropdown>().interactable = true;
                H2ODropdown.GetComponent<TMP_Dropdown>().onValueChanged.AddListener(delegate
                {
                    H2OPressed();
                });
            }
            if (clicks == 5)
            {
                Disable();
                CO2Button.GetComponent<Button>().interactable = true;
                CO2Button.GetComponent<Button>().onClick.AddListener(CO2Pressed);
                
            }
            if (clicks == 6)
            {
                Disable();
                tryReactionButton.GetComponent<Button>().interactable = true;
                tryReactionButton.GetComponent<Button>().onClick.AddListener(ReactionPressed);
            }
        }
    }

    private void click(int clicks)
    {
        Debug.Log(clicks);
        if (clicks == 1)
        {
            main.text = "";
            reactants.text = "This is all of the reactants that you can use! Hover over a button or dropdown menu option to see all the values for that compound.";
        }
        else if (clicks == 2)
        {
            reactants.text = "";
            display.text = "Here is where all of the current reaction and product information will be displayed!";
        }
        else if (clicks == 3)
        {
            display.text = "";
            buttons.text = "This is where we can get rid of all compounds on screen, test to see if our reaction will work, and have the molecules move around and collide!";
        }
        else if (clicks == 4)
        {
            certainSelection = true;
            buttons.text = "";
            reactants.text = "Lets try making a reaction! Try selecting the liquid state of H2O.";
            H2ODropdown.GetComponent<Image>().color = Color.yellow;
        }
        else if (clicks == 5)
        {
            H2ODropdown.GetComponent<Image>().color = Color.white;
            certainSelection = true;
            reactants.text = "";
            reactants.text = "Great! Now press on the CO2 button.";
            CO2Button.GetComponent<Image>().color = Color.yellow;

        }
        else if (clicks == 6)
        {
            CO2Button.GetComponent<Image>().color = Color.white;
            reactants.text = "";
            buttons.text = "Lets see what happens when we press the Try Reaction button";
            tryReactionButton.GetComponent<Image>().color = Color.yellow;
            certainSelection = true;
        }
        else if (clicks == 7)
        {
            tryReactionButton.GetComponent<Image>().color = Color.white;
            buttons.text = "";
            main.text = "Good Job! You just made your first reaction! Before we are done, there is another important piece of information to help you use this tool.";
        }
        else if (clicks == 8)
        {
            main.text = "";
            reactants.text = "You can have up to two different kinds of reactants, but up to 3 molecules of one reactant. To get multiple molecules of a reactant, just press on the button or drop down option again.";
        }
        else if (clicks == 9)
        {
            reactants.text = "";
            main.text = "You can stay in here and play with the system more to get more comfortable or go back to the main menu anytime!";
        }
        else
        {
            EndOfTutorial();
        }
    }

    private void EndOfTutorial()
    {
        reactants.gameObject.SetActive(false);
        main.gameObject.SetActive(false);
        buttons.gameObject.SetActive(false);
        display.gameObject.SetActive(false);
        backToMenuButton.SetActive(true);
    }

    public void BackToMenu()
    {
        GameManager.Instance.BackToMenu();
    }

    public void Disable()
    {
        foreach(Transform child in allReactants.transform)
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
        foreach (Transform child in allOptions.transform)
        {
            if (child.gameObject.GetComponent<Button>() != null)
            {
                child.gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }
    public void Enable()
    {
        foreach (Transform child in allReactants.transform)
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
        foreach (Transform child in allOptions.transform)
        {
            if (child.gameObject.GetComponent<Button>() != null)
            {
                child.gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }

    private void CO2Pressed()
    {
        Enable();
        co2Clicked = true;
        certainSelection = false;
        clicks =6;
        click(clicks);
    }
    private void H2OPressed()
    {
        Enable();
        h2oclicked = true;
        certainSelection = false;
        clicks = 5;
        click(clicks);
    }

    private void ReactionPressed()
    {
        Enable();
        certainSelection = false;
        clicks = 7;
        click(clicks);
    }
}
