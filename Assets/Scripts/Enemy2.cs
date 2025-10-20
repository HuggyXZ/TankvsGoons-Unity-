using System.Collections;
using UnityEngine;

public class Enemy2 : MonoBehaviour {
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource damagedSound;
    [SerializeField] private AudioSource playerDeathSound;

    [SerializeField] float enemyHealth = 200f;
    [SerializeField] float enemyMoveSpeed = 5f;
    Quaternion targetRotation;
    Vector2 moveDirection;
    bool disableEnemy = false;

    // Update is called once per frame
    void Update() {

        if (!GameManager.instance.GetGameOver() && !disableEnemy) {
            MoveEnemy();
            RotateEnemy();
        }
    }

    private void MoveEnemy() {
        transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position, enemyMoveSpeed * Time.deltaTime);
    }

    private void RotateEnemy() {
        moveDirection = (Player.Instance.transform.position - transform.position).normalized;

        targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);

        if (transform.rotation != targetRotation) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out Bullet bullet)) {
            StartCoroutine(Damaged());

            enemyHealth -= Player.Instance.GetBulletDamamge();

            if (enemyHealth <= 0) {
                AudioSource.PlayClipAtPoint(deathSound.clip, transform.position);
                GameManager.instance.AddScore(5);
                Destroy(gameObject);
            }
            else {
                AudioSource.PlayClipAtPoint(damagedSound.clip, transform.position);
            }

            Destroy(bullet.gameObject);
        }
        else if (collision.gameObject.TryGetComponent(out Player player)) {
            AudioSource.PlayClipAtPoint(playerDeathSound.clip, player.transform.position);
            GameManager.instance.GameOver();
            player.gameObject.SetActive(false);
        }
    }

    IEnumerator Damaged() {
        disableEnemy = true;
        yield return new WaitForSeconds(3f);
        disableEnemy = false;
    }
}
