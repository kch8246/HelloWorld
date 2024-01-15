using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 dir = Vector3.zero;
    private float speed = 20f;
    private float dmg = 1f;

    [SerializeField]
    private GameObject fxHitGo = null;


    public void Shoot(Vector3 _dir)
    {
        dir = _dir;
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
        if (!_other.CompareTag("Player") && !_other.CompareTag("Weapon"))
        {
            if (fxHitGo != null) Instantiate(fxHitGo, transform.position, Quaternion.identity);

            if (_other.CompareTag("Enemy"))
                _other.GetComponent<Enemy>().Damage(dmg);

            Destroy(gameObject);
        }
    }
}
