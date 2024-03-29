using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Vector2 stageSize = new Vector2(100f, 100f);
    [SerializeField] private GameObject enemyGo = null;

    private List<Enemy> enemyList = new List<Enemy>();
    private int maxEnemyCnt = 10;

    private UIManager uiMng = null;
    private int killCount = 0;


    private void Awake()
    {
        uiMng = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            if (enemyList.Count < maxEnemyCnt)
            {
                GameObject go = Instantiate(
                    enemyGo,
                    new Vector3(Random.Range(stageSize.x * -0.5f, stageSize.x * 0.5f),
                                0f,
                                Random.Range(stageSize.y * -0.5f, stageSize.y * 0.5f)),
                    Quaternion.identity);

                Enemy enemy = go.GetComponent<Enemy>();
                enemy.DiedCallback = EnemyDiedCallback;

                enemyList.Add(enemy);

                uiMng.UpdateKillCount(killCount, enemyList.Count, maxEnemyCnt);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void EnemyDiedCallback(Enemy _enemy)
    {
        enemyList.Remove(_enemy);

        ++killCount;
        uiMng.UpdateKillCount(killCount, enemyList.Count, maxEnemyCnt);
    }
}