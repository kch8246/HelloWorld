using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public interface Weapon
public abstract class Weapon : MonoBehaviour
{
    private float damage = 0f;

    public abstract void Use();
    // 가상함수
    //public virtual void VirtualTest() { }
    // 순수가상함수
    //public virtual void PureVirtualTest() = 0;
}
