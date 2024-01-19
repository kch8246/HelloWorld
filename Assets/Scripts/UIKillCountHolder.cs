using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIKillCountHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killCount = null;
    [SerializeField] private TextMeshProUGUI enemyCount = null;


    public void UpdateCount(int _killCnt, int _enemyCnt, int _maxEnemyCnt)
    {
        killCount.text = _killCnt.ToString() + " KILL";
        enemyCount.text = _enemyCnt.ToString() + " / " + _maxEnemyCnt.ToString();
    }
}
