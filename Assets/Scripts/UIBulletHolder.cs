using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBulletHolder : MonoBehaviour
{
    [SerializeField] GameObject[] bulletImgs = null;


    private void Awake()
    {
        UpdateCount(0);
    }

    public void UpdateCount(int _cnt)
    {
        if (_cnt > bulletImgs.Length) _cnt = bulletImgs.Length;

        for (int i = 0; i < bulletImgs.Length; ++i)
        {
            if (i < _cnt) bulletImgs[i].SetActive(true);
            else bulletImgs[i].SetActive(false);
        }
    }
}
