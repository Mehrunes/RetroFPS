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
        if (Input.GetKeyDown(KeyCode.Space) == true && (jumpCounter < 1))
        {
            characterRigidbody.velocity = Vector3.zero;
            characterRigidbody.angularVelocity = Vector3.zero;
            characterRigidbody.velocity += jumpSpeed * Vector3.up;
            jumpCounter++;
        }
        translation = Input.GetAxis("Vertical") * speed;
        straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);


        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        characterRigidbody.useGravity = false;
        characterRigidbody.velocity = Vector3.zero;
        characterRigidbody.angularVelocity = Vector3.zero;
        //transform.Translate(, jumpSpeed, -translation);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                characterRigidbody.velocity = new Vector3(0,jumpSpeed, 0);
        }
    }

    private void OnCollisionStay(Collision c)
    {
        isGrounded = true;
        characterRigidbody.useGravity = true;
        jumpCounter = 0;  
    }

    private void OnCollisionExit(Collision c)
    {
        isGrounded = false;
        characterRigidbody.useGravity = true;
    }
}
