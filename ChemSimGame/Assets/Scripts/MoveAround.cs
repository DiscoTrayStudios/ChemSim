using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    // Start is called before the first frame update
    
    private float RightX;
    private float LeftX;
    private float UpY;
    private float DownY;

    private Rigidbody body;
    private Vector3 postion;
    private float originalx;
    private float originaly;
    private float x;
    private float y;

    public bool reactant;
    void Start()
    {
        RightX = -2;
        LeftX = -16;
        UpY = 6;
        DownY = -2;
        x = Random();
        while (x == 00) { x = Random(); }
        y = Random();
        while (x == 00) { y = Random(); }
        body = GetComponent<Rigidbody>();
        body.velocity = new Vector3(x, y, 0);

    }

    // Update is called once per frame
    void Update()
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


    private float Random()
    {
        System.Random random = new System.Random();
        int num;
        if (reactant)
        {
             num = random.Next(1, 3);
            if (num == 1)
            {
                num = random.Next(-10, -4);
            }
            else { num = random.Next(5, 11); }
            
        }
        else
        {
            num = random.Next(-2, 3);
            RightX = 9;
            LeftX = 4;
        }
        return num;

    }
}
