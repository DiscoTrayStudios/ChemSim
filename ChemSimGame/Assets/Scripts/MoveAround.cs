using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool isMoving = true;

    private float RightX;
    private float LeftX;
    private float UpY;
    private float DownY;

    private Rigidbody body;
    private Vector3 postion;
    private float originalx;
    private float originaly;
    private Vector3 originalPosition;
    private float x;
    private float y;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        originalx = transform.position.x;
        originaly = transform.position.y;
        originalPosition = transform.position;
        x = Random();
        while (x == 00) { x = Random(); }
        y = Random();
        while (x == 00) { y = Random(); }
        RightX = -2;
        LeftX = -16;
        UpY = 6;
        DownY = -2;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            postion = body.transform.position;
            if (postion.x > RightX && body.velocity.x > 0)
            {
                x = -x;
            }

            if (postion.x < LeftX && body.velocity.x < 0)
            {
                x = -x;
            }

            if (postion.y > UpY && body.velocity.y > 0)
            {
                y = -y;
            }

            if (postion.y < DownY && body.velocity.y < 0)
            {
                y = -y;
            }

            body.velocity = new Vector3(x, y, 0);
        }
    }


    private float Random()
    {
        System.Random random = new System.Random();
        int num = random.Next(-10, 10);
        return num;

    }

    public void StartMoving()
    {
        isMoving = true;
        x = Random();
        while (x == 00) { x = Random(); }
        y = Random();
        while (x == 00) { y = Random(); }
        body.velocity = new Vector3(x, y, 0);
    }

    public void StopMoving()
    {
        isMoving = false;
        body.velocity = new Vector3(0, 0, 0);
        body.position = originalPosition;
    }
}
