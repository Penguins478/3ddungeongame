using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -18f;
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;

    Vector3 velocity;
    bool isGrounded;
    float trueSpeed;

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 && z != 0)
        {
            trueSpeed = (float)Mathf.Sqrt(speed * speed / 2);
        } else
        {
            trueSpeed = speed;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * trueSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
