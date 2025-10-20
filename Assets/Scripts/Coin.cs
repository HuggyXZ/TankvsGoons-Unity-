using UnityEngine;

public class Coin : MonoBehaviour {
    [SerializeField] private AudioSource collectSound;

    private int coinScore = 3;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            AudioSource.PlayClipAtPoint(collectSound.clip, transform.position);
            GameManager.instance.AddScore(coinScore);
            Destroy(gameObject);
        }
    }
}
