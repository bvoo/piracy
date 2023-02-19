using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public Camera mainCamera;

  public float moveSpeedMax = 5f;
  public float moveSpeedMul = 5f;

  public float fireRate = 0.2f;

  public GameObject bulletPrefab;
  public Rigidbody2D rb;

  private float _lastFire;

  private void FixedUpdate() {
    var mul = InputHelper.MoveInput * moveSpeedMul;

    rb.velocity = new Vector2(
      Math.Clamp(rb.velocity.x + mul.x, -moveSpeedMax, moveSpeedMax),
      Math.Clamp(rb.velocity.y + mul.y, -moveSpeedMax, moveSpeedMax)
    );
  }

  private void Update() {
    var trans = transform;

    var pos = trans.position;

    var mousePos = Input.mousePosition;

    var mouseWorld = mainCamera.ScreenToWorldPoint(mousePos);

    var dir = mouseWorld - transform.position;
    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    // shoot m0
    if (InputHelper.FirePressed && !(Time.time - _lastFire < fireRate)) {
      Fire(pos, mouseWorld);
    }
  }

  private void Fire(Vector2 start, Vector2 end) {
    _lastFire = Time.time;

    Debug.DrawLine(start, end, Color.red, 1f);

    Instantiate(bulletPrefab, start, transform.rotation);
  }
}
