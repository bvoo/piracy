using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour {
  private float _health = 100f;

  [PublicAPI]
  public float Health {
    get => _health;
    set {
      _health = value;
      OnHealthChanged();
    }
  }

  public GameObject healthBar;

  private void OnHealthChanged() {
    var scale = healthBar.transform.localScale;

    scale.x = _health / 100;

    healthBar.transform.localScale = scale;

    if (_health <= 0) {
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (!other.CompareTag("Bullet")) return;

    Health -= 5;

    Destroy(other.gameObject);
  }
}
