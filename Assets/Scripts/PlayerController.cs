using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public Camera mainCamera;

  public float moveSpeedMax = 5f;
  public float moveSpeedMul = 5f;

  public float fireRate = 0.2f;

  public GameObject bulletPrefab;
  public GameObject explosionPrefab;

  public Rigidbody2D rb;
  public GameObject healthBar;
  public TextMeshProUGUI healthText;

  private float _health = 100f;

  private float _lastFire;

  [PublicAPI]
  public float Health {
    get => _health;
    set {
      _health = value;
      OnHealthChanged();
    }
  }

  private void Awake() {
    Global.Player = gameObject;
    Health = 100f;
  }

  private void Update() {
    var trans = transform;

    var pos = trans.position;

    var mousePos = Input.mousePosition;

    var mouseWorld = mainCamera.ScreenToWorldPoint(mousePos);

    var dir = mouseWorld - transform.position;
    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    if (InputHelper.FirePressed && !(Time.time - _lastFire < fireRate)) Fire(pos, mouseWorld);
  }

  private void FixedUpdate() {
    var mul = InputHelper.MoveInput * moveSpeedMul;

    rb.velocity = new Vector2(
      Math.Clamp(rb.velocity.x + mul.x, -moveSpeedMax, moveSpeedMax),
      Math.Clamp(rb.velocity.y + mul.y, -moveSpeedMax, moveSpeedMax)
    );
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("EnemyBullet")) {
      Health -= 10;
    }
  }

  private void Fire(Vector2 start, Vector2 end) {
    _lastFire = Time.time;

    Debug.DrawLine(start, end, Color.red, 1f);

    Instantiate(bulletPrefab, start, transform.rotation);
  }

  private void OnHealthChanged() {
    var scale = healthBar.transform.localScale;

    scale.x = _health / 100;

    healthBar.transform.localScale = scale;

    healthText.text = $"{Math.Clamp(_health, 0, 100)}";

    if (_health <= 0) Die();
  }

  private void Die() {
    var trans = transform;

    Instantiate(explosionPrefab, trans.position, trans.rotation);

    Destroy(gameObject);
  }
}
