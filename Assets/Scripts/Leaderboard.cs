using System.Collections.Generic;

[System.Serializable]
public class Leaderboard {
  public List<HighScoreEntry> List = new();
}

public class HighScoreEntry {
  public string Name;
  public int Value;
}
