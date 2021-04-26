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
    public GameObject allChildren;

    private int clicks;
    private bool co2Clicked;
    private bool certainSelection;
    // Start is called before the first frame update
    void Start()
    {
        main.text = "Welcome to the ChemSim Tutorial! We're going to go over some of the basics so you can get more comfortable with the system. Please click to continue!";
        clicks = 0;
        co2Clicked = false;
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
            if (clicks == 5)
            {
                Disable();
                CO2Button.GetComponent<Button>().interactable = true;
                CO2Button.GetComponent<Button>().onClick.AddListener(CO2Pressed);
                
            }
        }
    }

    private void click(int clicks)
    {
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
            buttons.text = "";
            reactants.text = "Lets try making a reaction! Try selecting the liquid state of H2O.";
            //Code to make sure that happens
        }
        else if (clicks == 5)
        {
            certainSelection = true;
            reactants.text = "";
            reactants.text = "Great! Now press on the CO2 button.";
            CO2Button.GetComponent<Image>().color = Color.yellow;

            //Code to make sure that happens
        }
        else if (clicks == 6)
        {
            CO2Button.GetComponent<Image>().color = Color.white;
            reactants.text = "";
            buttons.text = "Lets see what happens when we press the Try Reaction button";
            //Code to make sure that happens
        }
        else if (clicks == 7)
        {
            buttons.text = "";
            main.text = "Good Job! You just made your first reaction! Before we are done, there is another important piece of information to help you use this tool.";
        }
        else if (clicks == 8)
        {
            main.text = "";
            reactants.text = "You can have up to two different kinds of reactants, but up to 3 molecules of one reactant. To get multiple molecules of a reactant, just press on the button or drop down option again.";
            //Code to make sure that happens
        }
        else if (clicks == 9)
        {
            reactants.text = "";
            main.text = "Perfect! You can stay in here and play with the system more to get more comfortable or go back to the main menu anytime!";
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
        foreach(Transform child in allChildren.transform)
        {
            if (child.gameObject.GetComponent<Button>() != null)
            {
                child.gameObject.GetComponent<Button>().interactable = false;
            }
            if (child.gameObject.GetComponent<Dropdown>() != null)
            {
                child.gameObject.GetComponent<Dropdown>().interactable = false;
            }
        }
    }
    public void Enable()
    {
        foreach (Transform child in allChildren.transform)
        {
            if (child.gameObject.GetComponent<Button>() != null)
            {
                child.gameObject.GetComponent<Button>().interactable = true;
            }
            if (child.gameObject.GetComponent<Dropdown>() != null)
            {
                child.gameObject.GetComponent<Dropdown>().interactable = true;
            }
        }
    }

    private void CO2Pressed()
    {
        Enable();
        co2Clicked = true;
        clicks += 1;
        click(clicks);
    }
}
