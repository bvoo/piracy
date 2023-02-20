using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XmlManager : MonoBehaviour {
  public static XmlManager Instance;
  public Leaderboard leaderboard;

  private void Awake() {
    Instance = this;

    var leaderboardData = PlayerPrefs.GetString("leaderboard", "");

    if (leaderboardData is null or "") return;

    var serializer = new XmlSerializer(typeof(Leaderboard));

    leaderboard = serializer.Deserialize(GenerateStreamFromString(leaderboardData)) as Leaderboard;
  }

  public void SaveScores() {
    var serializer = new XmlSerializer(typeof(Leaderboard));

    var stream = new MemoryStream();

    serializer.Serialize(stream, leaderboard);

    PlayerPrefs.SetString("leaderboard", stream.ToString());

    PlayerPrefs.Save();
  }

  private static Stream GenerateStreamFromString(string s) {
    var stream = new MemoryStream();
    var writer = new StreamWriter(stream);
    writer.Write(s);
    writer.Flush();
    stream.Position = 0;
    return stream;
  }
}
