using System.Collections;
using UnityEngine;

public class CoinSpawn : MonoBehaviour {

    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject coin;

    [SerializeField] private float spawnTimer = 10f;

    void Start() {
        StartCoroutine(SpawnCoin());
    }

    IEnumerator SpawnCoin() {
        int nextSpawnLocation = Random.Range(0, spawnPoints.Length);
        Instantiate(coin, spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnTimer);
        if (!GameManager.instance.GetGameOver()) {
            StartCoroutine(SpawnCoin());
        }
    }
}
