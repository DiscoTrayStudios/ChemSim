using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionManager : MonoBehaviour
{
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
    }
}
