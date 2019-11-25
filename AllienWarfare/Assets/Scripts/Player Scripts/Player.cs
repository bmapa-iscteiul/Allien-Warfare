using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("a"))
        {
            rb.AddForce(-20, 0, 0);
        }

        if (Input.GetKey("w"))
        {
            rb.AddForce(0, 0, 20);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(20, 0, 0);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -20);
        }
        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(0, 200, 0);
        }
    }
}
