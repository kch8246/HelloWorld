using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerCamera playerCam = null;

    private float moveSpeed = 10f;

    private float mouseX = 0f;
    private float mouseY = 0f;
    private float mouseSensX = 10f;
    private float mouseSensY = 10f;


    private void OnEnable() {}
    private void Start() {}

    private void Awake()
    {
        playerCam = GetComponentInChildren<PlayerCamera>();
    }

    private void Update()
    {
        KeyboardInputProcess();
        MouseInputProcess();
    }

    private void KeyboardInputProcess()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position =
            //    transform.position +
            //    transform.forward * moveSpeed * Time.deltaTime;

            transform.Translate(
                Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position =
                transform.position +
                -transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(
                Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(
                Vector3.right * moveSpeed * Time.deltaTime);
        }

        //if (Input.GetKey(KeyCode.Q))
        //{
        //    transform.Rotate(Vector3.up, -1f);
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
        //    transform.Rotate(Vector3.up, 1f);
        //}
    }

    private void MouseInputProcess()
    {
        mouseX +=
            Input.GetAxis("Mouse X") * mouseSensX;
        mouseY +=
            Input.GetAxis("Mouse Y") * mouseSensY;

        Camera cam = Camera.main;
        // Pitch, Yaw, Roll
        cam.transform.localEulerAngles =
            new Vector3(-mouseY, mouseX, 0f);
    }
}
