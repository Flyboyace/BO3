using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f; // Movement speed  
    [SerializeField]
    private float jumpSpeed = 5f;    // Jump force  
    [SerializeField]
    private float gravity = 9.8f;    // Gravity strength  

    private Vector3 velocity;        // Tracks vertical velocity  
    private bool isGrounded;         // Is the player on the ground  

    // Ground height for the player  
    private float groundHeight = 99.0f;

    // Animator component  
    private Animator _animator;
    private Rigidbody _rb;

    void Start()
    {
        // Get the Animator component from the object  
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is grounded  
        //isGrounded = transform.position.y <= groundHeight + 0.1f;

        // Horizontal movement  
        Vector3 movement = Input.GetAxis("Horizontal") * transform.right +
                           Input.GetAxis("Vertical") * transform.forward;

        // Normalize and apply movement  
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        transform.position += movement * movementSpeed * Time.deltaTime;

        // Handle jumping  
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            _rb.velocity = new Vector3(_rb.velocity.x, jumpSpeed, _rb.velocity.z);
            _animator.SetTrigger("Jump");
        }

        // Apply gravity  
        //if (!isGrounded)
        //{
        //    velocity.y -= gravity * Time.deltaTime; // Apply gravity when airborne  
        //}

        //Apply vertical movement  
        //transform.position += new Vector3(0, velocity.y * Time.deltaTime, 0);
        /*
        // Prevent sinking below ground  
        if (transform.position.y < groundHeight)
        {
            transform.position = new Vector3(transform.position.x, groundHeight, transform.position.z);
            velocity.y = 0; // Reset velocity when hitting the ground  
        }
        */
        // Update animator speed based on movement  
        UpdateAnimator(movement);
    }

    private void UpdateAnimator(Vector3 movement)
    {
        // Check if the player is moving  
        if (movement.magnitude > 0)
        {
            _animator.SetFloat("Speed", 1); // Set speed parameter to 1 when moving  
        }
        else
        {
            _animator.SetFloat("Speed", 0); // Set speed parameter to 0 when not moving  
        }
    }
}