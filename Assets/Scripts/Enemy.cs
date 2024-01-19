using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void DiedCallbackDelegate(Enemy _enemy);
    private DiedCallbackDelegate diedCallback = null;
    public DiedCallbackDelegate DiedCallback { set { diedCallback = value; } }

    private Weapon weapon = null;
    private GameObject targetGo = null;

    private float hp = 3f;
    private float atkRange = 10f;   // ���� ���� �Ÿ�
    private float stopDist = 5f;    // �÷��̾���� �Ÿ�����
    private float rotSpeed = 50f;   // ȸ���ӵ�
    private float moveSpeed = 10f;  // �̵��ӵ�


    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();

        targetGo = GameObject.FindGameObjectWithTag("Player");
        if (!targetGo) Debug.LogError("Player not found!");
    }

    private void Update()
    {
        LookAtTarget();
        ChaseTarget();
        AttackTarget();
    }

    private void LookAtTarget()
    {
        if (!targetGo) return;

        Vector3 dir = targetGo.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        transform.rotation =
            Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 90f + -angle, 0f), rotSpeed * Time.deltaTime);
    }

    private void ChaseTarget()
    {
        if (!targetGo) return;

        if (Vector3.Distance(targetGo.transform.position, transform.position) > stopDist)
        {
            Vector3 dir = (targetGo.transform.position - transform.position).normalized;
            transform.position = transform.position + (dir * moveSpeed * Time.deltaTime);
        }
    }

    private void AttackTarget()
    {
        if (!targetGo) return;

        if (Vector3.Distance(targetGo.transform.position, transform.position) < atkRange)
        {
            if (weapon) weapon.Use();
        }
    }

    public void Damage(float _dmg)
    {
        hp -= _dmg;
        if (hp <= 0f)
        {
            diedCallback?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
