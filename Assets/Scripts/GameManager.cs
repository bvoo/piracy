using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
  public GameObject gameOverCanvas;
  public TextMeshProUGUI gameOverScoreText;
  public TextMeshProUGUI scoreText;
  public TextField nameTextField;

  private int _score;

  [PublicAPI]
  public int Score {
    get => _score;
    set {
      _score = value;
      OnScoreChange();
    }
  }

  private void Awake() {
    Global.GameManager = this;

    if (Global.StoredName is not null) {
      nameTextField.value = Global.StoredName;
    }
  }

  public void OnEnemyDeath() { Score += 5; }

  private void OnScoreChange() {
    scoreText.text = $"{Score}";
    gameOverScoreText.text = $"Score {Score}";
  }

  public void OnPlayerDie() { gameOverCanvas.SetActive(true); }

  public void GameExit() {
    SaveScore();
    SceneManager.LoadScene("MainMenu");
  }

  public void Retry() {
    SaveScore();
    SceneManager.LoadScene("Main");
  }

  private void SaveScore() {
    Global.StoredName = nameTextField.value;
    var existing = XmlManager.instance.leaderboard.list.FirstOrDefault(x => x.Name == Global.StoredName);

    if (existing is not null) {
      XmlManager.instance.leaderboard.list.Remove(existing);
    }

    var newEntry = new HighScoreEntry {
      Name = Global.StoredName,
      Value = Score,
    };

    XmlManager.instance.leaderboard.list.Add(newEntry);
  }
}
