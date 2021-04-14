using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objRot : MonoBehaviour
{
    public float rotSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.CompareTag("Molecule"))
                {
                    PrintName(hit.transform.gameObject);
                    float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
                    float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

                    hit.transform.Rotate(Vector3.up, -rotX);
                    hit.transform.Rotate(Vector3.right, rotY);
                    
                }
                    
            }
        }
    }

    private void PrintName(GameObject temp)
    {
        print(temp.name);
    }

    private void rotateModel(Transform model)
    {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -rotX);
        transform.Rotate(Vector3.right, rotY);
    }
}
