using UnityEngine;

public class BulletController : MonoBehaviour {
  public float lifetime = 5f;
  public float speed = 5f;

  private void Start() { Destroy(gameObject, lifetime); }

  private void Update() {
    var trans = transform;

    var direction = trans.up;

    trans.position += direction * (speed * Time.deltaTime);
  }
}
