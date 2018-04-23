using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class characterController : MonoBehaviour
{

    public float speed = 10.0F;
    public float jumpSpeed = 3f;
    public bool isGrounded = true;
    public Rigidbody characterRigidbody;
    public CharacterController controller;
    public int jumpCounter = 0;
    public float translation;
    public float straffe;


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Space) == true) && (jumpCounter <= 1 ))
        {
            jumpCounter++;
            characterRigidbody.velocity = Vector3.zero;
            characterRigidbody.angularVelocity = Vector3.zero;
            characterRigidbody.velocity += jumpSpeed * Vector3.up;
        }
        translation = Input.GetAxis("Vertical") * speed;
        straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;
        transform.Translate(straffe, 0, translation);



        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpCounter++;
                characterRigidbody.velocity += jumpSpeed * Vector3.up; 
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpCounter = 0;
        isGrounded = true;
    }

  /*  private void OnCollisionStay(Collision c)
    {
        isGrounded = true;
    }
  */ 

    private void OnCollisionExit(Collision c)
    {
        isGrounded = false;
    }
}
