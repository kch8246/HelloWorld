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
    private WeaponGun weaponGun = null;

    private UIManager uiMng = null;

    private CharacterController characterController = null;


    private void OnEnable() {}
    private void Start() {}

    private void Awake()
    {
        playerCam = GetComponentInChildren<PlayerCamera>();
        //weapon = GetComponentInChildren<Weapon>();

        uiMng = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        characterController =
            GetComponent<CharacterController>();
    }

    private void Update()
    {
        KeyboardInputProcess();
        MouseInputProcess();
    }

    private void KeyboardInputProcess()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            //transform.position =
            //    transform.position +
            //    transform.forward * speed * Time.deltaTime;

            //transform.Translate(
            //    Vector3.forward * speed * Time.deltaTime);
            moveDir += transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //transform.position =
            //    transform.position +
            //    -transform.forward * speed * Time.deltaTime;
            moveDir += -transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(
            //    Vector3.left * speed * Time.deltaTime);
            moveDir += -transform.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(
            //    Vector3.right * speed * Time.deltaTime);
            moveDir += transform.right;
        }

        characterController.Move(
            moveDir.normalized *
            speed *
            Time.deltaTime);


        //if (Input.GetKey(KeyCode.Q))
        //{
        //    transform.Rotate(Vector3.up, -1f);
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
        //    transform.Rotate(Vector3.up, 1f);
        //}

        if (Input.GetKey(KeyCode.R))
        {
            if (weaponGun) weaponGun.Reload();
        }
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
            if (weaponGun != null)
            {
                if (weaponGun.CheckAvailable())
                {
                    weaponGun.Use(this.gameObject);
                    uiMng.UpdateBulletCount(weaponGun.BulletCnt);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Weapon"))
        {
            if (weaponGun) DropWeapon();
            GetWeapon(_other.gameObject);
        }
    }

    private void GetWeapon(GameObject _weaponGo)
    {
        _weaponGo.transform.SetParent(weaponHolderTr);
        _weaponGo.transform.localPosition = Vector3.zero;
        _weaponGo.transform.localRotation = Quaternion.identity;

        // 지금은 총 밖에 없어서 이런식으로 처리되지만
        // 무기 종류가 다양해지면 획득한 무기에 따라 처리해야 할 수도 있음.
        weaponGun = _weaponGo.GetComponent<WeaponGun>();
        weaponGun.ReloadDoneCallback = ReloadDoneCallback;

        uiMng.UpdateBulletCount(weaponGun.BulletCnt);
    }

    private void ReloadDoneCallback()
    {
        uiMng.UpdateBulletCount(weaponGun.BulletCnt);
    }

    private void DropWeapon()
    {
        if (weaponGun == null) return;

        Transform dropWeaponTr = weaponGun.transform;
        dropWeaponTr.transform.SetParent(null);

        float angle = Random.Range(0f, 360f);
        float dist = 5f;
        Vector3 dropPos =
            transform.position +
            new Vector3(Mathf.Cos(angle) * dist, 0f, Mathf.Sin(angle) * dist);
        dropWeaponTr.position = dropPos;
    }
}
