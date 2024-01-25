using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private EPortalType portalType = EPortalType.Red;

    [SerializeField] private MeshRenderer portalMr = null;
    [SerializeField] private Material matActivate = null;
    [SerializeField] private Material matDeactivate = null;

    private bool isActivated = false;

    private PortalManager portalManager = null;


    private void Awake()
    {
        portalManager = GameObject.FindGameObjectWithTag("PortalManager").GetComponent<PortalManager>();
    }

    private void Start()
    {
        DeactivatePortal();
    }

    public bool IsActivated()
    {
        return isActivated;
    }

    public void ActivatePortal()
    {
        if (portalMr != null)
        {
            portalMr.material = matActivate;
            isActivated = true;
        }
    }

    public void DeactivatePortal()
    {
        if (portalMr != null)
        {
            portalMr.material = matDeactivate;
            isActivated = false;
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player") ||
            _other.CompareTag("Projectile") ||
            _other.CompareTag("Enemy"))
        {
            if (portalManager) portalManager.PassProcess(portalType, _other.gameObject);
        }
    }

    public void PassProcess(GameObject _obj)
    {
        // 플레이어는 캐릭터 컨트롤러 때문에 위치 변경 방법이 달라서 별도 처리
        PlayerController pc = _obj.GetComponent<PlayerController>();
        if (pc)
        {
            // 뒤로 이동
            pc.Warp(transform.position + (transform.forward * -2f));
        }
        else
        {
            _obj.transform.rotation = transform.rotation;
            _obj.transform.position = transform.position + (transform.forward * 5f);
        }
    }
}
