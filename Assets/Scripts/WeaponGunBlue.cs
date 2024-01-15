using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGunBlue : WeaponGun
{
    private void Start()
    {
        SetBullet(Resources.Load("Prefabs\\P_BlueBullet") as GameObject);
    }
}
