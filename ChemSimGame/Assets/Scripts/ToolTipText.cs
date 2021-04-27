using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipText : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 offset;
    public Text text;
    public ReactionManager reaction;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setTextDrop(TextMeshProUGUI label) 
    {
        name = label.text;
        text.GetComponent<UnityEngine.UI.Text>().text = name + "\nΔH = " + reaction.getMoleculedH(name) + "\nΔS = " + reaction.getMoleculedS(name) + "\nΔG = " + reaction.getMoleculedG(name);
    }

    public void setTextButton(string name)
    {
        text.GetComponent<UnityEngine.UI.Text>().text = name + "\nΔH = " + reaction.getMoleculedH(name) + "\nΔS = " + reaction.getMoleculedS(name) + "\nΔG = " + reaction.getMoleculedG(name);
    }
}
