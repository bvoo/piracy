using TMPro;
using UnityEngine;

public class LeaderboardDisplay : MonoBehaviour {
  public GameObject scoreList;
  public GameObject scorePrefab;

  private void Awake() {
    var leaderboard = XmlManager.Instance.leaderboard;

    foreach (var entry in leaderboard.List) {
      var obj = Instantiate(scorePrefab, scoreList.transform);
      var tmp = obj.GetComponent<TextMeshProUGUI>();

      tmp.text = $"{entry.Name} {entry.Value}";
    }
  }
}
