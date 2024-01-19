using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIBulletHolder bulletHolder = null;


    public void UpdateBulletCount(int _cnt)
    {
        bulletHolder.UpdateCount(_cnt);
    }
}
