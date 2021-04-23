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
            buttons.text = "Lastly, this is where we can get rid of all compounds on screen, test to see if our reaction will work, and have the molecules move around and collide!";
        }
    }

}
