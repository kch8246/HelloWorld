using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public delegate void DiedCallbackDelegate(Enemy _enemy);
    private DiedCallbackDelegate diedCallback = null;
    public DiedCallbackDelegate DiedCallback { set { diedCallback = value; } }

    private WeaponGun weaponGun = null;
    private GameObject targetGo = null;
    //private CharacterController characterController = null;
    private NavMeshAgent agent = null;

    private float hp = 3f;
    private float atkRange = 10f;   // 공격 가능 거리
    private float stopDist = 5f;    // 플레이어와의 거리유지
    private float rotSpeed = 50f;   // 회전속도
    private float moveSpeed = 10f;  // 이동속도


    private void Awake()
    {
        weaponGun = GetComponentInChildren<WeaponGun>();

        targetGo = GameObject.FindGameObjectWithTag("Player");
        if (!targetGo) Debug.LogError("Player not found!");

        //characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //LookAtTarget();
        //ChaseTarget();
        ChaseTargetWithNav();
        AttackTarget();
    }

    private void LookAtTarget()
    {
        if (!targetGo) return;

        Vector3 dir = (targetGo.transform.position - transform.position).normalized;
        // Radian, Degree
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        transform.rotation =
            Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 90f + -angle, 0f), rotSpeed * Time.deltaTime);
    }

    private void ChaseTarget()
    {
        if (!targetGo) return;

        Vector3 enemyPos = transform.position;
        Vector3 targetPos = targetGo.transform.position;
        if (Vector3.Distance(targetPos, enemyPos) > stopDist)
        {
            Vector3 dir = (targetPos - enemyPos).normalized;
            //characterController.SimpleMove(dir * moveSpeed);
        }
    }

    private void ChaseTargetWithNav()
    {
        if (!targetGo) return;

        Vector3 enemyPos = transform.position;
        Vector3 targetPos = targetGo.transform.position;
        if (Vector3.Distance(targetPos, enemyPos) > stopDist)
            agent.SetDestination(targetGo.transform.position);
    }

    private void AttackTarget()
    {
        if (!targetGo) return;

        if (Vector3.Distance(targetGo.transform.position, transform.position) < atkRange)
        {
            if (weaponGun)
            {
                if (weaponGun.CheckAvailable())
                    weaponGun.Use(this.gameObject);
                else
                    weaponGun.Reload();
            }
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
