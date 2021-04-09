using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMolecule : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject molecule;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void make()
    {
        Instantiate(molecule, new Vector3(0,0,0), Quaternion.identity);
    }
}
