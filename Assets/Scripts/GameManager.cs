using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance { get; private set; }

    public event EventHandler OnCoinValueReached;
    private int coinValue = 10;

    private bool gameOver = false;
    private bool coinEventTriggered = false;

    [SerializeField] private int score = 0;



    private void Awake() {
        instance = this;
    }

    private void Update() {
        if (!coinEventTriggered && score >= coinValue) {
            coinEventTriggered = true;
            OnCoinValueReached?.Invoke(this, EventArgs.Empty);
        }
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
