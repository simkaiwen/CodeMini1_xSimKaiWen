using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 14.0f;
    float jumpheight = 15.0f;
    float djumpheight = 15.0f;

    float zLimitA = 9.5f;
    float xLimitA = 9.5f;
    float zLimitB = 19f;
    float xLimitB = 4.5f;

    string planeCount;

    float gravityMod = 3.0f;
    bool isOnGround = true;

    Rigidbody playerRb;  //referencing

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravityMod;

    
    }

    // Update is called once per frame
    void Update()
    {
        //Declare and Initialize variables to reference to User Interaction Inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float jumpInput = Input.GetAxis("Jump");

        //Keybind Controls (GameObject) 
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        PlayerJump();

        //Setting Boundaries within Plane A
        if (planeCount == "A")
        {
            //front and back
            if (transform.position.x > -4.5 && transform.position.x < 4.5 /*&& isOnGround == false*/)
            {
                
            }
            
            else if (transform.position.z > zLimitA)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zLimitA);
            }
            else if (transform.position.z < -zLimitA)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zLimitA);
            }

            //left and right
            if (transform.position.x < -xLimitA)
            {   
                transform.position = new Vector3(-xLimitA, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > xLimitA)
            {

                transform.position = new Vector3(xLimitA, transform.position.y, transform.position.z);
            }

            
        }

        // Setting Boundaries within Plane B
        if (planeCount == "B")
        {
            //front and back
            if (transform.position.x > -4.5 && transform.position.x < 4.5 && transform.position.z < 11)
            {

            }
            else if (transform.position.z < -zLimitB)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zLimitB);
            }
            else if (transform.position.z > zLimitB)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zLimitB);
            }


            //left and right
            if (transform.position.x < -xLimitB)
            { 
                transform.position = new Vector3(-xLimitB, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > xLimitB)
            {
                transform.position = new Vector3(xLimitB, transform.position.y, transform.position.z);
            }
        }


    }

    void PlayerJump()
    {
        if (isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("Player JUMPING Y Position: " + transform.position.y);
                playerRb.AddForce(Vector3.up * jumpheight, ForceMode.Impulse);
                isOnGround = false;
                
            }
        }

    }

    //Method Call from UnityEngine (Triggers when Object collides with ground)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneA"))
        {
            isOnGround = true;
            print("Player is on Plane A");
            planeCount = "A";


        }
        else if (collision.gameObject.CompareTag("PlaneB"))
        {
            isOnGround = true;
            print("Player is on Plane B");
            planeCount = "B";

      
        }

    }



}
