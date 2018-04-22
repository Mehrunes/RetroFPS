using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class characterController : MonoBehaviour
{

    public float speed = 10.0F;
    public float jumpSpeed = 5f;
    public bool isGrounded = true;
    public Rigidbody characterRigidbody;


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterRigidbody = GetComponent<Rigidbody>();
    }

// Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);


        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }
    void FixedUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Space) == true) && isGrounded)
            characterRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision c)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision c)
    {
        isGrounded = false;
    }
}
