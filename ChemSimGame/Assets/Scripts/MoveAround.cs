using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool isMoving = true;
    public double dh;

    private float RightX;
    private float LeftX;
    private float UpY;
    private float DownY;

    private Rigidbody body;
    private Vector3 postion;
    private float originalx;
    private float originaly;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float x;
    private float y;
    private System.Random random;

    public bool reactant;
    void Start()
    {
        /*dh = GameManager.Instance.getMoleculedH(this.name);*/
        random = new System.Random();
        RightX = -2;
        LeftX = -14;
        UpY = 6;
        DownY = -2;
        body = GetComponent<Rigidbody>();
        originalx = transform.position.x;
        originaly = transform.position.y;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        x = GenSpeed();
        y = GenSpeed();
        
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


    private float GenSpeed()
    {
        float speed = 2 * -(float)dh/100;
        if (random.Next(1) == 0)
        {
            speed *= -1;
        }
        if (!reactant)
        {
            RightX = 8;
            LeftX = 4;
        }
        return speed;

    }

    public void StartMoving()
    {
        isMoving = true;
        x = GenSpeed();
        y = GenSpeed();
        body.velocity = new Vector3(x, y, 0);
        body.freezeRotation = false;
        gameObject.GetComponent<TrailRenderer>().enabled = true;
    }

    public void StopMoving()
    {
        isMoving = false;
        body.velocity = new Vector3(0, 0, 0);
        body.position = originalPosition;
        body.freezeRotation = true;
        body.rotation = originalRotation;
        gameObject.GetComponent<TrailRenderer>().enabled = false;
    }
}
