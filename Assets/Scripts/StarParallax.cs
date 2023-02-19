using UnityEngine;

public class StarParallax : MonoBehaviour {
  public Transform cameraPos;

  private void Update() {
    var pos = cameraPos.position;

    transform.rotation = Quaternion.Euler(pos.y, -pos.x, 0);
  }
}
