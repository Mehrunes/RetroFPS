using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controls : MonoBehaviour
{

    public float speed = 10.0F;
    public float jumpSpeed = 3f;
    public bool isGrounded = true;
    public Rigidbody characterRigidbody;
    public int jumpCounter = 0;
    public float translation;
    public float straffe;
    public Collider colison;
    float distToGround;

    // Use this for initialization
    void Start()
    {
        distToGround = colison.bounds.extents.y;
        Cursor.lockState = CursorLockMode.Locked;
        characterRigidbody = GetComponent<Rigidbody>();
    }

    bool IsGrounded() {
   return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
     }

void FixedUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Space) == true) && (jumpCounter <= 1))
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
        if (IsGrounded() && hit.normal.y < 0.1f)
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
