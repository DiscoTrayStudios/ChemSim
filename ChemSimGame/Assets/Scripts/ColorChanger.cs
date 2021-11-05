using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Color defaultColor;
    public AudioSource sound;
    public bool notReactant;
    private Vector3 big;
    private Vector3 normal;
    // Start is called before the first frame update
    void Start()
    {
        if (notReactant)
        {
            normal = new Vector3(1.7185f, 1.7185f, 1.7185f);
            big = new Vector3(2f, 2f, 2f);
        }
        else
        {
            big = new Vector3(1.2f, 1.2f, 1.2f);
            normal = new Vector3(1f, 1f, 1f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void highlight()
    {
        if (GetComponentInParent<Button>().interactable)
        {
            if (GetComponentInParent<Image>().color != Color.yellow)
            {
                GetComponentInParent<Image>().color = Color.white;
            }
            GetComponentInParent<Transform>().localScale = big;
            sound.Play();
        }
    }

    public void defaultSettings()
    {
        if (GetComponentInParent<Image>().color != Color.yellow)
        {
            GetComponentInParent<Image>().color = defaultColor;
        }
        GetComponentInParent<Transform>().localScale = normal;
    }
}
