using UnityEngine;

public class BulletController : MonoBehaviour {
  public float lifetime = 5f;
  public float speed = 5f;

  private void Start() { Destroy(gameObject, lifetime); }

  private void FixedUpdate() { transform.position += Vector3.up * speed; }
}
