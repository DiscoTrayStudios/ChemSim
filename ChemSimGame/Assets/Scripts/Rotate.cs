using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotSpeed = 500f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.up, -rotX);
        transform.RotateAround(transform.position, Vector3.right, rotY);
    }
}
