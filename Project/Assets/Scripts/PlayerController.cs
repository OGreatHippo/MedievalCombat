using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSens = 100f;
    [SerializeField] private Transform playerChar;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private float tooFar = 10f;
    [SerializeField] private float mZCoord = 2f;
    [SerializeField] private float weaponRotationSpeed = 5f;

    private Vector3 mouseLastPos;
    private Vector3 velocity;
    private float xRotation;
    private bool isLockedOn = false;
    private bool weaponDrawn = false;

    private Transform target;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        Cursor.visible = false;
    }

    private void Update()
    {
        playerMovement();

        RaycastHit tag;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out tag, 10f))
            {
                if (tag.collider.tag != "Enemy")
                {
                    isLockedOn = false;
                }

                else
                {
                    isLockedOn = !isLockedOn;

                    target = tag.collider.transform;
                }
            }
        }

        if (isLockedOn)
        {
            transform.LookAt(target);

            playerChar.transform.rotation = transform.rotation;

            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            playerChar.transform.rotation = Quaternion.Euler(0, playerChar.transform.rotation.eulerAngles.y, 0);

            float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);

            if (distance >= tooFar)
            {
                isLockedOn = !isLockedOn;
                print("Dwarvish: I'm too far away");
            }
        }
        else
        {
            mouseLook();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponDrawn = !weaponDrawn;
        }

        //could add animation here when drawing weapon / sheating it
        if (weaponDrawn)
        {
            playerWeapon.SetActive(true);

            if (Input.GetMouseButton(0))
            {
                swingWeapon();
            }
        }
        else
        {    
            playerWeapon.SetActive(false);
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

    private void swingWeapon()
    {
        //playerWeapon.transform.position = GetMouseWorldPos();

        acceleration += (Input.mousePosition - mouseLastPos).magnitude / Time.deltaTime;

        mouseLastPos = transform.position;

        playerWeapon.transform.position = Vector3.MoveTowards(playerWeapon.transform.position, GetMouseWorldPos(), acceleration);
    }

    //private Vector3 mouseDelta()
    //{
    //    Vector3 mouseCurrentPos = Input.mousePosition;

    //    mouseCurrentPos.z = mZCoord;

    //    return mouseCurrentPos - mouseStartPos;
    //}


    //get mouse pixel coordinates and convert to world position
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}

