using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sensX;
    public float sensY;

    Transform cam;
    public Transform orientation;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    public WallRun wallRun;

    private void Start()
    {
        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            MyInput();
            cam.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
            orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }

    private void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
    }
}
