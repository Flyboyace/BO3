using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;
    [SerializeField]
    private float jumpSpeed = 5f;
    [SerializeField]
    private float gravity = 9.8f;
    [SerializeField]
    private float jumpCooldown = 0.5f;

    private Vector3 velocity;
    private bool canJump = true;
    private bool isGrounded;

    private float groundCheckDistance = 2f; // Slightly increased for better detection
    private LayerMask groundLayer;

    private Animator _animator;
    private Rigidbody _rb;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();

        // Correct layer mask assignment
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        // Adjusted ground check with lower origin
        Vector3 rayOrigin = transform.position + Vector3.down * 0.5f;
        RaycastHit hit;
        isGrounded = Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckDistance, groundLayer);

        // Debugging visualization
        Debug.DrawRay(rayOrigin, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);

        if (isGrounded)
        {
            Debug.Log("Grounded!");

            canJump = true;    
        }
        else
        {
            Debug.Log("Not Grounded!");
        }

        // Horizontal movement
        Vector3 movement = Input.GetAxis("Horizontal") * transform.right +
                           Input.GetAxis("Vertical") * transform.forward;

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        transform.position += movement * movementSpeed * Time.deltaTime;

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && canJump && isGrounded)
        {
            Jump();
        }

        // Update animator speed
        UpdateAnimator(movement);
    }

    private void Jump()
    {
        Debug.Log("Jump");
        _rb.velocity = new Vector3(_rb.velocity.x, jumpSpeed, _rb.velocity.z);
        _animator.SetTrigger("Jump");
        canJump = false;
      //  StartCoroutine(JumpCooldownCoroutine());
    }

    private IEnumerator JumpCooldownCoroutine()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            _rb.velocity += Vector3.down * gravity * Time.fixedDeltaTime;
        }
    }

    private void UpdateAnimator(Vector3 movement)
    {
        _animator.SetFloat("Speed", movement.magnitude > 0 ? 1 : 0);
    }
}