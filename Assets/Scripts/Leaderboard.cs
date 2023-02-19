using System.Collections.Generic;

[System.Serializable]
public class Leaderboard {
  public List<HighScoreEntry> list = new();
}

public class HighScoreEntry {
  public string Name;
  public int Value;
}
