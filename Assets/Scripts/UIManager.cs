using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIBulletHolder bulletHolder = null;
    [SerializeField] private UIKillCountHolder killCountHolder = null;


    public void UpdateBulletCount(int _cnt)
    {
        bulletHolder.UpdateCount(_cnt);
    }

    public void UpdateKillCount(int _killCnt, int _enemyCnt, int _maxEnemyCnt)
    {
        killCountHolder.UpdateCount(_killCnt, _enemyCnt, _maxEnemyCnt);
    }
}
