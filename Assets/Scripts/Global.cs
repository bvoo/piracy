using JetBrains.Annotations;
using UnityEngine;

public class Global : MonoBehaviour {
  [CanBeNull] public static string StoredName = null;

  public static GameObject Player;
  public static GameManager GameManager;
}
