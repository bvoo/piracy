using System;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

  private void Update() {
    if (Keyboard.current.escapeKey.isPressed) {
      SceneManager.LoadScene("MainMenu");
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
    Global.StoredName = nameTextField?.value ?? "Player";

    var existing = XmlManager.Instance.leaderboard.List.FirstOrDefault(x => x.Name == Global.StoredName);

    if (existing is not null) {
      XmlManager.Instance.leaderboard.List.Remove(existing);
    }

    var newEntry = new HighScoreEntry {
      Name = Global.StoredName,
      Value = Score,
    };

    XmlManager.Instance.leaderboard.List.Add(newEntry);

    XmlManager.Instance.SaveScores();
  }
}
