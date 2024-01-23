using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 dir = Vector3.zero;
    private float speed = 20f;
    private float dmg = 1f;
    private float duration = 5f;

    [SerializeField]
    private GameObject fxHitGo = null;

    private GameObject ownerGo = null;


    public void Shoot(Vector3 _dir, GameObject _ownerGo)
    {
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

        if (fxHitGo != null) Instantiate(fxHitGo, transform.position, Quaternion.identity);

        if (_other.CompareTag("Enemy"))
            _other.GetComponent<Enemy>().Damage(dmg);
        // 플레이어 데미지 처리

        Destroy(gameObject);
    }
}
