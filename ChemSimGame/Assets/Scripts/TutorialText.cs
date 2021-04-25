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
    private int clicks;
    // Start is called before the first frame update
    void Start()
    {
        main.text = "Welcome to the ChemSim Tutorial! We're going to go over some of the basics so you can get more comfortable with the system. Please click to continue!";
        clicks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicks += 1;
            click(clicks);
        }
    }

    private void click(int clicks)
    {
        if (clicks == 1)
        {
            main.text = "";
            reactants.text = "This is all of the reactants that you can use! Hover over a button or dropdown menu option to see all the values for that compound.";
        }
        if (clicks == 2)
        {
            reactants.text = "";
            display.text = "Here is where all of the current reaction and product information will be displayed!";
        }
        if (clicks == 3)
        {
            display.text = "";
            buttons.text = "This is where we can get rid of all compounds on screen, test to see if our reaction will work, and have the molecules move around and collide!";
        }
        if (clicks == 4)
        {
            buttons.text = "";
            reactants.text = "Lets try making a reaction! Try selecting the liquid state of H2O.";
            //Code to make sure that happens
        }
        if (clicks == 5)
        {
            reactants.text = "";
            reactants.text = "Great! Now press on the CO2 button.";
            //Code to make sure that happens
        }
        if (clicks == 6)
        {
            reactants.text = "";
            buttons.text = "Lets see what happens when we press the Try Reaction button";
            //Code to make sure that happens
        }
        if (clicks == 7)
        {
            buttons.text = "";
            main.text = "Good Job! You just made your first reaction! Before we are done, there is another important piece of information to help you use this tool.";
        }
        if (clicks == 8)
        {
            main.text = "";
            reactants.text = "You can have up to two different kinds of reactants, but up to 3 molecules of one reactant. To get multiple molecules of a reactant, just press on the button or drop down option again.";
            //Code to make sure that happens
        }
        if (clicks == 9)
        {
            reactants.text = "";
            main.text = "Perfect! You can stay in here and play with the system more to get more comfortable or go back to the main menu anytime!";
        }
    }

}
