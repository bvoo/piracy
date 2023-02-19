using TMPro;
using UnityEngine;

public class LeaderboardDisplay : MonoBehaviour {
  public GameObject scoreList;
  public GameObject scorePrefab;

  private void Awake() {
    var leaderboard = XmlManager.instance.leaderboard;

    foreach (var entry in leaderboard.list) {
      var obj = Instantiate(scorePrefab, scoreList.transform);
      var tmp = obj.GetComponent<TextMeshProUGUI>();

      tmp.text = $"{entry.Name} {entry.Value}";
    }
  }
}
