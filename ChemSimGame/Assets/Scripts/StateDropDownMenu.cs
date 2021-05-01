using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateDropDownMenu : MonoBehaviour
{
    public List<GameObject> options;
    private TMP_Dropdown menu;
    public ReactionManager manager;

    // Start is called before the first frame update
    void Start()
    {
        menu = gameObject.GetComponent<TMP_Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addMolecule()
    {
        if (menu.value != 0)
        {
            manager.addReactant(options[menu.value - 1]);
            menu.value = 0;
        }
    }
}
