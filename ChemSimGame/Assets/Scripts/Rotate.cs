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
        transform.Rotate(Vector3.up, -rotX, Space.Self);
        transform.Rotate(Vector3.forward, rotY, Space.Self);
    }
}
