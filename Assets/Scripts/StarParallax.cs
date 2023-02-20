using UnityEngine;

public class StarParallax : MonoBehaviour {
  public Transform cameraTransform;

  public float parallaxFactor;

  private Vector3 _startPos;

  private void Start() { _startPos = transform.position; }

  private void Update() {
    var newPos = cameraTransform.position * parallaxFactor;

    newPos.z = 0;

    newPos.x += _startPos.x;
    newPos.y += _startPos.y;

    transform.position = newPos;
  }
}
