using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public interface Weapon
public abstract class Weapon : MonoBehaviour
{
    private float damage = 0f;

    public abstract void Use();
    // �����Լ�
    //public virtual void VirtualTest() { }
    // ���������Լ�
    //public virtual void PureVirtualTest() = 0;
}
