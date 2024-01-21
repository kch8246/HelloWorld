using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QuadTree / OcTree
// AABB / OBB
// Frustum Culling / Occllusion Culling
public class WeaponGun : Weapon
{
    public delegate void ReloadDoneCallbackDelegate();
    private ReloadDoneCallbackDelegate reloadDoneCallback = null;
    public ReloadDoneCallbackDelegate ReloadDoneCallback { set { reloadDoneCallback = value; } }

    [SerializeField]
    private Transform bulletSpawnPointTr = null;

    private GameObject projectileGo = null;

    private readonly float cooltime = 0.5f;
    private float lastTime = 0f;

    private const int maxBulletCnt = 10;    // ÃÖ´ë ÃÑ¾Ë °¹¼ö
    private int bulletCnt = maxBulletCnt;   // ÃÑ¾Ë °¹¼ö
    private float reloadTime = 2f;          // ÀçÀåÀü ½Ã°£
    public int BulletCnt { get { return bulletCnt; } }

    // Overriding / Overloading
    public override void Use(GameObject _ownerGo)
    {
        if (CheckAvailable())
            Fire(_ownerGo);
    }

    private void Fire(int _i) { }
    private void Fire(float _f) { }
    private void Fire(int _i1, int _i2) { }

    private void Fire(GameObject _ownerGo)
    {
        if (projectileGo == null) return;

        if (Time.time - lastTime < cooltime) return;

        GameObject projectile =
            Instantiate(projectileGo);
        projectile.transform.position =
            bulletSpawnPointTr.position;
        projectile.GetComponent<Projectile>().Shoot(
            bulletSpawnPointTr.forward,
            _ownerGo);

        lastTime = Time.time;

        --bulletCnt;
    }

    protected void SetBullet(GameObject _bulletGo)
    {
        projectileGo = _bulletGo;
    }

    public bool CheckAvailable()
    {
        return bulletCnt > 0;
    }

    public void Reload()
    {
        Invoke("ReloadDone", reloadTime);
    }

    private void ReloadDone()
    {
        bulletCnt = maxBulletCnt;
        reloadDoneCallback?.Invoke();
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
