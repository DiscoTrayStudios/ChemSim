using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipText : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 offest;
    public Text text;
    private ReactionManager reaction;
    void Start()
    {
        reaction = GetComponent<ReactionManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setText(string name) 
    {
        text.GetComponent<UnityEngine.UI.Text>().text = name + "\ndH = " + reaction.getMoleculedH(name) + "\ndS = " + reaction.getMoleculedS(name) + "\ndG = " + reaction.getMoleculedG(name);
    }
}
