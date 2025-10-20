using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemy2;

    [SerializeField] private float spawnTimer = 2f;
    private float spawnRateIncreaseTimer = 10f;
    private float spawnTimerDecreaseValue = 0.25f;

    [SerializeField] private float spawnTimer2 = 8f;
    private float spawnRateIncreaseTimer2 = 15f;
    private float spawnTimerDecreaseValue2 = 1f;


    private void Start() {
        GameManager.instance.OnCoinValueReached += GameManager_OnCoinValueReached;

        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnRateIncrease());
    }

    private void GameManager_OnCoinValueReached(object sender, System.EventArgs e) {
        StartCoroutine(SpawnEnemy2());
        StartCoroutine(SpawnRateIncrease2());
    }


    IEnumerator SpawnEnemy() {
        int nextSpawnLocation = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnTimer);
        if (!GameManager.instance.GetGameOver()) {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy2() {
        int nextSpawnLocation = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy2, spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnTimer2);

        if (!GameManager.instance.GetGameOver()) {
            StartCoroutine(SpawnEnemy2());
        }
    }

    IEnumerator SpawnRateIncrease() {
        yield return new WaitForSeconds(spawnRateIncreaseTimer);
        float spawnTimerLimit = 1f;

        if (spawnTimer > spawnTimerLimit) {
            spawnTimer -= spawnTimerDecreaseValue;
        }

        StartCoroutine(SpawnRateIncrease());
    }

    IEnumerator SpawnRateIncrease2() {
        yield return new WaitForSeconds(spawnRateIncreaseTimer2);
        float spawnTimerLimit = 3f;

        if (spawnTimer2 > spawnTimerLimit) {
            spawnTimer2 -= spawnTimerDecreaseValue2;
        }

        StartCoroutine(SpawnRateIncrease2());
    }


}
