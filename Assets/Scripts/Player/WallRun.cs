using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    public Transform orentation;

    public float wallDistance = 0.5f;
    public float minimumJumpHeight = 1.5f;
    public float wallRunJumpForce;

    public float wallRunGravity;

    Camera cam;

    public float fov;
    public float wallRunFov;
    public float wallRunFovTime;
    public float camTilt;
    public float camTiltTime;

    public float tilt { get; private set; }

    bool wallLeft = false;
    bool wallRight = false;

    Rigidbody rb;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            CheckWall();

            if (CanWallRun())
            {
                if (wallLeft)
                {
                    StartWallRun();
                }
                else if (wallRight)
                {
                    StartWallRun();
                }
                else
                {
                    StopWallRun();
                }
            }
            else
            {
                StopWallRun();
            }
        }
    }

    private void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orentation.right, out leftWallHit,  wallDistance);
        wallRight = Physics.Raycast(transform.position, orentation.right, out rightWallHit, wallDistance);
    }

    private bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);  
    }

    private void StartWallRun()
    {
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFov, wallRunFovTime * Time.deltaTime);

        if (wallLeft)
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        }
        else if (wallRight)
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDistance = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

                rb.AddForce(100 * wallRunJumpForce * wallRunJumpDistance, ForceMode.Force);
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDistance = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

                rb.AddForce(100 * wallRunJumpForce * wallRunJumpDistance, ForceMode.Force);
            }
        }
    }

    private void StopWallRun()
    {
        rb.useGravity = true;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunFovTime * Time.deltaTime);

        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }
}
