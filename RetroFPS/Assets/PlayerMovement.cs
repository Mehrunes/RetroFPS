using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 30f;
    public float jumpSpeed = 10f;
    public float gravity = 20f;
    public float verticalVelocity = 0f;
    public int jumpCounter = 0;
    public float NoiseMultiplier = 1f;
    public bool canWallJump = true;
    public bool canJump = true;
    private Vector3 movementVector;
    private CharacterController controller;
    private SphereCollider Noise;


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        Noise = GetComponent<SphereCollider>();
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
                jumpCounter = 2;
            }
        }
        movementVector.y = 0;
        movementVector.Normalize();
        if ((Input.GetKey(KeyCode.LeftShift) == true) && (movementVector != Vector3.zero))
        {
            movementVector *= (speed / 2);
            Noise.radius = 5f;
        }
        else if ((Input.GetKey(KeyCode.LeftControl) == true) && (movementVector != Vector3.zero))
        {
            movementVector *= (speed / 4);
            Noise.radius = 1f;
        }
        else if (movementVector == Vector3.zero)
        {
            Noise.radius = 0f;
        }
        else
        {
            Noise.radius = 10f;
            movementVector *= speed;
        }
        movementVector.y = verticalVelocity;
        controller.Move(movementVector * Time.deltaTime);
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && (canWallJump))
            {
                jumpCounter = 2;
                canWallJump = false;
                verticalVelocity = jumpSpeed;
                movementVector = hit.normal * speed;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Noise.radius);
    }
}