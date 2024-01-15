using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QuadTree / OcTree
// AABB / OBB
// Frustum Culling / Occllusion Culling
public class WeaponGun : Weapon
{
    [SerializeField]
    private Transform bulletSpawnPointTr = null;

    private GameObject projectileGo = null;


    // Overriding / Overloading
    public override void Use()
    {
        Fire();
    }

    private void Fire(int _i) { }
    private void Fire(float _f) { }
    private void Fire(int _i1, int _i2) { }

    private void Fire()
    {
        if (projectileGo == null) return;

        GameObject projectile =
            Instantiate(projectileGo);
        projectile.transform.position =
            bulletSpawnPointTr.position;
        projectile.GetComponent<Projectile>().Shoot(
            bulletSpawnPointTr.forward);
    }

    protected void SetBullet(GameObject _bulletGo)
    {
        projectileGo = _bulletGo;
    }

    //private void Update()
    //{
    //    ChangeBullet();
    //}

    //private void ChangeBullet()
    //{
    //    if (Input.GetKey(KeyCode.Alpha1))
    //    {
    //        projectileGo =
    //            Resources.Load(
    //                "Prefabs\\P_RedBullet")
    //            as GameObject;
    //    }

    //    if (Input.GetKey(KeyCode.Alpha2))
    //    {
    //        projectileGo =
    //            Resources.Load(
    //                "Prefabs\\P_BlueBullet")
    //            as GameObject;
    //    }
    //}
}
