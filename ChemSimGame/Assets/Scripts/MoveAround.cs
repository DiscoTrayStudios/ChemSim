using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// This script controls molecules movement through both randomness and the DH value of the molecule
public class MoveAround : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool isMoving = false;
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

    private void Awake()
    {
        random = new System.Random();
        RightX = -2;
        LeftX = -14;
        UpY = 5;
        DownY = -4;
        body = GetComponent<Rigidbody>();
        originalx = transform.position.x;
        originaly = transform.position.y;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        x = GenSpeed();
        y = GenSpeed();
    }

    void Start()
    {
        /*dh = GameManager.Instance.getMoleculedH(this.name);*/
        
        StopMoving();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && !body.isKinematic)
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
        float speed = (30 - (float)dh/100)/5;
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
        body.isKinematic = false;
        isMoving = true;
        x = GenSpeed();
        y = GenSpeed();
        body.freezeRotation = false;
        gameObject.GetComponent<TrailRenderer>().enabled = true;
    }

    public void StopMoving()
    {
        body.isKinematic = true;
        isMoving = false;
        body.position = originalPosition;
        body.velocity = new Vector3(0, 0, 0);
        body.freezeRotation = true;
        body.rotation = originalRotation;
        gameObject.GetComponent<TrailRenderer>().enabled = false;
    }
}
