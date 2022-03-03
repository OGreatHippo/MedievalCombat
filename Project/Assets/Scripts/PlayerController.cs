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
    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private float tooFar = 10f;

    private Vector3 velocity;
    private float xRotation;
    private bool isLockedOn = false;
    private bool swingingWeapon = false;

    private Vector3 mOffset;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        Cursor.visible = false;
    }

    private void Update()
    {
        if (!isLockedOn)
        {
            mouseLook();
        }
        else
        {
            lockOn();
        }

        playerMovement();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(isLockedOn)
            {
                isLockedOn = !isLockedOn;
                print("LockedOff");
            }

            else 
            {
                isLockedOn = !isLockedOn;
                print("LockedOn");
            }
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

    private void lockOn()
    {
        RaycastHit tag;

        Transform target;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out tag, 10f))
        {
            if(tag.collider.tag == "Enemy")
            {
                target = tag.collider.transform;

                transform.LookAt(target);

                playerChar.transform.rotation = transform.rotation;

                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

                playerChar.transform.rotation = Quaternion.Euler(0, playerChar.transform.rotation.eulerAngles.y, 0);

                float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);

                if (distance >= tooFar)
                {
                    isLockedOn = !isLockedOn;
                    print("Dwarvish: I'm too far away");
                    swingingWeapon = !swingingWeapon;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    print("drawing weapon");

                    swingingWeapon = !swingingWeapon;
                }

                //could add animation here when drawing weapon / sheating it
                if(swingingWeapon)
                {
                    swingWeapon();
                }
                else
                {
                    print("sheating weapon");
                    playerWeapon.SetActive(false);
                }
            }  
        }
    }

    private void swingWeapon()
    {
        playerWeapon.SetActive(true);

        mOffset = playerWeapon.transform.position - GetMouseWorldPos();

        if (Input.GetMouseButton(0))
        {
            print("Swinging");

            //playerWeapon.transform.position = Input.mousePosition;

            playerWeapon.transform.position = GetMouseWorldPos(); //+ mOffset;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = 2;

        //mousePoint.z = playerWeapon.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}