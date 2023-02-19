using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {
  public float shootTimer = 3f;
  private float _health = 100f;

  public GameObject healthBar;

  public GameObject explosionPrefab;
  public GameObject bulletPrefab;

  [PublicAPI]
  public float Health {
    get => _health;
    set {
      _health = value;

      OnHealthChanged();
    }
  }

  private void Awake() { Health = 100f; }

  private void FixedUpdate() {
    if (Global.Player is null || Global.Player.IsDestroyed()) return;

    var trans = transform;

    // rotate towards player
    var dir = Global.Player.transform.position - trans.position;
    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    if (shootTimer <= 0.5f) {
      Debug.Log("shoot");
      Instantiate(bulletPrefab, trans.position, trans.rotation);
      shootTimer = 3f;
    }
    else {
      shootTimer -= Time.deltaTime;
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("PlayerBullet")) {
      Health -= 5;

      Destroy(other.gameObject);
    }
  }

  private void OnHealthChanged() {
    var scale = healthBar.transform.localScale;

    scale.x = _health / 100;

    healthBar.transform.localScale = scale;

    if (_health <= 0) Die();
  }

  private void Die() {
    var trans = transform;

    Instantiate(explosionPrefab, trans.position, trans.rotation);

    Destroy(gameObject);
  }
}
