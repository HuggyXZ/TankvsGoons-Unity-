using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour {
    public static StatsUI instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Awake() {
        instance = this;
    }

    void Update() {
        scoreText.text = "Score: " + GameManager.instance.GetScore();
    }

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
    }
}
