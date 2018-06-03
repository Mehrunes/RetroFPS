using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 8f;
    public float jumpSpeed = 10f;
    public float gravity = 10f;
    public float verticalVelocity = 0f;
    public float maxVelocity = 30f;
    public float maxSpeed = 23f;
    public int jumpCounter = 0;
    public bool canWallJump = true;
    private Vector3 movementVector;
    private CharacterController controller;
    private Rigidbody characterRigidbody;


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        characterRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movementVector = transform.TransformDirection(movementVector);
        if (controller.isGrounded)
        {
            jumpCounter = 0;
            canWallJump = true;
            verticalVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpCounter++;
                verticalVelocity = jumpSpeed;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.Space) == true) && (jumpCounter <= 1))
            {
                verticalVelocity += jumpSpeed;
                if (verticalVelocity >= maxVelocity)
                {
                    verticalVelocity = maxVelocity;
                }
                jumpCounter = 2;
            }
        }
        movementVector.y = 0;
        movementVector.Normalize();
        movementVector *= speed;
        movementVector.y = verticalVelocity;
        controller.Move(movementVector * Time.deltaTime);
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        jumpCounter = 1;
        if ((controller.isGrounded) && ((movementVector.x >= 7.9) || (movementVector.z >= 7.9)))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if ((movementVector.x < maxVelocity) && (movementVector.x >= 7.9))
                    movementVector.x += 3;
                if ((movementVector.y < maxVelocity) && (movementVector.y >= 7.9))
                    movementVector.y += 3;
            }
        }
        if (!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && (canWallJump))
            {
                jumpCounter = 2;
                canWallJump = false;
                verticalVelocity = jumpSpeed;
                movementVector = hit.normal * speed;
                if (verticalVelocity >= maxVelocity)
                {
                    verticalVelocity = maxVelocity;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpCounter = 0;
    }
}