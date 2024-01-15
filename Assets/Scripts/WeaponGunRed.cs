using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGunRed : WeaponGun
{
    private void Start()
    {
        SetBullet(Resources.Load("Prefabs\\P_RedBullet") as GameObject);
    }
}
