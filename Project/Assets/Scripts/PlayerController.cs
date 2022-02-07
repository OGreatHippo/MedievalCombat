using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSens = 100f;
    [SerializeField] private Transform playerChar;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 velocity;
    private float xRotation;
    private bool isLockedOn = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(!isLockedOn)
        {
            mouseLook();
        }
        
        playerMovement();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            isLockedOn = !isLockedOn;
            print("LockedOn");
        }
    }

    private void playerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    private void mouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerChar.Rotate(Vector3.up * mouseX);
    }
}