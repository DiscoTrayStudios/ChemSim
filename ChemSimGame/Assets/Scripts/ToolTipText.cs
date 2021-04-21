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

    public void setText(TextMeshProUGUI label) 
    {
        name = label.text;
        text.GetComponent<UnityEngine.UI.Text>().text = name + "\ndH = " + reaction.getMoleculedH(name) + "\ndS = " + reaction.getMoleculedS(name) + "\ndG = " + reaction.getMoleculedG(name);
    }
}
