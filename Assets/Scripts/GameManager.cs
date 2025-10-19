using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {

    public static GameManager instance { get; private set; }

    private bool gameOver = false;
    [SerializeField] private int score = 0;

    private void Awake() {
        instance = this;
    }

    public void GameOver() {
        gameOver = true;
        StatsUI.instance.GameOver();
        Invoke("Retry", 3f);
    }

    public void AddScore(int value) {
        score += value;
    }

    public void Retry() {
        SceneManager.LoadScene(0);
    }

    public bool GetGameOver() {
        return gameOver;
    }

    public int GetScore() {
        return score;
    }

}
