using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform orientation;

    public float moveSpeed = 6f;
    public float jumpForce = 5f;

    public float walkSpeed = 4f;
    public float sprintSpeed = 6f;
    public float acceleration = 10f;

    public float groundDrag = 6f;
    public float airDrag = 2f;

    public LayerMask groundMask;
    public Transform groundCheck;

    public float airMultiplier = 5f;

    float movementMultiplier = 10f;

    float playerHeight = 2f;

    float horizontalMovement;
    float verticalMovement;

    float groundDistance = 0.4f;

    bool isGrounded;

    Vector3 moveDirection;
    Rigidbody rb;

    RaycastHit slopeHit;
    Vector3 slopeMoveDirection;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cam = Camera.main;
    }

    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            MyInput();
            ControlDrag();
            ControlSpeed();

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.CompareTag("Chest"))
                {
                    PlayerUI.instance.interactText.SetActive(true);
                }
                else
                {
                    PlayerUI.instance.interactText.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {

                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.PlaceDynamite();
                    Debug.Log("dynamite placed!");
                }
            }

            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        }
    }

    private void FixedUpdate()
    {
        if (!PauseMenu.instance.isPaused)
        {
            MovePlayer();
        }
    }

    private void ControlSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(movementMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(movementMultiplier * moveSpeed * slopeMoveDirection.normalized, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(airMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Acceleration);
        }
    }

    private void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }
}
