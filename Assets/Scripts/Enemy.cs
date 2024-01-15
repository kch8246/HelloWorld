using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float hp = 3f;


    public void Damage(float _dmg)
    {
        hp -= _dmg;
        if (hp <= 0f) Destroy(gameObject);
    }
}
