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
    public GameObject reactionManager;

    public GameObject next;
    public GameObject last;

    private int clicks;
    private bool co2Clicked;
    private bool h2oClicked;
    private bool reactionClicked;
    private bool certainSelection;
    // Start is called before the first frame update
    void Start()
    {
        clicks = 0;
        co2Clicked = false;
        h2oClicked = false;
        reactionClicked = false;
        click();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !certainSelection)
        //{
        //    clicks += 1;
        //    click(clicks);
        //}
        //if (certainSelection)
        //{
        //    if (clicks == 4)
        //    {
        //        Disable();
        //        H2ODropdown.GetComponent<TMP_Dropdown>().interactable = true;
        //        H2ODropdown.GetComponent<TMP_Dropdown>().onValueChanged.AddListener(delegate
        //        {
        //            H2OPressed();
        //        });
        //    }
        //    if (clicks == 5)
        //    {
        //        Disable();
        //        CO2Button.GetComponent<Button>().interactable = true;
        //        CO2Button.GetComponent<Button>().onClick.AddListener(CO2Pressed);
                
        //    }
        //    if (clicks == 6)
        //    {
        //        Disable();
        //        tryReactionButton.GetComponent<Button>().interactable = true;
        //        tryReactionButton.GetComponent<Button>().onClick.AddListener(ReactionPressed);
        //    }
        //}
    }

    private void click()
    {
        if (clicks == 0)
        {
            clearText();
            main.text = "Welcome to the ChemSim Tutorial! We're going to go over some of the basics so you can get more comfortable with the system. Please use the buttons in the bottom right to navigate through the tutorial!";
            Disable();
        }
        else if (clicks == 1)
        {
            clearText();
            reactants.text = "These is all of the reactants that you can use! Hover over a button or dropdown menu option to see all the values for that compound.";
            Disable();
        }
        else if (clicks == 2)
        {
            clearText();
            display.text = "Here is where all of the current reaction and product information will be displayed!";
            Disable();
        }
        else if (clicks == 3)
        {
            clearText();
            buttons.text = "This is where we can get rid of all compounds on screen, test to see if our reaction will work, and have the molecules move around and collide!";
            Disable();
        }
        else if (clicks == 4)
        {
            certainSelection = true;
            clearText();
            reactants.text = "Lets try making a reaction! Try selecting the liquid state of H2O.";
            Disable();
            H2ODropdown.GetComponent<Image>().color = Color.yellow;
            H2ODropdown.GetComponent<TMP_Dropdown>().interactable = true;
            H2ODropdown.GetComponent<TMP_Dropdown>().onValueChanged.AddListener(delegate
            {
                H2OPressed();
            });

        }
        else if (clicks == 5)
        {

            H2ODropdown.GetComponent<Image>().color = Color.yellow;
            if (h2oClicked)
            {
                H2ODropdown.GetComponent<Image>().color = Color.white;
                certainSelection = true;
                clearText();
                reactants.text = "Great! Now press on the CO2 button.";
                Disable();
                CO2Button.GetComponent<Image>().color = Color.yellow;
                CO2Button.GetComponent<Button>().interactable = true;
                CO2Button.GetComponent<Button>().onClick.AddListener(CO2Pressed);
            }
            else
            {
                clicks=4;
            }

        }
        else if (clicks == 6)
        {

            CO2Button.GetComponent<Image>().color = Color.yellow;
            if (co2Clicked)
            {
                CO2Button.GetComponent<Image>().color = Color.white;
                clearText();
                buttons.text = "Lets see what happens when we press the Try Reaction button";
                Disable();
                tryReactionButton.GetComponent<Image>().color = Color.yellow;
                certainSelection = true;
                tryReactionButton.GetComponent<Button>().interactable = true;
                tryReactionButton.GetComponent<Button>().onClick.AddListener(ReactionPressed);
            }
            else
            {
                clicks=5;
            }
        }
        else if (clicks == 7)
        {

            tryReactionButton.GetComponent<Image>().color = Color.yellow;
            if (reactionClicked)
            {
                tryReactionButton.GetComponent<Image>().color = Color.white;
                clearText();
                main.text = "Good Job! You just made your first reaction! Before we are done, there is another important piece of information to help you use this tool.";
                Disable();
            }
            else
            {
                clicks=6;
            }
        }
        else if (clicks == 8)
        {
            clearText();
            reactants.text = "You can have up to two different kinds of reactants, but up to 3 molecules of one reactant. To get multiple molecules of a reactant, just press on the button or drop down option again.";
            Disable();
        }
        else if (clicks == 9)
        {
            clearText();
            main.text = "You can stay in here and play with the system more to get more comfortable or go back to the main menu anytime!";
            Disable();
        }
        else
        {
            clearText();
            EndOfTutorial();
            Enable();
        }
    }

    private void EndOfTutorial()
    {
        main.text = "";
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
        co2Clicked = true;
        clicks =6;
        click();

    }
    private void H2OPressed()
    {
        h2oClicked = true;
        clicks = 5;
        click();
    }

    private void ReactionPressed()
    {
        reactionClicked = true;
        clicks = 7;
        click();
    }

    public void forward()
    {
        clicks++;
        if (clicks >10)
        {
            clicks = 10;
        }
        setToWhite();
        Debug.Log(clicks);
        click();
    }

    public void backwards()
    {
        clicks--;
        if (clicks < 0)
        {
            clicks = 0;
        }
        if (4<clicks&&clicks<8)
        {
            clicks = 4;
            h2oClicked = false;
            co2Clicked = false;
            reactionClicked = false;
            reactionManager.GetComponent<ReactionManager>().ClearReactants();
        }
        setToWhite();
        click();
    }
    private void clearText()
    {
        main.text = "";
        reactants.text = "";
        buttons.text = "";
        display.text = "";
    }

    private void setToWhite()
    {
        tryReactionButton.GetComponent<Image>().color = Color.white;
        H2ODropdown.GetComponent<Image>().color = Color.white;
        CO2Button.GetComponent<Image>().color = Color.white;
    }
}
