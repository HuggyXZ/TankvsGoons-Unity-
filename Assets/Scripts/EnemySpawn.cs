using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject enemy;

    [SerializeField] private float spawnTimer = 2f;
    [SerializeField] private float spawnRateIncreaseTimer = 7.5f;
    [SerializeField] private float spawnTimerDecreaseValue = 0.25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnRateIncrease());
    }

    IEnumerator SpawnEnemy() {
        int nextSpawnLocation = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnTimer);
        if (!GameManager.instance.GetGameOver()) {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnRateIncrease() {
        yield return new WaitForSeconds(spawnRateIncreaseTimer);
        float spawnTimerLimit = 0.5f;

        if (spawnTimer > spawnTimerLimit) {
            spawnTimer -= spawnTimerDecreaseValue;
            StartCoroutine(SpawnRateIncrease());
        }

        StartCoroutine(SpawnRateIncrease());
    }
}
