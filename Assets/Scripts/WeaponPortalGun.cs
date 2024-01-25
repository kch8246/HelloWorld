using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPortalGun : Weapon
{
    public delegate void ReloadDoneCallbackDelegate();
    private ReloadDoneCallbackDelegate reloadDoneCallback = null;
    public ReloadDoneCallbackDelegate ReloadDoneCallback { set { reloadDoneCallback = value; } }

    [SerializeField]
    private Transform bulletSpawnPointTr = null;

    private GameObject projectilePortalRedGo = null;
    private GameObject projectilePortalBlueGo = null;

    private PortalManager portalMng = null;

    private readonly float cooltime = 0.5f;
    private float lastTime = 0f;

    private const int maxBulletCnt = 10;    // 최대 총알 갯수
    private int bulletCnt = maxBulletCnt;   // 총알 갯수
    private float reloadTime = 2f;          // 재장전 시간
    public int BulletCnt { get { return bulletCnt; } }


    private void Awake()
    {
        projectilePortalRedGo = Resources.Load("Prefabs\\P_PortalRedBullet") as GameObject;
        projectilePortalBlueGo = Resources.Load("Prefabs\\P_PortalBlueBullet") as GameObject;
    }

    // 포탈은 타입이 필요해서 어정쩡하지만 Use 오버로딩
    public override void Use(GameObject _ownerGo)
    {
        //if (CheckAvailable())
        //    Fire(_ownerGo);
    }

    public void Use(GameObject _ownerGo, EPortalType _type)
    {
        if(CheckAvailable())
            Fire(_ownerGo, _type);
    }

    private void Fire(GameObject _ownerGo, EPortalType _type)
    {
        if (Time.time - lastTime < cooltime) return;
        
        GameObject projectile = null;
        if (_type == EPortalType.Red)
            projectile = Instantiate(projectilePortalRedGo);
        else
            projectile = Instantiate(projectilePortalBlueGo);
        projectile.transform.position =
            bulletSpawnPointTr.position;
        projectile.GetComponent<ProjectilePortal>().Shoot(
            _type,
            bulletSpawnPointTr.forward,
            _ownerGo);

        lastTime = Time.time;

        --bulletCnt;
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
}
