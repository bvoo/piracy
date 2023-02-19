using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour {
  public GameObject bulletPrefab;
  public GameObject healthBar;
  public float shootTimer = 3f;
  private float _health = 100f;

  [PublicAPI]
  public float Health {
    get => _health;
    set {
      _health = value;
      OnHealthChanged();
    }
  }

  private void FixedUpdate() {
    if (shootTimer <= 0.5f) {
      Instantiate(bulletPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
      shootTimer = 3f;
    }
    else {
      shootTimer -= Time.deltaTime;
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (!other.CompareTag("Bullet")) return;

    Health -= 5;

    Destroy(other.gameObject);
  }

  private void OnHealthChanged() {
    var scale = healthBar.transform.localScale;

    scale.x = _health / 100;

    healthBar.transform.localScale = scale;

    if (_health <= 0) Destroy(gameObject);
  }
}
