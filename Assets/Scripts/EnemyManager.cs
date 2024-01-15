using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Vector2 stageSize = new Vector2(100f, 100f);
    [SerializeField] private GameObject enemyGo = null;


    private void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            GameObject enemy = Instantiate(
                enemyGo,
                new Vector3(Random.Range(stageSize.x * -0.5f, stageSize.x * 0.5f),
                            0f,
                            Random.Range(stageSize.y * -0.5f, stageSize.y * 0.5f)),
                Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}