using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReactionManager : MonoBehaviour
{
    public TextMeshProUGUI reactantListText;

    private ReactionTester reactionTester;
    private Dictionary<string, int> currentReactantNames;
    private Dictionary<string, ArrayList> reactantGameObjects;
    private int numReactants;

    // Start is called before the first frame update
    void Start()
    {
        reactionTester = gameObject.GetComponent<ReactionTester>();
        currentReactantNames = new Dictionary<string, int>();
        reactantGameObjects = new Dictionary<string, ArrayList>();
        numReactants = 0;
        reactantListText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addReactant(GameObject reactantObject)
    {
        string reactantName = reactantObject.name;
        if (!currentReactantNames.ContainsKey(reactantName))
        {
            currentReactantNames[reactantName] = 0;
            reactantGameObjects[reactantName] = new ArrayList();
        }
        currentReactantNames[reactantName] += 1;
        Vector3 position = new Vector3(-10 +(numReactants * 5), 0, 0);
        GameObject reactantInstance = Instantiate(reactantObject, position, Quaternion.identity);
        reactantGameObjects[reactantName].Add(reactantInstance);
        numReactants += 1;
        UpdateText();
    }

    private void UpdateText()
    {
        string text = "";
        foreach (string name in currentReactantNames.Keys)
        {
            if (currentReactantNames[name] > 0)
            {
                string nameText = name;
                if (currentReactantNames[name] > 1)
                {
                    nameText = currentReactantNames[name] + " " + nameText;
                }
                text += nameText + " + ";
            }
        }
        if (text.EndsWith(" + "))
        {
            text = text.Remove(text.Length - 3);
        }
        reactantListText.text = text;
    }
}
