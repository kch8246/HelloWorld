using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerCamera playerCam = null;

    private float moveSpeed = 10f;
    private float sprintSpeed = 20f;

    private float mouseX = 0f;
    private float mouseY = 0f;
    private float mouseSensX = 10f;
    private float mouseSensY = 10f;


    [SerializeField] private Transform shoulderTr = null;
    [SerializeField] private Transform weaponHolderTr = null;
    private Weapon weapon = null;


    private void OnEnable() {}
    private void Start() {}

    private void Awake()
    {
        playerCam = GetComponentInChildren<PlayerCamera>();
        //weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        KeyboardInputProcess();
        MouseInputProcess();
    }

    private void KeyboardInputProcess()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        if (Input.GetKey(KeyCode.W))
        {
            //transform.position =
            //    transform.position +
            //    transform.forward * speed * Time.deltaTime;

            transform.Translate(
                Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position =
                transform.position +
                -transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(
                Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(
                Vector3.right * speed * Time.deltaTime);
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
        mouseY = Mathf.Clamp(mouseY, -60f, 60f);

        Camera cam = Camera.main;
        // Pitch, Yaw, Roll
        // TODO: 피치회전 제한 처리
        cam.transform.localEulerAngles =
            new Vector3(-mouseY, 0f, 0f);
        transform.localEulerAngles =
            new Vector3(0f, mouseX, 0f);

        // 어깨 회전 추가
        shoulderTr.localEulerAngles =
            new Vector3(-mouseY, 0f, 0f);


        if (Input.GetMouseButtonDown(0))
        {
            if (weapon != null) weapon.Use();
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Weapon"))
        {
            if (weapon) DropWeapon();
            GetWeapon(_other.gameObject);
        }
    }

    private void GetWeapon(GameObject _weaponGo)
    {
        _weaponGo.transform.SetParent(weaponHolderTr);
        _weaponGo.transform.localPosition = Vector3.zero;
        _weaponGo.transform.localRotation = Quaternion.identity;

        weapon = _weaponGo.GetComponent<Weapon>();
    }

    private void DropWeapon()
    {
        if (weapon == null) return;

        Transform dropWeaponTr = weapon.transform;
        dropWeaponTr.transform.SetParent(null);

        float angle = Random.Range(0f, 360f);
        float dist = 5f;
        Vector3 dropPos =
            transform.position +
            new Vector3(Mathf.Cos(angle) * dist, 0f, Mathf.Sin(angle) * dist);
        dropWeaponTr.position = dropPos;
    }
}
