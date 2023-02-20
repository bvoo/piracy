using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
  public void StartGame() {
    SceneManager.LoadScene("Main");
  }
  
  public void Exit() {
    Application.Quit(0);
  }
}
