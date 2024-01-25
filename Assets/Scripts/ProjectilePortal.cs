using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePortal : MonoBehaviour
{
    private Vector3 dir = Vector3.zero;
    private float speed = 20f;
    private float dmg = 1f;
    private float duration = 5f;

    private GameObject ownerGo = null;

    private EPortalType portalType = EPortalType.Red;
    private PortalManager portalManager = null;


    private void Awake()
    {
        portalManager =
            GameObject.FindGameObjectWithTag("PortalManager").GetComponent<PortalManager>();
    }

    public void Shoot(EPortalType _portalType, Vector3 _dir, GameObject _ownerGo)
    {
        portalType = _portalType;
        dir = _dir;
        ownerGo = _ownerGo;

        Destroy(gameObject, duration);
    }

    private void Update()
    {
        if (dir.Equals(Vector3.zero) == true) return;

        transform.position =
            transform.position +
            dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (ownerGo == null) return;

        if (ownerGo.CompareTag(_other.tag) ||
            _other.CompareTag("Weapon") ||
            _other.CompareTag("Projectile")) return;

        if (portalManager)
        {
            portalManager.ActivatePortal(portalType, transform.position);
        }

        Destroy(gameObject);
    }
}
